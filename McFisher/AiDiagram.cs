using System.Drawing.Drawing2D;
using System.Drawing.Text;
using McFisher.AI;

namespace McFisher;

public partial class AiDiagram : Form
{
    private object _drawLock = new object();
    private Bitmap GraphBitmap;
    private Graphics Graph;
    private const int NeuronSize = 36;
    private const int NeuronZoneSize = NeuronSize + 6;
    private readonly Font NeuronFont = new Font(Form.DefaultFont.FontFamily, 12f);

    public AiBrain Brain;

    public AiDiagram()
    {
        InitializeComponent();
    }

    private void AiDiagram_FormClosing(object sender, FormClosingEventArgs e)
    {
        Hide();
        e.Cancel = true;
    }

    public void DrawAi(AiBrain brain)
    {
        lock (_drawLock)
        {
            if (brain == null)
            {
                return;
            }

            Brain = brain.Clone();
            Brain.Populate();
            AiIdTextBox.Text = Brain.Neurons[0].Id.ToString();
            var neuronsLayers = new List<List<Neuron>>();
            for (int i = 0; i <= Brain.LastLayerIndex; i++)
            {
                neuronsLayers.Add(Brain.Neurons.Where(n => n.Layer == i && (n.Outputs.Any() || n.Inputs.Any())).ToList());
            }
            
            var graphHeight = Math.Max(400, neuronsLayers.Max(l => l.Count + 1) * NeuronZoneSize);
            var neuronOffsetsY = new List<int>();
            for (int i = 0; i < neuronsLayers.Count; i++)
            {
                neuronOffsetsY.Add(AiTools.RandomInt(0, (int)(graphHeight / (neuronsLayers[i].Count + 1) / (i == 0 ? 4f : 2f))));
            }

            var neuronsLocations = new List<List<Point>>();
            for (int i = 0; i < neuronsLayers.Count; i++)
            {
                neuronsLocations.Add(new());
                for (int j = 0; j < neuronsLayers[i].Count; j++)
                {
                    int x = (NeuronZoneSize * 2) + AiTools.RandomInt(0, NeuronZoneSize / 3, true) + (i * NeuronZoneSize * 4);
                    int y = (graphHeight / (neuronsLayers[i].Count + 1)) * (j + 1) + AiTools.RandomInt(0, neuronOffsetsY[i], true);
                    neuronsLocations[i].Add(new(x, y));
                }
            }

            var graphWidth = neuronsLocations.Last().Max(p => p.X) + NeuronZoneSize;

            if (WindowState != FormWindowState.Maximized)
            {
                ClientSize = new(graphWidth, graphHeight + DrawPanel.Location.Y);
            }

            // draw
            GraphBitmap = new Bitmap(graphWidth, graphHeight);
            Graph = Graphics.FromImage(GraphBitmap);
            Graph.SmoothingMode = SmoothingMode.AntiAlias;
            Graph.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            Graph.Clear(DrawPanel.BackColor);

            // synapses
            for (int i = 1; i < neuronsLayers.Count; i++)
            {
                for (int j = 0; j < neuronsLayers[i].Count; j++)
                {
                    foreach (var synapse in neuronsLayers[i][j].Inputs)
                    {
                        var from = neuronsLocations[synapse.FromNeuron.Layer][neuronsLayers[synapse.FromNeuron.Layer].IndexOf(synapse.FromNeuron)];
                        var to = neuronsLocations[i][j];
                        DrawSynapse(from.X, from.Y, to.X, to.Y, synapse.Power);
                    }
                }
            }

            // neurons
            for (int i = 0; i < neuronsLayers.Count; i++)
            {
                for (int j = 0; j < neuronsLayers[i].Count; j++)
                {
                    DrawNeuron(neuronsLocations[i][j].X, neuronsLocations[i][j].Y,
                        i == 0 ? Brain.Neurons.IndexOf(neuronsLayers[i][j]).ToString() : "L" + i.ToString());
                }
            }

            // resize if needed
            if (GraphBitmap.Height > 900 || GraphBitmap.Width > 1900)
            {
                var x = Math.Min(900f / GraphBitmap.Height, 1900f / GraphBitmap.Width);
                GraphBitmap = new Bitmap(GraphBitmap, new Size((int)(GraphBitmap.Width * x), (int)(GraphBitmap.Height * x)));
            }

            // show graph
            DrawPanel.BackgroundImage = GraphBitmap;
        }
    }

    private void RedrawButton_Click(object sender, EventArgs e)
    {
        DrawAi(Brain);
    }

    private void DrawNeuron(int x, int y, string? text = null)
    {
        Graph.FillEllipse(new SolidBrush(Color.Azure), new(x - (NeuronSize / 2), y - (NeuronSize / 2), NeuronSize, NeuronSize));
        Graph.DrawEllipse(new(Color.MediumAquamarine, 2f), new(x - (NeuronSize / 2), y - (NeuronSize / 2), NeuronSize, NeuronSize));

        if (text != null)
        {
            var sf = new StringFormat();
            sf.LineAlignment = StringAlignment.Center;
            sf.Alignment = StringAlignment.Center;
            Graph.DrawString(text, NeuronFont, Brushes.Black, new Point(x, y + 1), sf);
        }
    }

    private void DrawSynapse(int x1, int y1, int x2, int y2, float power)
    {
        var color = power > 0
            ? Color.Orange
            : Color.LightSkyBlue;
        var width = Math.Abs(power) * 5;

        Graph.DrawLine(new(Color.FromArgb(128, color), width), x1, y1, x2, y2);
    }
}
