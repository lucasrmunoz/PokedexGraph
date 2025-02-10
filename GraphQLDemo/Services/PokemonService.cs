using PokeApiNet;

namespace GraphQLDemo.Services;

public class PokemonService
{
    private readonly PokeApiClient _client;

    public PokemonService()
    {
        _client = new PokeApiClient();
    }

    public async Task<Pokemon?> GetPokemonByName(string name)
    {
        try 
        {
            return await _client.GetResourceAsync<Pokemon>(name);
        }
        catch
        {
            return null;
        }
    }
} 