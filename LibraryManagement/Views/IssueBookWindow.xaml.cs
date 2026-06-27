using LibraryManagement.Data;
using LibraryManagement.Models;
using System;
using System.Linq;
using System.Windows;

namespace LibraryManagement.Views
{
    public partial class IssueBookWindow : Window
    {
        private AppDbContext db;

        public IssueBookWindow()
        {
            InitializeComponent();

            db = new AppDbContext();

            LoadData();
        }

        private void LoadData()
        {
            ReadersBox.ItemsSource = db.Readers.ToList();
            BooksBox.ItemsSource = db.Books.ToList();

            ReadersBox.DisplayMemberPath = "FullName";
            BooksBox.DisplayMemberPath = "Title";
        }

        private void IssueButton_Click(object sender, RoutedEventArgs e)
        {
            Readers reader = ReadersBox.SelectedItem as Readers;

            Books book = BooksBox.SelectedItem as Books;

            if (reader == null || book == null)
            {
                MessageBox.Show("Выберите читателя и книгу");
                return;
            }

            var copy = db.BookCopies
                .FirstOrDefault(x =>
                    x.BookId == book.Id &&
                    x.Status == "Free");

            if (copy == null)
            {
                MessageBoxResult result = MessageBox.Show(
                    "Свободных экземпляров нет. Поставить читателя в очередь?",
                    "Очередь",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    bool alreadyExists = db.QueueItems.Any(x =>
                        x.ReaderId == reader.Id &&
                        x.BookId == book.Id);

                    if (alreadyExists)
                    {
                        MessageBox.Show("Этот читатель уже находится в очереди на выбранную книгу");
                        return;
                    }

                    QueueItem queueItem = new QueueItem();

                    queueItem.ReaderId = reader.Id;
                    queueItem.BookId = book.Id;
                    queueItem.QueueDate = DateTime.Now;

                    db.QueueItems.Add(queueItem);

                    db.SaveChanges();

                    MessageBox.Show("Читатель добавлен в очередь");
                }

                return;
            }

            copy.Status = "Busy";

            Loan loan = new Loan();

            loan.ReaderId = reader.Id;
            loan.BookCopyId = copy.Id;

            loan.IssueDate = DateTime.Now;
            loan.ReturnDate = DateTime.Now.AddDays(14);

            db.Loans.Add(loan);

            QueueItem queueItemForReader = db.QueueItems.FirstOrDefault(x =>
            x.ReaderId == reader.Id &&
            x.BookId == book.Id);

            if (queueItemForReader != null)
            {
                db.QueueItems.Remove(queueItemForReader);
            }

            db.SaveChanges();

            MessageBox.Show("Книга успешно выдана");
        }
    }
}