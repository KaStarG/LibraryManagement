using LibraryManagement.Data;
using System;
using System.Linq;
using System.Windows;

namespace LibraryManagement.Views
{
    public partial class ReportsWindow : Window
    {
        private AppDbContext db;

        public ReportsWindow()
        {
            InitializeComponent();

            db = new AppDbContext();

            LoadReport();
        }

        private void LoadReport()
        {
            int booksCount = db.Books.Count();

            int readersCount = db.Readers.Count();

            int issuedCount = db.Loans
                .Count(x => x.ActualReturnDate == null);

            int overdueCount = db.Loans
                .Count(x =>
                    x.ActualReturnDate == null &&
                    x.ReturnDate < DateTime.Now);

            BooksCountText.Text =
                $"Всего книг: {booksCount}";

            ReadersCountText.Text =
                $"Всего читателей: {readersCount}";

            IssuedCountText.Text =
                $"Сейчас выдано книг: {issuedCount}";

            OverdueCountText.Text =
                $"Просроченных выдач: {overdueCount}";
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            LoadReport();
        }
    }
}