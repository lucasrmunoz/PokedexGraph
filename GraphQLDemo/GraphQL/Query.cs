using HotChocolate;
using GraphQLDemo.Services;
using PokeApiNet;

namespace GraphQLDemo.GraphQL;

public class Query
{
    [GraphQLDescription("Get a Pokemon by its name")]
    public async Task<PokemonType?> GetPokemon([Service] PokemonService pokemonService, string name)
    {
        var pokemon = await pokemonService.GetPokemonByName(name);
        return pokemon != null ? new PokemonType(pokemon) : null;
    }
}

[GraphQLDescription("Pokemon information")]
public class PokemonType
{
    public PokemonType(Pokemon pokemon)
    {
        Id = pokemon.Id;
        Name = pokemon.Name;
        Height = pokemon.Height;
        Weight = pokemon.Weight;
        BaseExperience = (int)pokemon?.BaseExperience;
        Types = pokemon.Types.Select(t => t.Type.Name).ToList();
    }

    [GraphQLDescription("The Pokemon's Pokedex number")]
    public int Id { get; set; }

    [GraphQLDescription("The Pokemon's name")]
    public string Name { get; set; } = string.Empty;

    [GraphQLDescription("The Pokemon's height in decimeters")]
    public int Height { get; set; }

    [GraphQLDescription("The Pokemon's weight in hectograms")]
    public int Weight { get; set; }

    [GraphQLDescription("The base experience gained from defeating this Pokemon")]
    public int BaseExperience { get; set; }

    [GraphQLDescription("The Pokemon's types")]
    public List<string> Types { get; set; } = new();
} 