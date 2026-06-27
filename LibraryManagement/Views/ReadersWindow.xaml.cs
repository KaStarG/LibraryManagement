using LibraryManagement.Data;
using LibraryManagement.Models;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Windows;

namespace LibraryManagement.Views
{
    public partial class ReadersWindow : Window
    {
        private AppDbContext db;

        public ReadersWindow()
        {
            InitializeComponent();

            db = new AppDbContext();

            LoadReaders();
        }

        private void LoadReaders()
        {
            ReadersGrid.ItemsSource = db.Readers.ToList();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new AddReaderWindow();

            if (window.ShowDialog() == true)
            {
                LoadReaders();
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedReader = ReadersGrid.SelectedItem as Readers;

            if (selectedReader == null)
            {
                MessageBox.Show("Выберите читателя");
                return;
            }

            var window = new EditReaderWindow(selectedReader);

            if (window.ShowDialog() == true)
            {
                LoadReaders();
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var reader = ReadersGrid.SelectedItem as Readers;

            if (reader == null)
            {
                MessageBox.Show("Выберите читателя");
                return;
            }

            db.Readers.Remove(reader);
            db.SaveChanges();

            LoadReaders();
        }
    }
}