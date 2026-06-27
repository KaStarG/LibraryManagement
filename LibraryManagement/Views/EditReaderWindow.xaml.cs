using LibraryManagement.Data;
using LibraryManagement.Models;
using System.Reflection.PortableExecutable;
using System.Windows;

namespace LibraryManagement.Views
{
    public partial class EditReaderWindow : Window
    {
        private AppDbContext db;
        private Readers reader;

        public EditReaderWindow(Readers reader)
        {
            InitializeComponent();

            db = new AppDbContext();

            this.reader = db.Readers.Find(reader.Id);

            FullNameTextBox.Text = this.reader.FullName;
            PhoneTextBox.Text = this.reader.Phone;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            reader.FullName = FullNameTextBox.Text;
            reader.Phone = PhoneTextBox.Text;

            db.SaveChanges();

            DialogResult = true;
        }
    }
}