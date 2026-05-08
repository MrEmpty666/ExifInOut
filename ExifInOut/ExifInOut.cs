using ImageMagick;
using static System.Net.Mime.MediaTypeNames;


    public class ExifInput
    {
        static public void Replace(ExifTag<string> tag, string path, string text)
        {
            MagickImage image = new MagickImage(path);
            using (image)
            {
                var profile = image.GetExifProfile() ?? new ExifProfile();

                // Устанавливаем текстовые значения
                Console.Write(tag.ToString() + ": ");
                profile.SetValue(tag, text);
                // Добавляем профиль к изображению
                image.SetProfile(profile);
                // Сохраняем результат
                image.Write(path);
            }

        }

        static public string GetExifText(string path)
        {
            MagickImage image = new MagickImage(path);
            var profile = image.GetExifProfile();
            if (profile == null)
            {
                return "Exif данных нет";
            }
        return string.Join("\n",profile.Values);
        }
    }
                          
