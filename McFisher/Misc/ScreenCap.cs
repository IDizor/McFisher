using System.Collections.Concurrent;
using System.Drawing.Imaging;
using System.Runtime.ExceptionServices;
using SharpDX;
using SharpDX.Direct3D11;
using SharpDX.DXGI;

namespace McFisher.Misc;

public static class ScreenCap
{
    private enum Status
    {
        Starts = 1,
        Active,
        Stops,
        Inactive
    }

    private static volatile Status status;

    public static Exception globalException { get; set; }

    private static AutoResetEvent waitHandle { get; set; }

    private static ConcurrentQueue<Bitmap> bitmapQueue { get; set; }

    private static Thread captureThread { get; set; }

    private static Thread callbackThread { get; set; }

    public static bool SkipFirstFrame { get; set; }

    public static bool SkipFrames { get; set; }

    public static bool PreserveBitmap { get; set; }

    public static bool IsActive => status != Status.Inactive;

    public static bool IsNotActive => status == Status.Inactive;

    public static event EventHandler<OnScreenUpdatedEventArgs> OnScreenUpdated;

    public static event EventHandler<OnCaptureStopEventArgs> OnCaptureStop;

    private static Action<Bitmap> OnScreenUpdatedAction;

    public static int DisplayIndex;

    public static int AdapterIndex;

    static ScreenCap()
    {
        globalException = null;
        waitHandle = null;
        bitmapQueue = null;
        captureThread = null;
        callbackThread = null;
        status = Status.Inactive;
        SkipFirstFrame = true;
        SkipFrames = true;
        PreserveBitmap = false;
    }

    public static void StartCapture(int displayIndex = 0, int adapterIndex = 0)
    {
        StartCapture(null, displayIndex, adapterIndex);
    }

    public static void StartCapture(Action<Bitmap> onScreenUpdated, int displayIndex = 0, int adapterIndex = 0)
    {
        if (status == Status.Inactive)
        {
            OnScreenUpdatedAction = onScreenUpdated;
            DisplayIndex = displayIndex;
            AdapterIndex = adapterIndex;

            waitHandle = new AutoResetEvent(initialState: false);
            bitmapQueue = new ConcurrentQueue<Bitmap>();
            captureThread = new Thread((ThreadStart)delegate
            {
                CaptureMain(adapterIndex, displayIndex);
            });
            callbackThread = new Thread((ThreadStart)delegate
            {
                CallbackMain(onScreenUpdated);
            });
            status = Status.Starts;
            captureThread.Priority = ThreadPriority.Highest;
            captureThread.Start();
            callbackThread.Start();
        }
    }

    public static void StopCapture()
    {
        if (status == Status.Active)
        {
            status = Status.Stops;
        }
    }

    private static void CaptureMain(int adapterIndex, int displayIndex)
    {
        SharpDX.DXGI.Resource desktopResourceOut = null;
        try
        {
            using Factory1 factory = new Factory1();
            using Adapter1 adapter = factory.GetAdapter1(adapterIndex);
            using SharpDX.Direct3D11.Device device = new SharpDX.Direct3D11.Device(adapter);
            using Output output = adapter.GetOutput(displayIndex);
            using Output1 output2 = output.QueryInterface<Output1>();
            int num = output2.Description.DesktopBounds.Right - output2.Description.DesktopBounds.Left;
            int num2 = output2.Description.DesktopBounds.Bottom - output2.Description.DesktopBounds.Top;
            Rectangle rect = new Rectangle(Point.Empty, new Size(num, num2));
            Texture2DDescription texture2DDescription = default;
            texture2DDescription.CpuAccessFlags = CpuAccessFlags.Read;
            texture2DDescription.BindFlags = BindFlags.None;
            texture2DDescription.Format = Format.B8G8R8A8_UNorm;
            texture2DDescription.Width = num;
            texture2DDescription.Height = num2;
            texture2DDescription.OptionFlags = ResourceOptionFlags.None;
            texture2DDescription.MipLevels = 1;
            texture2DDescription.ArraySize = 1;
            texture2DDescription.SampleDescription.Count = 1;
            texture2DDescription.SampleDescription.Quality = 0;
            texture2DDescription.Usage = ResourceUsage.Staging;
            Texture2DDescription description = texture2DDescription;
            using Texture2D texture2D = new Texture2D(device, description);
            using OutputDuplication outputDuplication = output2.DuplicateOutput(device);
            status = Status.Active;
            int num3 = 0;
            do
            {
                try
                {
                    OutputDuplicateFrameInformation frameInfoRef;
                    Result result = outputDuplication.TryAcquireNextFrame(100, out frameInfoRef, out desktopResourceOut);
                    if (result.Success)
                    {
                        num3++;
                        using Texture2D source = desktopResourceOut.QueryInterface<Texture2D>();
                        device.ImmediateContext.CopyResource(source, texture2D);
                        DataBox dataBox = device.ImmediateContext.MapSubresource(texture2D, 0, MapMode.Read, SharpDX.Direct3D11.MapFlags.None);
                        Bitmap bitmap = new Bitmap(num, num2, PixelFormat.Format32bppRgb);
                        BitmapData bitmapData = bitmap.LockBits(rect, ImageLockMode.WriteOnly, bitmap.PixelFormat);
                        IntPtr intPtr = dataBox.DataPointer;
                        IntPtr intPtr2 = bitmapData.Scan0;
                        for (int i = 0; i < num2; i++)
                        {
                            Utilities.CopyMemory(intPtr2, intPtr, num * 4);
                            intPtr = IntPtr.Add(intPtr, dataBox.RowPitch);
                            intPtr2 = IntPtr.Add(intPtr2, bitmapData.Stride);
                        }

                        bitmap.UnlockBits(bitmapData);
                        device.ImmediateContext.UnmapSubresource(texture2D, 0);
                        while (SkipFrames && bitmapQueue.Count > 1)
                        {
                            bitmapQueue.TryDequeue(out var result2);
                            result2.Dispose();
                        }

                        if (num3 > 1 || !SkipFirstFrame)
                        {
                            bitmapQueue.Enqueue(bitmap);
                            waitHandle.Set();
                        }
                    }
                    else if (ResultDescriptor.Find(result).ApiCode != Result.WaitTimeout.ApiCode)
                    {
                        result.CheckError();
                    }
                }
                finally
                {
                    desktopResourceOut?.Dispose();
                    try
                    {
                        outputDuplication.ReleaseFrame();
                    }
                    catch
                    {
                    }
                }
            }
            while (status == Status.Active);
        }
        catch (Exception ex)
        {
            globalException = ex;
            status = Status.Stops;
        }
        finally
        {
            callbackThread.Join();
            Exception ex2 = globalException;
            while (bitmapQueue.Count > 0)
            {
                bitmapQueue.TryDequeue(out var result3);
                result3.Dispose();
            }

            globalException = null;
            waitHandle = null;
            bitmapQueue = null;
            captureThread = null;
            callbackThread = null;
            status = Status.Inactive;
            if (OnCaptureStop != null)
            {
                OnCaptureStop(null, new OnCaptureStopEventArgs(ex2 != null ? new Exception(ex2.Message, ex2) : null));
            }
            else if (ex2 != null)
            {
                //ExceptionDispatchInfo.Capture(ex2).Throw();
            }
        }
    }

    private static void CallbackMain(Action<Bitmap> onScreenUpdated)
    {
        try
        {
            while (status <= Status.Active)
            {
                Bitmap result;
                while (waitHandle.WaitOne(10) && bitmapQueue.TryDequeue(out result))
                {
                    try
                    {
                        onScreenUpdated?.Invoke(result);
                        OnScreenUpdated?.Invoke(null, new OnScreenUpdatedEventArgs(result));
                    }
                    finally
                    {
                        if (!PreserveBitmap)
                        {
                            result.Dispose();
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            globalException = ex;
            status = Status.Stops;
        }
    }
}

public class OnScreenUpdatedEventArgs : EventArgs
{
    public Bitmap Bitmap { get; set; }

    internal OnScreenUpdatedEventArgs(Bitmap bitmap)
    {
        Bitmap = bitmap;
    }
}

public class OnCaptureStopEventArgs : EventArgs
{
    public Exception Exception { get; set; }

    internal OnCaptureStopEventArgs(Exception exception)
    {
        Exception = exception;
    }
}