using LibraryManagement.Data;
using LibraryManagement.Models;
using System;
using System.Windows;

namespace LibraryManagement.Views
{
    public partial class EditBookWindow : Window
    {
        private readonly AppDbContext db;
        private readonly Books book;

        public EditBookWindow(Books selectedBook)
        {
            InitializeComponent();

            db = new AppDbContext();

            
            book = selectedBook;

            
            TitleBox.Text = book.Title;
            AuthorBox.Text = book.Author;
            YearBox.Text = book.Year.ToString();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TitleBox.Text))
            {
                MessageBox.Show("Введите название");
                return;
            }

            if (!int.TryParse(YearBox.Text, out int year))
            {
                MessageBox.Show("Год должен быть числом");
                return;
            }

      
            book.Title = TitleBox.Text;
            book.Author = AuthorBox.Text;
            book.Year = year;

            db.Books.Update(book);
            db.SaveChanges();

            DialogResult = true;
            Close();
        }
    }
}