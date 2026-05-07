using ImageMagick;
using static System.Net.Mime.MediaTypeNames;

namespace ExifInput
{
    public class ExifInput
    {


        static void Repalce (ExifTag<string> tag, string path)
        {
            MagickImage image = new MagickImage(path);
            using (image)
            {
                var profile = image.GetExifProfile() ?? new ExifProfile();

                // Устанавливаем текстовые значения
                Console.Write(tag.ToString() + ": ");
                var str = Console.ReadLine().ToString();
                profile.SetValue(tag, str);
                // Добавляем профиль к изображению
                image.SetProfile(profile);
                // Сохраняем результат
                image.Write(path);
            }

        }
    }
}
