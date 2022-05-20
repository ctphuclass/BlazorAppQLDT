using Microsoft.AspNetCore.Components;

namespace BlazorAppQLDT.Client.Services.SuperHeroService
{
    public class SuperHeroService : ISuperHeroService
    {
        private readonly HttpClient _http;
        private readonly NavigationManager _navigationManager;

        public List<SuperHero> Heroes { get; set; } = new List<SuperHero>();
        public List<Comic> Comics { get; set ; } = new List<Comic>();
        public SuperHeroService(HttpClient http , NavigationManager navigationManager)
        {
            _http = http;
            _navigationManager = navigationManager;
        }
        public async Task GetComics()
        {
            var result = await _http.GetFromJsonAsync<List<Comic>>("api/superhero/comic");
            if (result != null)
            {
                Comics = result;
            }
        }

        public async Task<SuperHero> GetSingleHero(int id)
        {
            var result = await _http.GetFromJsonAsync<SuperHero>($"api/superhero/{id}");
            //Console.WriteLine(result.ToString());
            if (result != null)
                return result;
            throw new Exception("Error not found.");
        }

        public async Task GetSuperHeroes()
        {
            var result = await _http.GetFromJsonAsync<List<SuperHero>>("api/superhero");
            if(result != null)
            {
                Heroes = result;
            }
        }

        public async Task  UpdateHero(SuperHero hero)
        {
            var result = await _http.PutAsJsonAsync($"api/superhero/{hero.Id}", hero);
            //var response = await result.Content.ReadFromJsonAsync<List<SuperHero>>();
            await SetHeroes(result);
        }

        public async Task CreateHero(SuperHero hero)
        {
            var result = await _http.PostAsJsonAsync("api/superhero", hero);
            await SetHeroes(result);
        }

        private async Task SetHeroes(HttpResponseMessage result)
        {
            //var response = await result.Content.ReadFromJsonAsync<List<SuperHero>>();
            //Heroes = response;
            _navigationManager.NavigateTo("superheroes");
        }

        public async Task DeleteHero(int id)
        {
            var result = await _http.DeleteAsync($"api/superhero/{id}");
            await SetHeroes(result);
        }
    }
}
