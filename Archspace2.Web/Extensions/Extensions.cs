using System.Drawing;

namespace Archspace2.Web
{
    public static class Extensions
    {
        public static string ToHtml(this Color tColor)
        {
            return $"#{tColor.R.ToString("X2")}{tColor.G.ToString("X2")}{tColor.B.ToString("X2")}";
        }
    }
}
