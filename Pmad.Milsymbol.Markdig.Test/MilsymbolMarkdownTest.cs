using Markdig;
using Pmad.Milsymbol.Markdig;

namespace Pmad.Milsymbol.Markdig.Test;

public class MilsymbolMarkdownTest
{
    [Fact]
    public void MilsymbolInline_ShouldRenderSvg()
    {
        var pipeline = new MarkdownPipelineBuilder()
            .UseMilsymbol()
            .Build();

        var markdown = "This is a symbol: :ms[10031000131211050000]: inline.";
        var html = Markdown.ToHtml(markdown, pipeline);

        Assert.Contains("<svg", html);
        Assert.Contains("xmlns=\"http://www.w3.org/2000/svg\"", html);
    }

    [Fact]
    public void MilsymbolInline_MultipleSymbols_ShouldRenderAll()
    {
        var pipeline = new MarkdownPipelineBuilder()
            .UseMilsymbol()
            .Build();

        var markdown = "Symbol 1: :ms[10031000131211050000]: and Symbol 2: :ms[10061000161211000000]:.";
        var html = Markdown.ToHtml(markdown, pipeline);

        var svgCount = System.Text.RegularExpressions.Regex.Matches(html, "<svg").Count;
        Assert.Equal(2, svgCount);
    }

    [Fact]
    public void MilsymbolInline_InvalidSidc_ShouldNotRender()
    {
        var pipeline = new MarkdownPipelineBuilder()
            .UseMilsymbol()
            .Build();

        var markdown = "Invalid: :ms[invalid]: symbol.";
        var html = Markdown.ToHtml(markdown, pipeline);

        Assert.DoesNotContain("<svg", html);
        Assert.Contains(":ms[invalid]:", html);
    }

    [Fact]
    public void MilsymbolInline_InParagraph_ShouldRender()
    {
        var pipeline = new MarkdownPipelineBuilder()
            .UseMilsymbol()
            .Build();

        var markdown = @"# Military Symbols

This document shows military symbols:

- Friend Infantry: :ms[10031000131211050000]:
- Enemy Armor: :ms[10061000161211000000]:

End of document.";
        
        var html = Markdown.ToHtml(markdown, pipeline);

        Assert.Contains("<svg", html);
        Assert.Contains("<h1>Military Symbols</h1>", html);
        Assert.Contains("<li>Friend Infantry:", html);
    }

    [Fact]
    public void MilsymbolInline_WithSize_ShouldRenderWithSize()
    {
        var pipeline = new MarkdownPipelineBuilder()
            .UseMilsymbol()
            .Build();

        var markdown = "Large symbol: :ms[10031000131211050000, size=50]:";
        var html = Markdown.ToHtml(markdown, pipeline);

        Assert.Contains("<svg", html);
        // The size affects the width/height attributes of the SVG
    }

    [Fact]
    public void MilsymbolInline_WithUniqueDesignation_ShouldRenderWithText()
    {
        var pipeline = new MarkdownPipelineBuilder()
            .UseMilsymbol()
            .Build();

        var markdown = "Unit: :ms[10031000131211050000, uniqueDesignation=\"A-1\"]:";
        var html = Markdown.ToHtml(markdown, pipeline);

        Assert.Contains("<svg", html);
        Assert.Contains("A-1", html);
    }

    [Fact]
    public void MilsymbolInline_WithMultipleOptions_ShouldRenderWithAllOptions()
    {
        var pipeline = new MarkdownPipelineBuilder()
            .UseMilsymbol()
            .Build();

        var markdown = "Unit: :ms[10031000131211050000, size=40, uniqueDesignation=\"B-2\", higherFormation=\"2BDE\"]:";
        var html = Markdown.ToHtml(markdown, pipeline);

        Assert.Contains("<svg", html);
        Assert.Contains("B-2", html);
        Assert.Contains("2BDE", html);
    }

    [Fact]
    public void MilsymbolInline_WithDirection_ShouldRenderWithDirection()
    {
        var pipeline = new MarkdownPipelineBuilder()
            .UseMilsymbol()
            .Build();

        var markdown = "Moving unit: :ms[10031000131211050000, direction=45]:";
        var html = Markdown.ToHtml(markdown, pipeline);

        Assert.Contains("<svg", html);
    }

    [Fact]
    public void MilsymbolInline_WithAdditionalInformation_ShouldRenderWithText()
    {
        var pipeline = new MarkdownPipelineBuilder()
            .UseMilsymbol()
            .Build();

        var markdown = "Info: :ms[10031000131211050000, additionalInformation=\"Ready\"]:";
        var html = Markdown.ToHtml(markdown, pipeline);

        Assert.Contains("<svg", html);
        Assert.Contains("Ready", html);
    }

    [Fact]
    public void MilsymbolInline_NoOptions_ShouldRenderNormally()
    {
        var pipeline = new MarkdownPipelineBuilder()
            .UseMilsymbol()
            .Build();

        var markdown = "No options: :ms[10031000131211050000]:";
        var html = Markdown.ToHtml(markdown, pipeline);

        Assert.Contains("<svg", html);
    }

    [Fact]
    public void MilsymbolInline_OptionsWithQuotedStrings_ShouldParse()
    {
        var pipeline = new MarkdownPipelineBuilder()
            .UseMilsymbol()
            .Build();

        var markdown = "Quoted: :ms[10031000131211050000, uniqueDesignation=\"Alpha Company\"]:";
        var html = Markdown.ToHtml(markdown, pipeline);

        Assert.Contains("<svg", html);
        Assert.Contains("Alpha Company", html);
    }

    [Fact]
    public void MilsymbolInline_OptionsWithCommaInQuotes_ShouldParse()
    {
        var pipeline = new MarkdownPipelineBuilder()
            .UseMilsymbol()
            .Build();

        var markdown = "Comma: :ms[10031000131211050000, uniqueDesignation=\"A-1, Ready\"]:";
        var html = Markdown.ToHtml(markdown, pipeline);

        Assert.Contains("<svg", html);
        Assert.Contains("A-1, Ready", html);
    }

    [Fact]
    public void MilsymbolInline_InvalidOptionsFormat_ShouldNotRender()
    {
        var pipeline = new MarkdownPipelineBuilder()
            .UseMilsymbol()
            .Build();

        var markdown = "Invalid: :ms[10031000131211050000, invalid:";
        var html = Markdown.ToHtml(markdown, pipeline);

        Assert.DoesNotContain("<svg", html);
    }

    [Fact]
    public void MilsymbolInline_MixedValidAndInvalidOptions_ShouldRenderWithValidOnes()
    {
        var pipeline = new MarkdownPipelineBuilder()
            .UseMilsymbol()
            .Build();

        var markdown = "Mixed: :ms[10031000131211050000, size=40, invalidOption=test, uniqueDesignation=\"A-1\"]:";
        var html = Markdown.ToHtml(markdown, pipeline);

        Assert.Contains("<svg", html);
        Assert.Contains("A-1", html);
    }

    [Fact]
    public void MilsymbolInline_ShortOptionNames_ShouldRender()
    {
        var pipeline = new MarkdownPipelineBuilder()
            .UseMilsymbol()
            .Build();

        var markdown = "Short: :ms[10031000131211050000, s=50, ud=\"A-1\"]:";
        var html = Markdown.ToHtml(markdown, pipeline);

        Assert.Contains("<svg", html);
        Assert.Contains("A-1", html);
    }
}
