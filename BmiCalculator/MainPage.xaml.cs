namespace BmiCalculator;

public partial class MainPage : ContentPage
{
    private string _selectedGender = "Male"; // Default gender, default is male

    public MainPage()
    {
        InitializeComponent();
        HighlightSelectedGender(); // Highlight "Male" on launch
    }


    // Gender Selection

    private void OnMaleTapped(object? sender, EventArgs e)
    {
        _selectedGender = "Male";
        HighlightSelectedGender();
    }

    private void OnFemaleTapped(object? sender, EventArgs e)
    {
        _selectedGender = "Female";
        HighlightSelectedGender();
    }

    
    private void HighlightSelectedGender()
    {
        if (_selectedGender == "Male")
        {
            // Highlight Male
            MaleFrame.BorderColor = Color.FromArgb("#42A5F5"); // blue border
            MaleFrame.BackgroundColor = Color.FromArgb("#E3F2FD"); // light blue bg
            MaleFrame.Opacity = 1.0;

            // Dim Female
            FemaleFrame.BorderColor = Colors.Transparent;
            FemaleFrame.BackgroundColor = Colors.White;
            FemaleFrame.Opacity = 0.5;
        }
        else
        {
            // Highlight Female
            FemaleFrame.BorderColor = Color.FromArgb("#EC407A"); // pink border
            FemaleFrame.BackgroundColor = Color.FromArgb("#FCE4EC"); // light pink bg
            FemaleFrame.Opacity = 1.0;

            // Dim Male
            MaleFrame.BorderColor = Colors.Transparent;
            MaleFrame.BackgroundColor = Colors.White;
            MaleFrame.Opacity = 0.5;
        }
    }

    
    //  labels
    

    private void OnHeightSliderChanged(object? sender, ValueChangedEventArgs e)
    {
        HeightValueLabel.Text = ((int)e.NewValue).ToString();
    }

    private void OnWeightSliderChanged(object? sender, ValueChangedEventArgs e)
    {
        WeightValueLabel.Text = ((int)e.NewValue).ToString();
    }

    // bmi calc button

    private async void OnCalculateBmiClicked(object? sender, EventArgs e)
    {
        double height = HeightSlider.Value;
        double weight = WeightSlider.Value;

        // input validation
        if (height <= 0)
        {
            await DisplayAlert("Invalid Input",
                "Height must be greater than 0.", "Ok");
            return;
        }
        if (weight <= 0)
        {
            await DisplayAlert("Invalid Input",
                "Weight must be greater than 0.", "Ok");
            return;
        }

       
        // BMI = (weight in lbs × 703) / (height in inches)²
        double bmi = (weight * 703) / (height * height);
        bmi = Math.Round(bmi, 1);

        string healthStatus = GetHealthStatus(bmi, _selectedGender);
        string recommendations = GetRecommendations(healthStatus);

        // Result strings
        string result =
            $"Gender: {_selectedGender}\n" +  //Gender
            $"BMI: {bmi}\n" +                 //BMI
            $"Health Status: {healthStatus}\n" + //Healthstatus
            $"Recommendations:\n{recommendations}"; //recommendatios 

        // ---------- Show alert ----------
        await DisplayAlert("Your calculated BMI results are:", result, "Ok");
    }

    // underweight to obese 

    private static string GetHealthStatus(double bmi, string gender)
    {
        if (gender == "Male")
        {
            // Male thresholds
            if (bmi < 18.5) return "Underweight";
            if (bmi < 25) return "Normal weight";
            if (bmi < 30) return "Overweight";
            return "Obese";
        }
        else
        {
            // Female slightly different
            if (bmi < 18) return "Underweight";
            if (bmi < 24) return "Normal weight";
            if (bmi < 29) return "Overweight";
            return "Obese";
        }
    }

    // recommendations on bmi

    private static string GetRecommendations(string healthStatus)
    {
        return healthStatus switch
        {
            "Underweight" =>
                " -Increase calorie intake with nutrient-dense foods " +
                    "such as nuts, avocados, and whole grains.\n" +
                " -Incorporate strength training exercises to build " +
                    "muscle mass.\n" +
                " -Consult a healthcare provider to rule out " +
                    "underlying health conditions.",

            "Normal weight" =>
                " -Maintain a balanced diet rich in fruits, vegetables, " +
                    "lean proteins, and whole grains.\n" +
                " -Stay physically active with at least 150 minutes of " +
                    "moderate exercise per week.\n" +
                " -Continue monitoring your weight and health regularly.",

            "Overweight" =>
                " -Reduce processed foods and focus on portion control.\n" +
                " -Engage in regular aerobic exercises (e.g., jogging, " +
                    "swimming) and strength training.\n" +
                " -Drink plenty of water and track your progress.",

            "Obese" =>
                " -Seek guidance from a healthcare professional for a " +
                    "personalized weight management plan.\n" +
                " -Focus on a calorie-controlled diet with high fiber " +
                    "and lean protein.\n" +
                " -Start with low-impact exercises such as walking or " +
                    "cycling and gradually increase intensity.",

            _ => " -Consult a healthcare provider for personalized advice."
        };
    }
}