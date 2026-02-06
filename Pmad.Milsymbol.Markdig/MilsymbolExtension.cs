using Markdig;
using Markdig.Renderers;
using Pmad.Milsymbol.Icons;

namespace Pmad.Milsymbol.Markdig;

/// <summary>
/// A Markdig extension that adds support for military symbol rendering in Markdown.
/// This extension registers the inline parser and HTML renderer needed to process military symbol syntax.
/// </summary>
public sealed class MilsymbolExtension : IMarkdownExtension
{
    private readonly ISymbolIconGenerator generator;
    private readonly SymbolIconOptions defaultOptions;

    /// <summary>
    /// Initializes a new instance of the <see cref="MilsymbolExtension"/> class.
    /// </summary>
    /// <param name="generator">The symbol icon generator used to create military symbol SVG markup.</param>
    /// <param name="defaultOptions">The default options applied to all military symbols unless overridden.</param>
    public MilsymbolExtension(ISymbolIconGenerator generator, SymbolIconOptions defaultOptions)
    {
        this.generator = generator;
        this.defaultOptions = defaultOptions;
    }

    /// <summary>
    /// Sets up the extension by registering the military symbol inline parser with the pipeline.
    /// </summary>
    /// <param name="pipeline">The Markdown pipeline builder to configure.</param>
    public void Setup(MarkdownPipelineBuilder pipeline)
    {
        if (!pipeline.InlineParsers.Contains<MilsymbolInlineParser>())
        {
            pipeline.InlineParsers.Add(new MilsymbolInlineParser(defaultOptions));
        }
    }

    /// <summary>
    /// Sets up the extension by registering the military symbol HTML renderer with the pipeline.
    /// </summary>
    /// <param name="pipeline">The Markdown pipeline.</param>
    /// <param name="renderer">The renderer to configure. Only HTML renderers are supported.</param>
    public void Setup(MarkdownPipeline pipeline, IMarkdownRenderer renderer)
    {
        if (renderer is HtmlRenderer htmlRenderer)
        {
            if (!htmlRenderer.ObjectRenderers.Contains<MilsymbolInlineRenderer>())
            {
                htmlRenderer.ObjectRenderers.Add(new MilsymbolInlineRenderer(generator));
            }
        }
    }
}
