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

                Console.Write(tag.ToString() + ": ");

                profile.SetValue(tag, text);
                image.SetProfile(profile);

                image.Write(path);
            }

        }

        static public IReadOnlyList<IExifValue> GetExifText(string path)
        {
            MagickImage image = new MagickImage(path);
            var profile = image.GetExifProfile();
        return profile.Values;
        }
    }
                          
