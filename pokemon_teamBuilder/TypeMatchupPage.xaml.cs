namespace pokemon_teamBuilder;

public partial class TypeMatchupPage : ContentPage
{
	public TypeMatchupPage()
	{
		InitializeComponent();
        BindingContext = new TypeMatchupViewModel();
    }
}