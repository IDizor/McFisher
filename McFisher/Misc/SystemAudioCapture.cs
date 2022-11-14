using NAudio.CoreAudioApi;
using NAudio.Wave;

namespace McFisher.Misc;

public class SystemAudioCapture : WasapiCapture
{
    public SystemAudioCapture(int bufferMs)
        : base(WasapiLoopbackCapture.GetDefaultLoopbackCaptureDevice(), false, bufferMs)
    {
    }

    protected override AudioClientStreamFlags GetAudioClientStreamFlags()
    {
        return AudioClientStreamFlags.Loopback;
    }

    public int SampleRate => WaveFormat.SampleRate;
}