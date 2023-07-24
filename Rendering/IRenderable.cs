using Rendering.Buffers;

namespace Renderables;

public interface IRenderable
{
    void Render(ref RenderBuffer image);
}
