namespace BmiCalculator;

[QueryProperty(nameof(Bmi), "bmi")]
[QueryProperty(nameof(HealthStatus), "healthStatus")]
[QueryProperty(nameof(Gender), "gender")]
public partial class RecommendationsPage : ContentPage
{
    private double _bmi;
    private string _healthStatus = string.Empty;
    private string _gender = string.Empty;

    //  Query Prop

    public string Bmi
    {
        set
        {
            if (double.TryParse(value, out double b))
                _bmi = b;
        }
    }

    public string HealthStatus
    {
        set => _healthStatus = Uri.UnescapeDataString(value ?? string.Empty);
    }

    public string Gender
    {
        set => _gender = value ?? "Male";
    }

    public RecommendationsPage()
    {
        InitializeComponent();
    }

    // Page Appearing

    protected override void OnAppearing()
    {
        base.OnAppearing();
        DisplayRecommendations();
    }

    private void DisplayRecommendations()
    {
        // Summary line
        SummaryLabel.Text = $"{_gender}  •  BMI: {_bmi:F1}  •  {_healthStatus}";

        // Get the three recommendations for this category
        string[] recs = GetRecommendations(_healthStatus);
        Rec1Label.Text = recs[0];
        Rec2Label.Text = recs[1];
        Rec3Label.Text = recs[2];
    }

    //  Personalized Recommendations 

    private static string[] GetRecommendations(string healthStatus)
    {
        return healthStatus switch
        {
            "Underweight" => new[]
            {
                "Increase calorie intake with nutrient-dense foods such as nuts, avocados, and whole grains.",
                "Incorporate strength training exercises to build muscle mass.",
                "Consult a healthcare provider to rule out underlying health conditions."
            },

            "Normal weight" => new[]
            {
                "Maintain a balanced diet rich in fruits, vegetables, lean proteins, and whole grains.",
                "Stay physically active with at least 150 minutes of moderate exercise per week.",
                "Continue monitoring your weight and health regularly."
            },

            "Overweight" => new[]
            {
                "Reduce processed foods and focus on portion control.",
                "Engage in regular aerobic exercises (e.g., jogging, swimming) and strength training.",
                "Drink plenty of water and track your progress."
            },

            "Obese" => new[]
            {
                "Seek guidance from a healthcare professional for a personalized weight management plan.",
                "Focus on a calorie-controlled diet with high fiber and lean protein.",
                "Start with low-impact exercises such as walking or cycling and gradually increase intensity."
            },

            _ => new[]
            {
                "Consult a healthcare provider for personalized advice.",
                "Maintain a balanced and healthy lifestyle.",
                "Stay active and monitor your health regularly."
            }
        };
    }

    //  Navigation

    private async void OnBackToResultClicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }

    private async void OnBackToInputClicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("../..");
    }
}