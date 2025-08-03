using HotChocolate;
using PokedexGraph.Services;
using PokeApiNet;
using PokedexGraphApi.Models;
namespace PokedexGraph.GraphQL;

public class Query
{
    [GraphQLDescription("Get a Pokemon by its name")]
    public async Task<PokemonDto?> GetPokemon([Service] PokemonService pokemonService, string name)
    {
        var pokemon =  await pokemonService.GetPokemonByName(name);
        if (pokemon == null) return null;

        return new PokemonDto
        {
            Id = pokemon.Id,
            Name = pokemon?.Name,
            Types = pokemon?.Types.Select(x => x.Type.Name).ToList()
                    ?? [new($"No types found for {name}")],
            Height = pokemon?.Height,
            Weight = pokemon?.Weight,
            Abilities = pokemon?.Abilities.Select(x => x.Ability.Name).ToList()
                        ?? [new($"No abilities found for {name}")],
            SpriteUrl = pokemon?.Sprites?.Versions?.GenerationI?.RedBlue?.FrontDefault ??
                        "No sprite available for Gen 1",
            BaseExperience = pokemon?.BaseExperience,
        };
    }
}