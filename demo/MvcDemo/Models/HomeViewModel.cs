using Pmad.Milsymbol.AspNetCore.Orbat;

namespace MvcDemo.Models
{
    public class HomeViewModel
    {

        public required IOrbatUnit RootUnit { get; set; }

        public string? Symbol { get; set; }
    }
}
