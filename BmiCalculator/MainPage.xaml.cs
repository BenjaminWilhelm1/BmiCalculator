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


        //  Now going to BmiResultPage, this passes data via query parameters
        await Shell.Current.GoToAsync(
            $"{nameof(BmiResultPage)}?height={height}&weight={weight}&gender={_selectedGender}");
        
    }
}