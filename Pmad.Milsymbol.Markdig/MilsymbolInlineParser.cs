using System.Globalization;
using Markdig.Helpers;
using Markdig.Parsers;
using Pmad.Milsymbol.Icons;

namespace Pmad.Milsymbol.Markdig;

/// <summary>
/// Parses military symbol inline syntax in Markdown documents.
/// Recognizes the syntax: :ms[SIDC, options]:
/// where SIDC is a 20 or 30 digit code and options are optional key-value pairs.
/// </summary>
/// <example>
/// Example usage in Markdown:
/// <code>
/// :ms[10031000001211000000]:
/// :ms[10031000001211000000, size=50, uniquedesignation="A-1"]:
/// :ms[10031000001211000000, s=50, ud="A-1"]:
/// </code>
/// </example>
public sealed class MilsymbolInlineParser : InlineParser
{
    private readonly SymbolIconOptions defaultOptions;

    /// <summary>
    /// Initializes a new instance of the <see cref="MilsymbolInlineParser"/> class.
    /// </summary>
    /// <param name="defaultOptions">The default options to use for symbols when no options are specified.</param>
    public MilsymbolInlineParser(SymbolIconOptions defaultOptions)
    {
        this.defaultOptions = defaultOptions;
        OpeningCharacters = new[] { ':' };
    }

    /// <summary>
    /// Attempts to match and parse a military symbol inline element from the current position in the Markdown text.
    /// Expected format: :ms[SIDC, options]:
    /// </summary>
    /// <param name="processor">The inline processor managing the parsing state.</param>
    /// <param name="slice">The current slice of text being parsed.</param>
    /// <returns>True if a valid military symbol syntax was found and parsed; otherwise, false.</returns>
    public override bool Match(InlineProcessor processor, ref StringSlice slice)
    {
        var startPosition = slice.Start;
        var c = slice.CurrentChar;

        if (c != ':')
        {
            return false;
        }

        // Save the start position
        var start = slice.Start;
        
        // Move past the opening ':'
        c = slice.NextChar();

        // Check if it starts with 'ms['
        var match = "ms[";
        for (int i = 0; i < match.Length; i++)
        {
            if (slice.CurrentChar != match[i])
            {
                slice.Start = start;
                return false;
            }
            slice.NextChar();
        }

        // Find the closing ']'
        var contentStart = slice.Start;
        var contentEnd = -1;

        // Need to respect quoted strings when finding the closing bracket
        var inQuotes = false;
        while (slice.CurrentChar != '\0')
        {
            if (slice.CurrentChar == '"')
            {
                inQuotes = !inQuotes;
            }
            else if (slice.CurrentChar == ']' && !inQuotes)
            {
                contentEnd = slice.Start;
                break;
            }
            slice.NextChar();
        }

        if (contentEnd == -1)
        {
            // No closing ']' found
            slice.Start = start;
            return false;
        }

        // Extract content (SIDC and possibly options)
        var content = slice.Text.Substring(contentStart, contentEnd - contentStart);
        
        // Split content to get SIDC and options
        var (sidc, optionsText) = SplitContentToSidcAndOptions(content);
        
        // Validate SIDC format
        if (!IsValidSidc(sidc))
        {
            slice.Start = start;
            return false;
        }

        // Move past ']'
        slice.NextChar();
        
        // Parse options if present
        var options = string.IsNullOrWhiteSpace(optionsText) ? defaultOptions : ParseOptions(optionsText);
        
        // Expect closing ':'
        if (slice.CurrentChar != ':')
        {
            slice.Start = start;
            return false;
        }
        
        var inline = new MilsymbolInline
        {
            Sidc = sidc,
            Options = options,
            Span = new global::Markdig.Syntax.SourceSpan(startPosition, slice.Start)
        };
        
        processor.Inline = inline;
        slice.NextChar(); // Move past the closing ':'
        return true;
    }

    /// <summary>
    /// Splits the content into SIDC and options text.
    /// The first token (before the first comma outside quotes) is the SIDC.
    /// Everything after is options.
    /// </summary>
    /// <param name="content">The content to split.</param>
    /// <returns>A tuple of SIDC and options text.</returns>
    private static (string sidc, string optionsText) SplitContentToSidcAndOptions(string content)
    {
        var inQuotes = false;
        for (int i = 0; i < content.Length; i++)
        {
            if (content[i] == '"')
            {
                inQuotes = !inQuotes;
            }
            else if (content[i] == ',' && !inQuotes)
            {
                // Found the first comma - SIDC is before, options are after
                var sidc = content.Substring(0, i).Trim();
                var options = content.Substring(i + 1).Trim();
                return (sidc, options);
            }
        }
        
        // No comma found - entire content is SIDC
        return (content.Trim(), string.Empty);
    }
    
    /// <summary>
    /// Validates whether a string is a valid SIDC (Symbol Identification Coding) code.
    /// </summary>
    /// <param name="sidc">The string to validate.</param>
    /// <returns>True if the SIDC is valid (20 or 30 numeric digits); otherwise, false.</returns>
    private static bool IsValidSidc(string sidc)
    {
        if (string.IsNullOrWhiteSpace(sidc))
        {
            return false;
        }

        // SIDC should be 20 or 30 digits
        if (sidc.Length != 20 && sidc.Length != 30)
        {
            return false;
        }

        // All characters should be digits
        foreach (var c in sidc)
        {
            if (!char.IsDigit(c))
            {
                return false;
            }
        }

        return true;
    }
    
    /// <summary>
    /// Parses the options string from the military symbol syntax.
    /// Options are comma-separated key-value pairs (key=value).
    /// String values can be enclosed in double quotes.
    /// </summary>
    /// <param name="optionsText">The options text to parse (without the surrounding braces).</param>
    /// <returns>A <see cref="SymbolIconOptions"/> instance with parsed values merged with default options.</returns>
    private SymbolIconOptions ParseOptions(string optionsText)
    {
        if (string.IsNullOrWhiteSpace(optionsText))
        {
            return defaultOptions;
        }

        var options = new SymbolIconOptions()
        {
            // Copy default options
            Size = defaultOptions.Size,
            StrokeWidth = defaultOptions.StrokeWidth,
            OutlineWidth = defaultOptions.OutlineWidth,
            UniqueDesignation = defaultOptions.UniqueDesignation,
            AdditionalInformation = defaultOptions.AdditionalInformation,
            HigherFormation = defaultOptions.HigherFormation,
            CommonIdentifier = defaultOptions.CommonIdentifier,
            ReinforcedReduced = defaultOptions.ReinforcedReduced,
            Direction = defaultOptions.Direction,
        };

        // Split by comma, respecting quoted strings
        var pairs = SplitOptions(optionsText);
        
        foreach (var pair in pairs)
        {
            var equalIndex = pair.IndexOf('=');
            if (equalIndex <= 0)
            {
                continue;
            }
            
            var key = pair.Substring(0, equalIndex).Trim();
            var value = pair.Substring(equalIndex + 1).Trim();
            
            // Remove quotes if present
            if (value.Length >= 2 && value[0] == '"' && value[value.Length - 1] == '"')
            {
                value = value.Substring(1, value.Length - 2);
            }
            
            switch (key.ToLowerInvariant())
            {
                case "size":
                case "s":
                    if (double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out var size))
                    {
                        options.Size = size;
                    }
                    break;
                    
                case "strokewidth":
                case "sw":
                    if (double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out var strokeWidth))
                    {
                        options.StrokeWidth = strokeWidth;
                    }
                    break;
                    
                case "outlinewidth":
                case "ow":
                    if (double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out var outlineWidth))
                    {
                        options.OutlineWidth = outlineWidth;
                    }
                    break;
                    
                case "uniquedesignation":
                case "ud":
                    options.UniqueDesignation = value;
                    break;
                    
                case "additionalinformation":
                case "ai":
                    options.AdditionalInformation = value;
                    break;
                    
                case "higherformation":
                case "hf":
                    options.HigherFormation = value;
                    break;
                    
                case "commonidentifier":
                case "ci":
                    options.CommonIdentifier = value;
                    break;
                    
                case "reinforcedreduced":
                case "rr":
                    options.ReinforcedReduced = value;
                    break;
                    
                case "direction":
                case "d":
                    if (double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out var direction))
                    {
                        options.Direction = direction;
                    }
                    break;
            }
        }
        
        return options;
    }
    
    /// <summary>
    /// Splits the options text into individual key-value pair strings.
    /// Respects quoted strings so that commas within quotes are not treated as separators.
    /// </summary>
    /// <param name="text">The options text to split.</param>
    /// <returns>A list of individual option strings (key=value format).</returns>
    private static List<string> SplitOptions(string text)
    {
        var result = new List<string>();
        var current = new System.Text.StringBuilder();
        var inQuotes = false;
        
        foreach (var c in text)
        {
            if (c == '"')
            {
                inQuotes = !inQuotes;
                current.Append(c);
            }
            else if (c == ',' && !inQuotes)
            {
                if (current.Length > 0)
                {
                    result.Add(current.ToString());
                    current.Clear();
                }
            }
            else
            {
                current.Append(c);
            }
        }
        
        if (current.Length > 0)
        {
            result.Add(current.ToString());
        }
        
        return result;
    }
}
