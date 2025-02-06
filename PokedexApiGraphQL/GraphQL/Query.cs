using PokeApiNet;

namespace PokedexApiGraphQL.GraphQL;
using PokeApiNet;

public class Query
{
    private readonly PokeApiClient _pokeClient;

    public Query(PokeApiClient pokeClient)
    {
        _pokeClient = pokeClient;
    }

    [GraphQLDescription("Returns a welcome message")]
    public string GetWelcomeMessage() => "Welcome to the PokeAPI GraphQL server!";

    [GraphQLDescription("Get Pokemon types by name")]
    public async Task<IEnumerable<string>> GetPokemonTypes(string name)
    {
        var pokemon = await _pokeClient.GetResourceAsync<Pokemon>(name.ToLower());
        return pokemon.Types.Select(t => t.Type.Name);
    }
}

