using System.Text.Json;
using System.Text.Json.Serialization;


namespace pokemon_teamBuilder
{
    public partial class MainPage : ContentPage
    {
        private HttpClient httpClient;

        public MainPage()
        {
            InitializeComponent();
            httpClient = new HttpClient();

        }
        private async void DownloadBtn_Clicked(object sender, EventArgs e)
        {
            DownloadBtn.IsEnabled = false;
            try
            {
                string userInput = PokemonEntry.Text?.Trim().ToLower();
                if (string.IsNullOrEmpty(userInput))
                {
                    await DisplayAlert("Error", "Please enter a Pokémon name or ID.", "OK");
                    DownloadBtn.IsEnabled = true;
                    return;
                }




                var response = await httpClient.GetAsync($"https://pokeapi.co/api/v2/pokemon/{userInput}/");
                if (response != null && response.IsSuccessStatusCode)
                {
                    string text = await response.Content.ReadAsStringAsync();

                    var pokemon = JsonSerializer.Deserialize<Pokemon>(text);

                    if (pokemon != null)
                    {
                        ViewContents.Text = $"Name: {pokemon.Name}\n" +
                                        $"Height: {pokemon.Height}\n" +
                                        $"Weight: {pokemon.Weight}";
                        
                        PokemonImage.Source = pokemon.Sprites.FrontDefault;
                    }
                }
                else
                {
                    await DisplayAlert("Error", $"Pokémon '{userInput}' not found.", "OK");
                }
            }
            catch
            {
                await DisplayAlert("Error", "Error in downloading Webpage", "OK");
            }
            DownloadBtn.IsEnabled = true;
        }

    }

    public class Pokemon
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("height")]
        public int Height { get; set; }

        [JsonPropertyName("weight")]
        public int Weight { get; set; }

        [JsonPropertyName("sprites")]
        public Sprites Sprites { get; set; }

    }
    public class Sprites
    {
        [JsonPropertyName("front_default")]
        public string FrontDefault { get; set; }
    }
}
