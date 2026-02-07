using Markdig;
using Pmad.Milsymbol.Icons;

namespace Pmad.Milsymbol.Markdig;

/// <summary>
/// Extension methods for <see cref="MarkdownPipelineBuilder"/> to enable military symbol rendering.
/// </summary>
public static class MarkdownPipelineBuilderExtensions
{
    /// <summary>
    /// Adds military symbol support to the Markdown pipeline.
    /// This enables inline military symbols using the syntax: :milsymbol[SIDC]{options}:
    /// </summary>
    /// <param name="pipeline">The Markdown pipeline builder to extend.</param>
    /// <param name="generator">Optional symbol icon generator. If null, a default <see cref="SymbolIconGenerator"/> will be used.</param>
    /// <param name="defaultOptions">Optional default options for symbol rendering. If null, default <see cref="SymbolIconOptions"/> will be used.</param>
    /// <returns>The same pipeline builder instance for method chaining.</returns>
    /// <example>
    /// <code>
    /// var pipeline = new MarkdownPipelineBuilder()
    ///     .UseMilsymbol()
    ///     .Build();
    /// </code>
    /// </example>
    public static MarkdownPipelineBuilder UseMilsymbol(this MarkdownPipelineBuilder pipeline, ISymbolIconGenerator? generator = null, SymbolIconOptions? defaultOptions = null)
    {
        pipeline.Extensions.Add(new MilsymbolExtension(
            generator ?? new SymbolIconGenerator(),
            defaultOptions ?? new SymbolIconOptions() { Size = 30 }));
        return pipeline;
    }
}
