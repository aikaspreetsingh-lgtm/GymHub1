using PdfSharpCore.Fonts;

public class FontResolver : IFontResolver
{
    private static readonly string _fontName = "Roboto#";

    public byte[] GetFont(string faceName)
    {
        var fontPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Fonts", "Roboto-Regular.ttf");

        if (!File.Exists(fontPath))
            throw new FileNotFoundException("Font file not found", fontPath);

        return File.ReadAllBytes(fontPath);
    }

    public FontResolverInfo ResolveTypeface(string familyName, bool isBold, bool isItalic)
    {
        return new FontResolverInfo(_fontName);
    }

   
    public string DefaultFontName => _fontName;
}
