namespace BmiCalculator
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            // Stack based nav
            Routing.RegisterRoute(nameof(BmiResultPage), typeof(BmiResultPage));
            Routing.RegisterRoute(nameof(RecommendationsPage), typeof(RecommendationsPage));
        }
    }
}
