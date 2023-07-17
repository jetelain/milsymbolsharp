
namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {

            var image = Image.Load<Rgba32>(@"c:\temp\Sans titre.png");

            var stats = new Dictionary<Rgba32, int>();

            for(var x = 0; x < image.Width; x++)
            {
                for(var y=0; y < image.Height; y++)
                {
                    var px = image[x, y];

                    if (px.R < 128) px.R = 0;
                    else /*if (px.R > 192)*/ px.R = 255;

                    if (px.G < 128) px.G = 0;
                    else /*if(px.G > 192)*/ px.G = 255;

                    if (px.B < 128) px.B = 0;
                    else /*if(px.B > 192)*/ px.B = 255;

                    if (px.A < 64) px.A = 0;
                    else if (px.A > 192) px.A = 255;
                    else px.A = 128;

                    if (stats.TryGetValue(px, out var value))
                    {
                        stats[px] = value+1;
                    }
                    else
                    {
                        stats[px] = 1;
                    }

                }
            }

            foreach(var key in stats.OrderBy(s => s.Value))
            {
                Console.WriteLine($"{key.Key.ToHex()} {key.Value}");
            }


            Console.WriteLine("Hello, World!");
        }

    }
}