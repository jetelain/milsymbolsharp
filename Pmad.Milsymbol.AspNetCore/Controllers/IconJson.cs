using Pmad.Milsymbol.App6d;

namespace Pmad.Milsymbol.AspNetCore.Controllers
{
    public class IconJson
    {
        public IconJson(string code, string[] entity)
        {
            Code = code;
            Entity = entity;
        }

        public IconJson(App6dMainIcon icon)
        {
            Code = icon.Code;
            if (!string.IsNullOrEmpty(icon.EntitySubtype))
            {
                Entity = [icon.Entity!, icon.EntityType!, icon.EntitySubtype];
            }
            else if (!string.IsNullOrEmpty(icon.EntityType))
            {
                Entity = [icon.Entity!, icon.EntityType];
            }
            else
            {
                Entity = [icon.Entity!];
            }
        }

        public string Code { get; }

        public string[] Entity { get; }
    }
}