using LibraryManagement.Data;
using LibraryManagement.Models;
using System;
using System.Linq;
using System.Windows;

namespace LibraryManagement.Views
{
    public partial class ReturnBookWindow : Window
    {
        private AppDbContext db;

        public ReturnBookWindow()
        {
            InitializeComponent();

            db = new AppDbContext();

            LoadLoans();
        }

        private void LoadLoans()
        {
            try
            {
                LoansGrid.ItemsSource = db.Loans
                    .Where(x => x.ActualReturnDate == null)
                    .ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Load error: " + ex.Message);
            }
        }

        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            Loan loan = LoansGrid.SelectedItem as Loan;

            if (loan == null)
            {
                MessageBox.Show("Выберите выдачу");
                return;
            }

            try
            {
                loan.ActualReturnDate = DateTime.Now;

                var copy = db.BookCopies
                    .FirstOrDefault(x => x.Id == loan.BookCopyId);

                if (copy == null)
                {
                    MessageBox.Show("Экземпляр книги не найден");
                    return;
                }

                copy.Status = "Free";

                var firstQueueItem = db.QueueItems
                    .Where(x => x.BookId == copy.BookId)
                    .OrderBy(x => x.QueueDate)
                    .FirstOrDefault();

                db.SaveChanges();

                LoadLoans();

                if (firstQueueItem != null)
                {
                    Readers reader = db.Readers
                        .FirstOrDefault(x => x.Id == firstQueueItem.ReaderId);

                    Books book = db.Books
                        .FirstOrDefault(x => x.Id == firstQueueItem.BookId);

                    string readerName = reader != null
                        ? reader.FullName
                        : "неизвестный читатель";

                    string bookTitle = book != null
                        ? book.Title
                        : "неизвестная книга";

                    MessageBox.Show(
                        $"Книга возвращена.\n\nНа книгу \"{bookTitle}\" есть очередь.\nПервый в очереди: {readerName}");
                }
                else
                {
                    MessageBox.Show("Книга возвращена");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Return error: " + ex.Message);
            }
        }
    }
}