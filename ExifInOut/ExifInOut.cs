using ImageMagick;
using static System.Net.Mime.MediaTypeNames;


    public class ExifInput
    {
        static public void Repalce(ExifTag<string> tag, string path)
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

        static public void WriteExif(string path)
        {
            MagickImage image = new MagickImage(path);
            var profile = image.GetExifProfile();
            if (profile == null)
            {
                Console.WriteLine("Exif данных нет");
                return;
            }
            foreach (var value in profile.Values)
                Console.Write(value.ToString());
        }
    }

