using BookShop.Model;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace BookShop.View
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow Instance { get; set; }


        public ObservableCollection<Manufacturer> Manufacturers
        {
            get { return (ObservableCollection<Manufacturer>)GetValue(ManufacturersProperty); }
            set { SetValue(ManufacturersProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Manufacturers.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ManufacturersProperty =
            DependencyProperty.Register("Manufacturers", typeof(ObservableCollection<Manufacturer>), typeof(MainWindow));



        public ObservableCollection<Book> Books
        {
            get { return (ObservableCollection<Book>)GetValue(MyPropertyBooks); }
            set { SetValue(MyPropertyBooks, value); }
        }

        public static readonly DependencyProperty MyPropertyBooks =
            DependencyProperty.Register("Books", typeof(ObservableCollection<Book>), typeof(MainWindow));

        public MainWindow()
        {
            InitializeComponent();
            UpdateCollectionBooks();

            Instance = this;
        }

        void UpdateCollectionBooks()
        {
            var position = ListBooks.SelectedIndex == -1 ? 0 : ListBooks.SelectedIndex;
            Books = new ObservableCollection<Book>(App.db.Book);
            Manufacturers = new ObservableCollection<Manufacturer>(App.db.Manufacturer);
            ListBooks.SelectedIndex = position;
        }

        private void ChangeWidthColumnContentControl(double minWidht, double widht)
        {
            ColumnContentControl.MinWidth = minWidht;
            ColumnContentControl.Width = new GridLength(widht);
        }

        private static void SaveChangesDataBase()
        {
            App.db.SaveChanges();
        }

        private void ImageAwesome_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Books.Add(new Book
            {
                Name = "Новая книга"
            });
            ListBooks.SelectedIndex = ListBooks.Items.Count - 1;
            ListBooks.ScrollIntoView(ListBooks.SelectedItem);
            AcceptImage.Visibility = Visibility.Visible;
        }


        private void SaveNewBook()
        {
            AcceptImage.Visibility = Visibility.Collapsed;
            App.db.Book.Add(Books.LastOrDefault());
            App.db.SaveChanges();
        }


        private void ListBooks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Подтверждение создания элемента, если требуется
            if (AcceptImage.Visibility == Visibility.Visible)
            {
                var answerMessageBox = MessageBox.Show("Потдвердить изменения", "Подтверждение", MessageBoxButton.YesNo);
                if (answerMessageBox == MessageBoxResult.Yes)
                {
                    SaveNewBook();
                    UpdateCollectionBooks();
                }
                else if (answerMessageBox == MessageBoxResult.No)
                {
                    Books.RemoveAt(ListBooks.Items.Count - 1);
                    AcceptImage.Visibility = Visibility.Collapsed;
                }
            }

            if (ManufacturersComboBox == null || ListBooks == null) return;

            App.Book = ListBooks.SelectedItem as Book;

            if (ColumnContentControl.Width != new GridLength(0)) return;
            ChangeWidthColumnContentControl(200, 280);
        }
        private void ImageAwesome_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ChangeWidthColumnContentControl(0, 0);
        }

        private void AcceptImage_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            SaveNewBook();
            UpdateCollectionBooks();
        }

        private void Window_Closed(object sender, System.EventArgs e)
        {
            SaveChangesDataBase();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            App.Book.ManufacturerId = (ManufacturersComboBox.SelectedItem as Manufacturer).Id;
            SaveChangesDataBase();

            if (AcceptImage.Visibility == Visibility.Visible) return;
            UpdateCollectionBooks();
        }

        private void PictureBook_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog()
            {
                Filter = "All Files (*.*)|*.*"
            };

            if (openFile.ShowDialog().GetValueOrDefault())
            {
                BitmapFrame.Create(new MemoryStream(File.ReadAllBytes(openFile.FileName)), BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
                App.Book.Photo = File.ReadAllBytes(openFile.FileName);
            }

            if (AcceptImage.Visibility == Visibility.Visible) return;
            UpdateCollectionBooks();
        }

        private void ImageAwesome_MouseUp_1(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if(ListBooks.SelectedItem == null)
            {
                MessageBox.Show("Нет выбранного элемента");
                return;
            }

            var answerMessageBox = MessageBox.Show($"Подтвердить удаление {(ListBooks.SelectedItem as Book).Name}", "Подтверждение", MessageBoxButton.YesNo);
            if (answerMessageBox == MessageBoxResult.Yes)
            {
                try
                {
                    App.db.Book.Remove(ListBooks.SelectedItem as Book);
                }
                catch
                {
                    AcceptImage.Visibility = Visibility.Collapsed;
                    Books.RemoveAt(ListBooks.Items.Count - 1);
                }
                SaveChangesDataBase();
                UpdateCollectionBooks();
            }
        }


    }
}
