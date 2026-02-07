using Markdig;
using Markdig.Syntax;

namespace Pmad.Milsymbol.Markdig.Test;

public class MilsymbolInlineParserTest
{
    [Fact]
    public void Parser_ValidSimpleSyntax_ShouldParse()
    {
        var pipeline = new MarkdownPipelineBuilder()
            .UseMilsymbol()
            .Build();

        var markdown = ":ms[10031000131211050000]:";
        var document = Markdown.Parse(markdown, pipeline);
        
        var inline = FindMilsymbolInline(document);
        Assert.NotNull(inline);
        Assert.Equal("10031000131211050000", inline!.Sidc);
    }

    [Fact]
    public void Parser_ValidSidc30Digits_ShouldParse()
    {
        var pipeline = new MarkdownPipelineBuilder()
            .UseMilsymbol()
            .Build();

        var markdown = ":ms[100310001312110500001234567890]:";
        var document = Markdown.Parse(markdown, pipeline);
        
        var inline = FindMilsymbolInline(document);
        Assert.NotNull(inline);
        Assert.Equal("100310001312110500001234567890", inline!.Sidc);
    }

    [Fact]
    public void Parser_InvalidSidcLength_ShouldNotParse()
    {
        var pipeline = new MarkdownPipelineBuilder()
            .UseMilsymbol()
            .Build();

        var markdown = ":ms[123]:";
        var document = Markdown.Parse(markdown, pipeline);
        
        var inline = FindMilsymbolInline(document);
        Assert.Null(inline);
    }

    [Fact]
    public void Parser_InvalidSidcCharacters_ShouldNotParse()
    {
        var pipeline = new MarkdownPipelineBuilder()
            .UseMilsymbol()
            .Build();

        var markdown = ":ms[1003100013121105000X]:";
        var document = Markdown.Parse(markdown, pipeline);
        
        var inline = FindMilsymbolInline(document);
        Assert.Null(inline);
    }

    [Fact]
    public void Parser_MissingClosingBracket_ShouldNotParse()
    {
        var pipeline = new MarkdownPipelineBuilder()
            .UseMilsymbol()
            .Build();

        var markdown = ":ms[10031000131211050000:";
        var document = Markdown.Parse(markdown, pipeline);
        
        var inline = FindMilsymbolInline(document);
        Assert.Null(inline);
    }

    [Fact]
    public void Parser_MissingClosingColon_ShouldNotParse()
    {
        var pipeline = new MarkdownPipelineBuilder()
            .UseMilsymbol()
            .Build();

        var markdown = ":ms[10031000131211050000]";
        var document = Markdown.Parse(markdown, pipeline);
        
        var inline = FindMilsymbolInline(document);
        Assert.Null(inline);
    }

    [Fact]
    public void Parser_NotStartingWithColon_ShouldNotParse()
    {
        var pipeline = new MarkdownPipelineBuilder()
            .UseMilsymbol()
            .Build();

        var markdown = "ms[10031000131211050000]:";
        var document = Markdown.Parse(markdown, pipeline);
        
        var inline = FindMilsymbolInline(document);
        Assert.Null(inline);
    }

    [Fact]
    public void Parser_WrongPrefix_ShouldNotParse()
    {
        var pipeline = new MarkdownPipelineBuilder()
            .UseMilsymbol()
            .Build();

        var markdown = ":symbol[10031000131211050000]:";
        var document = Markdown.Parse(markdown, pipeline);
        
        var inline = FindMilsymbolInline(document);
        Assert.Null(inline);
    }

    [Fact]
    public void Parser_EmptySidc_ShouldNotParse()
    {
        var pipeline = new MarkdownPipelineBuilder()
            .UseMilsymbol()
            .Build();

        var markdown = ":ms[]:";
        var document = Markdown.Parse(markdown, pipeline);
        
        var inline = FindMilsymbolInline(document);
        Assert.Null(inline);
    }

    [Fact]
    public void Parser_NoOptions_ShouldParse()
    {
        var pipeline = new MarkdownPipelineBuilder()
            .UseMilsymbol()
            .Build();

        var markdown = ":ms[10031000131211050000]:";
        var document = Markdown.Parse(markdown, pipeline);
        
        var inline = FindMilsymbolInline(document);
        Assert.NotNull(inline);
        Assert.Equal("10031000131211050000", inline!.Sidc);
    }

    [Fact]
    public void Parser_MissingClosingBracket_WithOptions_ShouldNotParse()
    {
        var pipeline = new MarkdownPipelineBuilder()
            .UseMilsymbol()
            .Build();

        var markdown = ":ms[10031000131211050000, size=50:";
        var document = Markdown.Parse(markdown, pipeline);
        
        var inline = FindMilsymbolInline(document);
        Assert.Null(inline);
    }

    [Fact]
    public void Parser_SingleNumericOption_ShouldParse()
    {
        var pipeline = new MarkdownPipelineBuilder()
            .UseMilsymbol()
            .Build();

        var markdown = ":ms[10031000131211050000, size=50]:";
        var document = Markdown.Parse(markdown, pipeline);
        
        var inline = FindMilsymbolInline(document);
        Assert.NotNull(inline);
        Assert.Equal(50, inline!.Options.Size);
    }

    [Fact]
    public void Parser_MultipleNumericOptions_ShouldParseAll()
    {
        var pipeline = new MarkdownPipelineBuilder()
            .UseMilsymbol()
            .Build();

        var markdown = ":ms[10031000131211050000, size=50, direction=45, strokeWidth=2.5]:";
        var document = Markdown.Parse(markdown, pipeline);
        
        var inline = FindMilsymbolInline(document);
        Assert.NotNull(inline);
        Assert.Equal(50, inline!.Options.Size);
        Assert.Equal(45, inline.Options.Direction);
        Assert.Equal(2.5, inline.Options.StrokeWidth);
    }

    [Fact]
    public void Parser_StringOption_ShouldParse()
    {
        var pipeline = new MarkdownPipelineBuilder()
            .UseMilsymbol()
            .Build();

        var markdown = ":ms[10031000131211050000, uniqueDesignation=\"A-1\"]:";
        var document = Markdown.Parse(markdown, pipeline);
        
        var inline = FindMilsymbolInline(document);
        Assert.NotNull(inline);
        Assert.Equal("A-1", inline!.Options.UniqueDesignation);
    }

    [Fact]
    public void Parser_MultipleStringOptions_ShouldParseAll()
    {
        var pipeline = new MarkdownPipelineBuilder()
            .UseMilsymbol()
            .Build();

        var markdown = ":ms[10031000131211050000, uniqueDesignation=\"Unit 1\", higherFormation=\"2BDE\", additionalInformation=\"Ready\"]:";
        var document = Markdown.Parse(markdown, pipeline);
        
        var inline = FindMilsymbolInline(document);
        Assert.NotNull(inline);
        Assert.Equal("Unit 1", inline!.Options.UniqueDesignation);
        Assert.Equal("2BDE", inline.Options.HigherFormation);
        Assert.Equal("Ready", inline.Options.AdditionalInformation);
    }

    [Fact]
    public void Parser_MixedOptions_ShouldParseAll()
    {
        var pipeline = new MarkdownPipelineBuilder()
            .UseMilsymbol()
            .Build();

        var markdown = ":ms[10031000131211050000, size=40, uniqueDesignation=\"A-1\"]:";
        var document = Markdown.Parse(markdown, pipeline);
        
        var inline = FindMilsymbolInline(document);
        Assert.NotNull(inline);
        Assert.Equal(40, inline!.Options.Size);
        Assert.Equal("A-1", inline.Options.UniqueDesignation);
    }

    [Fact]
    public void Parser_OptionsWithCommaInQuotes_ShouldParse()
    {
        var pipeline = new MarkdownPipelineBuilder()
            .UseMilsymbol()
            .Build();

        var markdown = ":ms[10031000131211050000, uniqueDesignation=\"A-1, Ready\"]:";
        var document = Markdown.Parse(markdown, pipeline);
        
        var inline = FindMilsymbolInline(document);
        Assert.NotNull(inline);
        Assert.Equal("A-1, Ready", inline!.Options.UniqueDesignation);
    }

    [Fact]
    public void Parser_AllSupportedOptions_ShouldParseAll()
    {
        var pipeline = new MarkdownPipelineBuilder()
            .UseMilsymbol()
            .Build();

        var markdown = ":ms[10031000131211050000, size=50, strokeWidth=2, outlineWidth=3, uniqueDesignation=\"A\", additionalInformation=\"B\", higherFormation=\"C\", commonIdentifier=\"D\", reinforcedReduced=\"E\", direction=90]:";
        var document = Markdown.Parse(markdown, pipeline);
        
        var inline = FindMilsymbolInline(document);
        Assert.NotNull(inline);
        Assert.Equal(50, inline!.Options.Size);
        Assert.Equal(2, inline.Options.StrokeWidth);
        Assert.Equal(3, inline.Options.OutlineWidth);
        Assert.Equal("A", inline.Options.UniqueDesignation);
        Assert.Equal("B", inline.Options.AdditionalInformation);
        Assert.Equal("C", inline.Options.HigherFormation);
        Assert.Equal("D", inline.Options.CommonIdentifier);
        Assert.Equal("E", inline.Options.ReinforcedReduced);
        Assert.Equal(90, inline.Options.Direction);
    }

    [Fact]
    public void Parser_ShortOptionNames_ShouldParse()
    {
        var pipeline = new MarkdownPipelineBuilder()
            .UseMilsymbol()
            .Build();

        var markdown = ":ms[10031000131211050000, s=50, sw=2, ow=3, ud=\"A\", ai=\"B\", hf=\"C\", ci=\"D\", rr=\"E\", d=90]:";
        var document = Markdown.Parse(markdown, pipeline);
        
        var inline = FindMilsymbolInline(document);
        Assert.NotNull(inline);
        Assert.Equal(50, inline!.Options.Size);
        Assert.Equal(2, inline.Options.StrokeWidth);
        Assert.Equal(3, inline.Options.OutlineWidth);
        Assert.Equal("A", inline.Options.UniqueDesignation);
        Assert.Equal("B", inline.Options.AdditionalInformation);
        Assert.Equal("C", inline.Options.HigherFormation);
        Assert.Equal("D", inline.Options.CommonIdentifier);
        Assert.Equal("E", inline.Options.ReinforcedReduced);
        Assert.Equal(90, inline.Options.Direction);
    }

    [Fact]
    public void Parser_InvalidOptionName_ShouldIgnore()
    {
        var pipeline = new MarkdownPipelineBuilder()
            .UseMilsymbol()
            .Build();

        var markdown = ":ms[10031000131211050000, invalidOption=test, size=40]:";
        var document = Markdown.Parse(markdown, pipeline);
        
        var inline = FindMilsymbolInline(document);
        Assert.NotNull(inline);
        Assert.Equal(40, inline!.Options.Size);
    }

    [Fact]
    public void Parser_InvalidNumericValue_ShouldIgnore()
    {
        var pipeline = new MarkdownPipelineBuilder()
            .UseMilsymbol()
            .Build();

        var markdown = ":ms[10031000131211050000, size=invalid, uniqueDesignation=\"A-1\"]:";
        var document = Markdown.Parse(markdown, pipeline);
        
        var inline = FindMilsymbolInline(document);
        Assert.NotNull(inline);
        Assert.Equal("A-1", inline!.Options.UniqueDesignation);
    }

    [Fact]
    public void Parser_CaseInsensitiveOptions_ShouldParse()
    {
        var pipeline = new MarkdownPipelineBuilder()
            .UseMilsymbol()
            .Build();

        var markdown = ":ms[10031000131211050000, SIZE=50, UniqueDesignation=\"A\"]:";
        var document = Markdown.Parse(markdown, pipeline);
        
        var inline = FindMilsymbolInline(document);
        Assert.NotNull(inline);
        Assert.Equal(50, inline!.Options.Size);
        Assert.Equal("A", inline.Options.UniqueDesignation);
    }

    [Fact]
    public void Parser_WhitespaceAroundEquals_ShouldParse()
    {
        var pipeline = new MarkdownPipelineBuilder()
            .UseMilsymbol()
            .Build();

        var markdown = ":ms[10031000131211050000, size = 50 , uniqueDesignation = \"A-1\" ]:";
        var document = Markdown.Parse(markdown, pipeline);
        
        var inline = FindMilsymbolInline(document);
        Assert.NotNull(inline);
        Assert.Equal(50, inline!.Options.Size);
        Assert.Equal("A-1", inline.Options.UniqueDesignation);
    }

    [Fact]
    public void Parser_InSentence_ShouldParse()
    {
        var pipeline = new MarkdownPipelineBuilder()
            .UseMilsymbol()
            .Build();

        var markdown = "This is a symbol :ms[10031000131211050000]: in a sentence.";
        var document = Markdown.Parse(markdown, pipeline);
        
        var inline = FindMilsymbolInline(document);
        Assert.NotNull(inline);
        Assert.Equal("10031000131211050000", inline!.Sidc);
    }

    [Fact]
    public void Parser_MultipleSymbols_ShouldParseAll()
    {
        var pipeline = new MarkdownPipelineBuilder()
            .UseMilsymbol()
            .Build();

        var markdown = "Symbol 1: :ms[10031000131211050000]: and Symbol 2: :ms[10061000161211000000]:.";
        var document = Markdown.Parse(markdown, pipeline);
        
        var inlines = FindAllMilsymbolInlines(document);
        Assert.Equal(2, inlines.Count);
        Assert.Equal("10031000131211050000", inlines[0].Sidc);
        Assert.Equal("10061000161211000000", inlines[1].Sidc);
    }

    [Fact]
    public void OpeningCharacters_ShouldContainColon()
    {
        var parser = new MilsymbolInlineParser(new Icons.SymbolIconOptions());
        
        Assert.Contains(':', parser.OpeningCharacters!);
    }

    [Fact]
    public void Parser_NoOptionsSpecified_ShouldUseDefaultOptions()
    {
        var defaultOptions = new Icons.SymbolIconOptions
        {
            Size = 100,
            StrokeWidth = 5,
            Direction = 180,
            UniqueDesignation = "DEFAULT"
        };

        var pipeline = new MarkdownPipelineBuilder()
            .UseMilsymbol(defaultOptions: defaultOptions)
            .Build();

        var markdown = ":ms[10031000131211050000]:";
        var document = Markdown.Parse(markdown, pipeline);
        
        var inline = FindMilsymbolInline(document);
        Assert.NotNull(inline);
        Assert.Equal(100, inline!.Options.Size);
        Assert.Equal(5, inline.Options.StrokeWidth);
        Assert.Equal(180, inline.Options.Direction);
        Assert.Equal("DEFAULT", inline.Options.UniqueDesignation);
    }

    [Fact]
    public void Parser_EmptyOptionsSpecified_ShouldUseDefaultOptions()
    {
        var defaultOptions = new Icons.SymbolIconOptions
        {
            Size = 75,
            OutlineWidth = 2.5,
            HigherFormation = "DEFAULT-HF"
        };

        var pipeline = new MarkdownPipelineBuilder()
            .UseMilsymbol(defaultOptions: defaultOptions)
            .Build();

        var markdown = ":ms[10031000131211050000]:";
        var document = Markdown.Parse(markdown, pipeline);
        
        var inline = FindMilsymbolInline(document);
        Assert.NotNull(inline);
        Assert.Equal(75, inline!.Options.Size);
        Assert.Equal(2.5, inline.Options.OutlineWidth);
        Assert.Equal("DEFAULT-HF", inline.Options.HigherFormation);
    }

    [Fact]
    public void Parser_PartialOptionsSpecified_ShouldMergeWithDefaults()
    {
        var defaultOptions = new Icons.SymbolIconOptions
        {
            Size = 80,
            StrokeWidth = 3,
            Direction = 45,
            UniqueDesignation = "DEFAULT-UD",
            HigherFormation = "DEFAULT-HF"
        };

        var pipeline = new MarkdownPipelineBuilder()
            .UseMilsymbol(defaultOptions: defaultOptions)
            .Build();

        var markdown = ":ms[10031000131211050000, size=120, uniqueDesignation=\"CUSTOM\"]:";
        var document = Markdown.Parse(markdown, pipeline);
        
        var inline = FindMilsymbolInline(document);
        Assert.NotNull(inline);
        Assert.Equal(120, inline!.Options.Size);
        Assert.Equal(3, inline.Options.StrokeWidth);
        Assert.Equal(45, inline.Options.Direction);
        Assert.Equal("CUSTOM", inline.Options.UniqueDesignation);
        Assert.Equal("DEFAULT-HF", inline.Options.HigherFormation);
    }

    [Fact]
    public void Parser_AllOptionsOverridden_ShouldNotUseDefaults()
    {
        var defaultOptions = new Icons.SymbolIconOptions
        {
            Size = 100,
            StrokeWidth = 5,
            OutlineWidth = 3,
            Direction = 90,
            UniqueDesignation = "DEFAULT-UD",
            AdditionalInformation = "DEFAULT-AI",
            HigherFormation = "DEFAULT-HF",
            CommonIdentifier = "DEFAULT-CI",
            ReinforcedReduced = "DEFAULT-RR"
        };

        var pipeline = new MarkdownPipelineBuilder()
            .UseMilsymbol(defaultOptions: defaultOptions)
            .Build();

        var markdown = ":ms[10031000131211050000, size=50, strokeWidth=2, outlineWidth=1, direction=180, uniqueDesignation=\"A\", additionalInformation=\"B\", higherFormation=\"C\", commonIdentifier=\"D\", reinforcedReduced=\"E\"]:";
        var document = Markdown.Parse(markdown, pipeline);
        
        var inline = FindMilsymbolInline(document);
        Assert.NotNull(inline);
        Assert.Equal(50, inline!.Options.Size);
        Assert.Equal(2, inline.Options.StrokeWidth);
        Assert.Equal(1, inline.Options.OutlineWidth);
        Assert.Equal(180, inline.Options.Direction);
        Assert.Equal("A", inline.Options.UniqueDesignation);
        Assert.Equal("B", inline.Options.AdditionalInformation);
        Assert.Equal("C", inline.Options.HigherFormation);
        Assert.Equal("D", inline.Options.CommonIdentifier);
        Assert.Equal("E", inline.Options.ReinforcedReduced);
    }

    [Fact]
    public void Parser_AllOptionsOverriddenWithShortNames_ShouldNotUseDefaults()
    {
        var defaultOptions = new Icons.SymbolIconOptions
        {
            Size = 100,
            StrokeWidth = 5,
            OutlineWidth = 3,
            Direction = 90,
            UniqueDesignation = "DEFAULT-UD",
            AdditionalInformation = "DEFAULT-AI",
            HigherFormation = "DEFAULT-HF",
            CommonIdentifier = "DEFAULT-CI",
            ReinforcedReduced = "DEFAULT-RR"
        };

        var pipeline = new MarkdownPipelineBuilder()
            .UseMilsymbol(defaultOptions: defaultOptions)
            .Build();

        var markdown = ":ms[10031000131211050000, s=50, sw=2, ow=1, d=180, ud=\"A\", ai=\"B\", hf=\"C\", ci=\"D\", rr=\"E\"]:";
        var document = Markdown.Parse(markdown, pipeline);
        
        var inline = FindMilsymbolInline(document);
        Assert.NotNull(inline);
        Assert.Equal(50, inline!.Options.Size);
        Assert.Equal(2, inline.Options.StrokeWidth);
        Assert.Equal(1, inline.Options.OutlineWidth);
        Assert.Equal(180, inline.Options.Direction);
        Assert.Equal("A", inline.Options.UniqueDesignation);
        Assert.Equal("B", inline.Options.AdditionalInformation);
        Assert.Equal("C", inline.Options.HigherFormation);
        Assert.Equal("D", inline.Options.CommonIdentifier);
        Assert.Equal("E", inline.Options.ReinforcedReduced);
    }

    [Fact]
    public void Parser_MultipleSymbolsWithDefaults_ShouldAllUseDefaults()
    {
        var defaultOptions = new Icons.SymbolIconOptions
        {
            Size = 60,
            Direction = 270
        };

        var pipeline = new MarkdownPipelineBuilder()
            .UseMilsymbol(defaultOptions: defaultOptions)
            .Build();

        var markdown = "Symbol 1: :ms[10031000131211050000]: and Symbol 2: :ms[10061000161211000000, uniqueDesignation=\"B\"]:.";
        var document = Markdown.Parse(markdown, pipeline);
        
        var inlines = FindAllMilsymbolInlines(document);
        Assert.Equal(2, inlines.Count);
        Assert.Equal(60, inlines[0].Options.Size);
        Assert.Equal(270, inlines[0].Options.Direction);
        Assert.Equal(60, inlines[1].Options.Size);
        Assert.Equal(270, inlines[1].Options.Direction);
        Assert.Equal("B", inlines[1].Options.UniqueDesignation);
    }

    private static MilsymbolInline? FindMilsymbolInline(MarkdownDocument document)
    {
        return document.Descendants<MilsymbolInline>().FirstOrDefault();
    }

    private static List<MilsymbolInline> FindAllMilsymbolInlines(MarkdownDocument document)
    {
        return document.Descendants<MilsymbolInline>().ToList();
    }
}
