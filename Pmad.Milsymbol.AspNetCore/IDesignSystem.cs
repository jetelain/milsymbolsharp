using Microsoft.AspNetCore.Html;

namespace Pmad.Milsymbol.AspNetCore
{
    public interface IDesignSystem
    {
        string FormRow { get; }
        
        string ControlLabel { get; }
        
        string? InputGroupAppend { get; }

        IHtmlContent CreateToggleButton(IHtmlContent content, string type, string? id, string? name, string? value);
    }
}
