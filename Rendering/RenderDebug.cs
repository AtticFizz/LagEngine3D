namespace Rendering;

public static class RenderDebug
{
    public delegate void TextChangedDelegate(string text);
    public static TextChangedDelegate? OnTextChanged;

    private static string _text = "";

    public static void Clear()
    {
        _text = "";
        OnTextChanged?.Invoke(_text);
    }

    public static void Log(params object[] args)
    {
        for (int i = 0; i < args.Length; i++)
            _text += args[i] + " ";
        OnTextChanged?.Invoke(_text);
    }

    public static void LogLine(params object[] args)
    {
        for (int i = 0; i < args.Length; i++)
            _text += args[i] + " ";
        _text += "\n";
        OnTextChanged?.Invoke(_text);
    }
}
