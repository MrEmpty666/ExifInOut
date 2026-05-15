using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using ImageMagick;
using static ExifInput;

namespace Dekstop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string? filePath;
        public MainWindow()
        {
            InitializeComponent();
        }


        private void DropArea_DragEnter(object sender, DragEventArgs e)
        {

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {          
                e.Effects = DragDropEffects.Copy;
                DropArea.Background = Brushes.LightGreen;
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }
        }

        private void DropArea_Drop(object sender, DragEventArgs e)
        {
            DropArea.Background = Brushes.LightGray;

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {

                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                filePath = files[0];

                

                string ext = System.IO.Path.GetExtension(filePath).ToLower();
                if (!(ext == ".png" || ext == ".jpg" || ext == ".jpeg"))
                {
                    MessageBox.Show("Файл не является картинкой!");
                    filePath = null;
                }
                else
                { 
                    //Image img = new Image();
                    //img.Source = new BitmapImage(new Uri(filePath));
                    //img.Stretch = Stretch.Uniform;
                    //DropArea.Child = img;

                    var mapping = new Dictionary<ExifTag, TextBox>
                    {
                        { ExifTag.Artist, ArtistTextBox },
                        { ExifTag.Copyright, CopyrightTextBox },
                        { ExifTag.ImageDescription, ImageDescriptionTextBox },
                        { ExifTag.UserComment, CommentTextBox }
                    };
                    var ExifData = GetExifText(filePath);
                    foreach (var item in ExifData)
                    {
                        var tag = item.Tag;
                        var text = item.ToString();
                        if (!mapping.ContainsKey(tag)) continue;
                        var tb = mapping[tag];
                        tb.Text = text;
                    }

                }
            }
        }
        private void ReplaceExif_Click(object sender, RoutedEventArgs e)
        {
            if (filePath == null)
            {
                MessageBox.Show("Картнка не вставлена");

            }
            else
            {
                var mapping = new Dictionary<ExifTag, TextBox>
                    {
                        { ExifTag.Artist, ArtistTextBox },
                        { ExifTag.Copyright, CopyrightTextBox },
                        { ExifTag.ImageDescription, ImageDescriptionTextBox },
                        { ExifTag.UserComment, CommentTextBox }
                    };
                var ExifData = GetExifText(filePath);
                foreach (var item in ExifData)
                {
                    var tag = item.Tag;
                    if (!mapping.ContainsKey(tag)) continue;
                    var tb = mapping[tag];
                    Replace(tag, filePath, tb.Text);
                    tb.Text = item.ToString();
                }
            }
        }
    }
}