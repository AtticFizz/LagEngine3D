using Options;
using System.Diagnostics;
using Rendering;
using Rendering.Buffers;
using Renderables;

namespace LagEngine3D;

public partial class RenderView : Form
{
    private DebugView _debugView;

    private RenderBuffer _renderBuffer;

    public RenderView()
    {
        InitializeComponent();
        _debugView = new DebugView();
        _debugView.Show();
        _renderBuffer = new RenderBuffer(ClientSize.Width, ClientSize.Height);
    }

    private void RenderView_Load(object sender, EventArgs e)
    {
        DoubleBuffered = true;
        Paint += OnPaint;
        OnStart();
    }

    private void OnStart()
    {
        InfoText.Text = RenderOptions.InfoTextLoading;
        RenderOptions.ScreenWidth = ClientSize.Width;
        RenderOptions.ScreenHeight = ClientSize.Height;
        View.OnStart();
    }

    private void OnPaint(object? sender, PaintEventArgs e)
    {
        e.Graphics.DrawImageUnscaled(_renderBuffer.Image, 0, 0);
    }

    private Stopwatch stopWatch = new Stopwatch();

    private void FramerateTimer_Tick(object sender, EventArgs e)
    {
        stopWatch.Start();

        _renderBuffer.Clear(RenderOptions.BackgroundColor);

        View.OnUpdate();
        foreach (IRenderable renderable in Renderer.ObjectsToRender)
            renderable.Render(ref _renderBuffer);

        Refresh();

        stopWatch.Stop();

        double framerate = 1000.0 / (double)stopWatch.ElapsedMilliseconds;
        Time.DeltaTime = 1.0 / framerate; // DeltaTime ???
        stopWatch.Reset();
        InfoText.Text = ((int)framerate).ToString();
        RenderDebug.Clear();
        RenderDebug.LogLine("framerate: " + framerate.ToString());
        RenderDebug.LogLine("deltaTime: " + Time.DeltaTime.ToString());
        RenderDebug.LogLine("_meshRotation: " + (Time.DeltaTime * View._meshRotation).ToString());
    }

    private void RenderView_SizeChanged(object sender, EventArgs e)
    {
        RenderOptions.ScreenWidth = ClientSize.Width;
        RenderOptions.ScreenHeight = ClientSize.Height;
        _renderBuffer = new RenderBuffer(ClientSize.Width, ClientSize.Height);
    }

    private void RenderView_FormClosed(object sender, FormClosedEventArgs e)
    {
        _debugView.Close();
    }
}