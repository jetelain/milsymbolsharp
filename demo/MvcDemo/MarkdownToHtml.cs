using Markdig;
using Pmad.Milsymbol.Markdig;

namespace MvcDemoBS5
{
    public static class MarkdownToHtml
    {
        public static string ConvertMarkdownToHtml (string markdown)
        {
            var pipeline = new Markdig.MarkdownPipelineBuilder()
                .UseAdvancedExtensions()
                .UseMilsymbol() // Use the custom Milsymbol extension
                .Build();
            return Markdig.Markdown.ToHtml(markdown, pipeline);
        }
    }
}
