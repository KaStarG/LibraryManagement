using LibraryManagement.Data;
using LibraryManagement.Data;
using LibraryManagement.Models;
using System.Reflection.PortableExecutable;
using System.Windows;
using System.Windows;

namespace LibraryManagement.Views
{
    public partial class AddReaderWindow : Window
    {
        private AppDbContext db;

        public AddReaderWindow()
        {
            InitializeComponent();

            db = new AppDbContext();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(FullNameTextBox.Text))
                {
                    MessageBox.Show("Введите ФИО");
                    return;
                }

                Readers reader = new Readers
                {
                    FullName = FullNameTextBox.Text,
                    Phone = PhoneTextBox.Text,
                    Email = "",
                    RegisteredAt = DateTime.Now
                };

                db.Readers.Add(reader);
                db.SaveChanges();

                DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}