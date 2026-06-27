using LibraryManagement.Data;
using LibraryManagement.Models;
using System;
using System.Windows;

namespace LibraryManagement.Views
{
    public partial class AddBookWindow : Window
    {
        private AppDbContext db;

        public AddBookWindow()
        {
            InitializeComponent();

            db = new AppDbContext();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Books book = new Books();

                book.Title = TitleBox.Text;
                book.PublishYear = int.Parse(YearBox.Text);
                book.Pages = int.Parse(PagesBox.Text);
                book.IsReadingRoomOnly = ReadingRoomBox.IsChecked ?? false;

                book.AuthorId = 1;
                book.PublisherId = 1;

                db.Books.Add(book);

                db.SaveChanges();

                MessageBox.Show("Книга успешно добавлена");

                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}