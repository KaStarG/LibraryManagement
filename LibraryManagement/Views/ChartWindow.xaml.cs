using LibraryManagement.Data;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using System.Linq;
using System.Windows;

namespace LibraryManagement.Views
{
    public partial class ChartWindow : Window
    {
        private AppDbContext db;

        public ChartWindow()
        {
            InitializeComponent();
            db = new AppDbContext();
            LoadChart();
        }

        private void LoadChart()
        {
            var data = db.Books
                .GroupBy(x => x.PublishYear)
                .Select(x => new
                {
                    Year = x.Key,
                    Count = x.Count()
                })
                .OrderBy(x => x.Year)
                .ToList();

          
            BooksChart.Series = new ISeries[]
            {
                new ColumnSeries<int>
                {
                    Values = data.Select(x => x.Count).ToArray(),
                    Name = "Количество книг"
                }
            };

            BooksChart.XAxes = new Axis[]
            {
                new Axis
                {
                    Labels = data.Select(x => x.Year.ToString()).ToArray(),
                    Name = "Год публикации"
                }
            };

            BooksChart.YAxes = new Axis[]
            {
                new Axis
                {
                    Name = "Количество книг"
                }
            };
        }
    }
}