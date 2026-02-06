using Markdig.Syntax.Inlines;
using Pmad.Milsymbol.Icons;

namespace Pmad.Milsymbol.Markdig;

/// <summary>
/// Represents a military symbol inline element in the Markdown document.
/// This is the AST (Abstract Syntax Tree) node created by <see cref="MilsymbolInlineParser"/> 
/// and rendered by <see cref="MilsymbolInlineRenderer"/>.
/// </summary>
public sealed class MilsymbolInline : LeafInline
{
    /// <summary>
    /// Gets or sets the SIDC (Symbol Identification Coding) code that identifies the military symbol.
    /// Must be a 20 or 30 digit numeric string.
    /// </summary>
    public string Sidc { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the rendering options for the military symbol, such as size, stroke width, and text annotations.
    /// </summary>
    public SymbolIconOptions Options { get; set; } = new();
}
