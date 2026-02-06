using Markdig.Renderers;
using Markdig.Renderers.Html;
using Pmad.Milsymbol.Icons;

namespace Pmad.Milsymbol.Markdig;

/// <summary>
/// Renders <see cref="MilsymbolInline"/> elements to HTML by generating SVG military symbols.
/// This renderer is responsible for converting the parsed military symbol AST nodes into actual SVG output.
/// </summary>
public sealed class MilsymbolInlineRenderer : HtmlObjectRenderer<MilsymbolInline>
{
    private readonly ISymbolIconGenerator generator;

    /// <summary>
    /// Initializes a new instance of the <see cref="MilsymbolInlineRenderer"/> class.
    /// </summary>
    /// <param name="generator">The symbol icon generator used to create SVG military symbols.</param>
    public MilsymbolInlineRenderer(ISymbolIconGenerator generator)
    {
        this.generator = generator;
    }

    /// <summary>
    /// Renders the military symbol inline element to HTML.
    /// Generates the SVG representation of the symbol and writes it to the output.
    /// </summary>
    /// <param name="renderer">The HTML renderer to write output to.</param>
    /// <param name="obj">The military symbol inline element to render.</param>
    protected override void Write(HtmlRenderer renderer, MilsymbolInline obj)
    {
        var symbol = generator.Generate(obj.Sidc, obj.Options);
        renderer.Write(symbol.Svg);
    }
}
