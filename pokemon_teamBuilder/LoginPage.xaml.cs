namespace pokemon_teamBuilder;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
	}
    private async void OnLoginClicked(object sender, EventArgs e)
    {
        string username = UsernameEntry.Text?.Trim();
        string password = PasswordEntry.Text;

        // For demo purposes, accept any non-empty credentials
        if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
        {
            // Swap root page to AppShell
            Application.Current.Windows[0].Page = new AppShell();
        }
        else
        {
            await DisplayAlert("Error", "Please enter both username and password.", "OK");
        }
    }
}