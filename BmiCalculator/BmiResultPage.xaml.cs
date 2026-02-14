namespace BmiCalculator;

[QueryProperty(nameof(Height), "height")]
[QueryProperty(nameof(Weight), "weight")]
[QueryProperty(nameof(Gender), "gender")]
public partial class BmiResultPage : ContentPage
{
    private double _bmi;
    private string _healthStatus = string.Empty;
    private string _gender = string.Empty;

    //  Query Properties 

    public string Height
    {
        set
        {
            if (double.TryParse(value, out double h))
                _height = h;
        }
    }
    private double _height;

    public string Weight
    {
        set
        {
            if (double.TryParse(value, out double w))
                _weight = w;
        }
    }
    private double _weight;

    public string Gender
    {
        set
        {
            _gender = value ?? "Male";
        }
    }

    public BmiResultPage()
    {
        InitializeComponent();
    }

    //  Page Appearing 

    protected override void OnAppearing()
    {
        base.OnAppearing();
        CalculateAndDisplay();
    }

    private void CalculateAndDisplay()
    {
        // BMI = (weight in lbs × 703) / (height in inches)2
        if (_height > 0)
        {
            _bmi = (_weight * 703) / (_height * _height);
            _bmi = Math.Round(_bmi, 1);
        }

        _healthStatus = GetHealthStatus(_bmi, _gender);

        // Update UI
        BmiValueLabel.Text = _bmi.ToString("F1");
        GenderLabel.Text = _gender;
        HealthStatusLabel.Text = _healthStatus;

        // Color code the status
        HealthStatusLabel.TextColor = _healthStatus switch
        {
            "Underweight" => Color.FromArgb("#FFA726"),
            "Normal weight" => Color.FromArgb("#66BB6A"),
            "Overweight" => Color.FromArgb("#EF5350"),
            "Obese" => Color.FromArgb("#B71C1C"),
            _ => Color.FromArgb("#333333")
        };
    }

    //  BMI Category gender specific 

    private static string GetHealthStatus(double bmi, string gender)
    {
        if (gender == "Male")
        {
            if (bmi < 18.5) return "Underweight";
            if (bmi < 25) return "Normal weight";
            if (bmi < 30) return "Overweight";
            return "Obese";
        }
        else
        {
            if (bmi < 18) return "Underweight";
            if (bmi < 24) return "Normal weight";
            if (bmi < 29) return "Overweight";
            return "Obese";
        }
    }

    //  Navigation 

    private async void OnViewRecommendationsClicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(
            $"{nameof(RecommendationsPage)}?bmi={_bmi}&healthStatus={_healthStatus}&gender={_gender}");
    }

    private async void OnBackToInputClicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}