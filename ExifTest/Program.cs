using ImageMagick;
using static System.Net.Mime.MediaTypeNames;
using static ExifInput;

internal class Program
{
    private static void Main(string[] args)
    {
        var path = Console.ReadLine();
        GetExifText(path);
        Console.ReadLine();
        Replace(ExifTag.ImageDescription, path, "");
        Console.ReadLine();
        GetExifText(path);
        Console.ReadLine();
    }
}