using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using LibraryManagement.Views;

namespace LibraryManagement
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BooksButton_Click(object sender, RoutedEventArgs e)
        {
            BooksWindow window = new BooksWindow();

            window.ShowDialog();
        }

        private void QueueButton_Click(object sender, RoutedEventArgs e)
        {
            QueueWindow window = new QueueWindow();

            window.ShowDialog();
        }

        private void ReadersButton_Click(object sender, RoutedEventArgs e)
        {
            ReadersWindow window = new ReadersWindow();

            window.ShowDialog();
        }

        private void IssueButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                IssueBookWindow window = new IssueBookWindow();
                window.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            ReturnBookWindow window = new ReturnBookWindow();

            new ReturnBookWindow().ShowDialog();

            window.ShowDialog();
        }

        private void ReportsButton_Click(object sender, RoutedEventArgs e)
        {
            ReportsWindow window = new ReportsWindow();

            window.ShowDialog();
        }
        private void ChartButton_Click(object sender, RoutedEventArgs e)
        {
            ChartWindow window = new ChartWindow();

            window.ShowDialog();
        }
    }
}