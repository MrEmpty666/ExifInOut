using ImageMagick;
using static System.Net.Mime.MediaTypeNames;



using (MagickImage image = new MagickImage("C:\\Users\\Empty\\source\\repos\\ExifTest\\ExifTest\\1.png"))
{
    var profile = image.GetExifProfile() ?? new ExifProfile();

    // Устанавливаем текстовые значения
    profile.SetValue(ExifTag.Copyright, Console.ReadLine().ToString());
    profile.SetValue(ExifTag.Artist, Console.ReadLine().ToString());
    profile.SetValue(ExifTag.ImageDescription, Console.ReadLine().ToString());
    // Добавляем профиль к изображению
    image.SetProfile(profile);
    // Сохраняем результат
    image.Write("C:\\Users\\Empty\\source\\repos\\ExifTest\\ExifTest\\out.png");
}

MagickImage image1 = new MagickImage("C:\\Users\\Empty\\source\\repos\\ExifTest\\ExifTest\\out.png");
var govno = image1.GetExifProfile();

foreach (var a in govno.Values)
{
    Console.WriteLine(a.ToString());
}