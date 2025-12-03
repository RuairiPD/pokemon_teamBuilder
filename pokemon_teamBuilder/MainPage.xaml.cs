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
                var response = await httpClient.GetAsync("https://pokeapi.co/api/v2/");
                if (response != null && response.IsSuccessStatusCode)
                {
                    string text = await response.Content.ReadAsStringAsync();

                    var pokemon = JsonSerializer.Deserialize<Pokemon>(text);

                    if (pokemon != null)
                    {
                        string output = $"Name: {pokemon.Name}\n" +
                                        $"Height: {pokemon.Height}\n" +
                                        $"Weight: {pokemon.Weight}";
                        ViewContents.Text = output;
                    }
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
