using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using System.Collections.Generic;
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
                    MessageBox.Show("Неправильный формат файла");
                    filePath = null;
                }
                else
                {
                    //Загрузить изображение и отобразить его
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(filePath, UriKind.Absolute);
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.EndInit();
                    ImageDisplay.Source = bitmap;
                    NoImageText.Visibility = Visibility.Hidden;

                    //Показать exif данные
                    LoadExifToTextBoxes(filePath);
                }
            }
        }

        private void ReplaceExif_Click(object sender, RoutedEventArgs e)
        {
            if (filePath == null)
            {
                MessageBox.Show("Файл не вставлен");
            }
            else
            {
                var mapping = new Dictionary<ExifTag, TextBox>
                {
                    { ExifTag.Artist, ArtistTextBox },
                    { ExifTag.Copyright, CopyrightTextBox },
                    { ExifTag.ImageDescription, ImageDescriptionTextBox },
                    { ExifTag.Model, ModelTextBox }
                };

                foreach (var item in mapping)
                {
                    var tag = item.Key;
                    var tb = item.Value;
                    Replace((ExifTag<string>)tag, filePath, tb.Text ?? string.Empty);
                }
                RefreshImage();
            }
        }

        private void RefreshImage()
        {
            if (filePath != null)
            {
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(filePath, UriKind.Absolute);
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.EndInit();
                bitmap.Freeze();
                ImageDisplay.Source = bitmap;

                LoadExifToTextBoxes(filePath);
            }
        }

        private void LoadExifToTextBoxes(string path)
        {
            var mapping = new Dictionary<ExifTag, TextBox>
            {
                { ExifTag.Artist, ArtistTextBox },
                { ExifTag.Copyright, CopyrightTextBox },
                { ExifTag.ImageDescription, ImageDescriptionTextBox },
                { ExifTag.Model, ModelTextBox }
            };

            foreach (var tb in mapping.Values)
            {
                tb.Text = string.Empty;
            }

            foreach (var item in GetExifText(path))
            {
                var tag = item.Tag;
                if (!mapping.ContainsKey(tag)) continue;
                var tb = mapping[tag];
                tb.Text = item.ToString() ?? string.Empty;
            }
        }
    }
}
