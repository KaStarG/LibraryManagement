using LibraryManagement.Data;
using LibraryManagement.Models;
using System.Linq;
using System.Windows;

namespace LibraryManagement.Views
{
    public partial class BooksWindow : Window
    {
        private AppDbContext db;

        public BooksWindow()
        {
            InitializeComponent();

            db = new AppDbContext();

            LoadBooks();
        }

        private void LoadBooks()
        {
            try
            {
                BooksGrid.ItemsSource = db.Books.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddBookWindow window = new AddBookWindow();

            window.ShowDialog();

            LoadBooks();
        }
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedBook = BooksGrid.SelectedItem as Books;

            if (selectedBook == null)
            {
                MessageBox.Show("Выберите книгу");
                return;
            }

            var window = new EditBookWindow(selectedBook);
            if (window.ShowDialog() == true)
            {
                LoadBooks();
            }
        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedBook = BooksGrid.SelectedItem as Books;

            if (selectedBook == null)
            {
                MessageBox.Show("Выберите книгу");
                return;
            }

            db.Books.Remove(selectedBook);
            db.SaveChanges();

            LoadBooks();
        }
    }
}