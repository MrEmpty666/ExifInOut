using System.Windows;
using System.Windows.Media;
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
            // Проверяем, есть ли данные типа файлов
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {          
                e.Effects = DragDropEffects.Copy; // визуальный эффект
                DropArea.Background = Brushes.LightGreen; // визуальный отклик
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }
        }

        // Когда объект отпущен
        private void DropArea_Drop(object sender, DragEventArgs e)
        {
            DropArea.Background = Brushes.LightGray; // возвращаем фон

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                // Получаем массив путей к файлам
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                // Берем первый файл (можно обработать все)
                filePath = files[0];

                string ext = System.IO.Path.GetExtension(filePath).ToLower();
                if (!(ext == ".png" || ext == ".jpg" || ext == ".jpeg"))
                {
                    MessageBox.Show("Файл не является картинкой!");
                    filePath = null;
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
                string input = InputTextBox.Text;

                Replace(ExifTag.ImageDescription, filePath, input);
            }
        }

        private void ShowExif_Click(object sender, RoutedEventArgs e)
        {
            if (filePath == null)
            {
                MessageBox.Show("Картнка не вставлена");
            }
            else
            {
                OutputTextBlock.Text = GetExifText(filePath);
            }
        }
    }
}