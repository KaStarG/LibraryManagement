using LibraryManagement.Data;
using LibraryManagement.Models;
using System;
using System.Linq;
using System.Windows;

namespace LibraryManagement.Views
{
    public partial class QueueWindow : Window
    {
        private AppDbContext db;

        public QueueWindow()
        {
            InitializeComponent();

            db = new AppDbContext();

            LoadData();
        }

        private void LoadData()
        {
            ReadersBox.ItemsSource = db.Readers.ToList();
            ReadersBox.DisplayMemberPath = "FullName";

            BooksBox.ItemsSource = db.Books.ToList();
            BooksBox.DisplayMemberPath = "Title";

            LoadQueue();
        }

        private void LoadQueue()
        {
            var queue = db.QueueItems
                .OrderBy(x => x.QueueDate)
                .ToList()
                .Select(x => new QueueDisplayItem
                {
                    Id = x.Id,
                    Reader = db.Readers.FirstOrDefault(r => r.Id == x.ReaderId)?.FullName,
                    Book = db.Books.FirstOrDefault(b => b.Id == x.BookId)?.Title,
                    QueueDate = x.QueueDate
                })
                .ToList();

            QueueGrid.ItemsSource = queue;
        }

        private void AddQueueButton_Click(object sender, RoutedEventArgs e)
        {
            Readers reader = ReadersBox.SelectedItem as Readers;
            Books book = BooksBox.SelectedItem as Books;

            if (reader == null || book == null)
            {
                MessageBox.Show("Выберите читателя и книгу");
                return;
            }

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

            LoadQueue();

            MessageBox.Show("Читатель добавлен в очередь");
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            LoadQueue();
        }

        private void DeleteQueueButton_Click(object sender, RoutedEventArgs e)
        {
            QueueDisplayItem selectedItem = QueueGrid.SelectedItem as QueueDisplayItem;

            if (selectedItem == null)
            {
                MessageBox.Show("Выберите запись очереди");
                return;
            }

            QueueItem queueItem = db.QueueItems
                .FirstOrDefault(x => x.Id == selectedItem.Id);

            if (queueItem == null)
            {
                MessageBox.Show("Запись не найдена");
                return;
            }

            db.QueueItems.Remove(queueItem);

            db.SaveChanges();

            LoadQueue();

            MessageBox.Show("Запись удалена из очереди");
        }

        private class QueueDisplayItem
        {
            public int Id { get; set; }

            public string Reader { get; set; }

            public string Book { get; set; }

            public DateTime QueueDate { get; set; }
        }
    }
}