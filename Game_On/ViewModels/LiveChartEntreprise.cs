using Game_On.Models;
using LiveCharts;
using LiveCharts.Wpf;
using System.Windows.Media;

namespace Game_On.ViewModels
{
    public class LiveChartEntreprise
    {
        public SeriesCollection Series { get; set; }
        public List<string> EntrepriseNames { get; set; }

        public LiveChartEntreprise(List<Entreprise> entreprises)
        {
            // Top 5 par score décroissant
            var top5 = entreprises
                .OrderByDescending(e => e.Score)
                .Take(5)
                .ToList();

            EntrepriseNames = top5
                .Select(e => e.NomEntreprise)
                .ToList();

            // Dégradé de couleurs pour les barres
            var gradient = new LinearGradientBrush
            {
                StartPoint = new System.Windows.Point(0, 1),
                EndPoint = new System.Windows.Point(0, 0)
            };
            gradient.GradientStops.Add(new GradientStop(Color.FromRgb(63, 81, 181), 0));   // bleu
            gradient.GradientStops.Add(new GradientStop(Color.FromRgb(30, 136, 229), 1));  // bleu clair

            Series = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Score moyen",
                    Values = new ChartValues<double>(top5.Select(e => (double)e.Score)),
                    DataLabels = true,
                    LabelPoint = point => point.Y.ToString("N0"),
                    Fill = gradient
                }
            };
        }
    }
}
