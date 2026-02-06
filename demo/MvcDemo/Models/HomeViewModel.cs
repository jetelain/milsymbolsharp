using Pmad.Milsymbol.AspNetCore.Orbat;

namespace MvcDemo.Models
{
    public class HomeViewModel
    {

        public required IOrbatUnit RootUnit { get; set; }

        public string? Symbol1 { get; set; }
        public string? Symbol2 { get; set; }
        public string? Symbol3 { get; set; }

        public string Markdown { get; set; } = string.Empty;
    }
}
