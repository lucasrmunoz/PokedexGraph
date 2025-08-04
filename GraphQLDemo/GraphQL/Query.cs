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

    [GraphQLDescription("Get a Pokemon by its ID (1-151 only)")]
    public async Task<PokemonDto?> GetPokemonById([Service] PokemonService pokemonService, int id)
    {
        // Validate that the ID is within the original 151 Pokemon
        if (id < 1 || id > 151)
        {
            throw new GraphQLException("Pokemon has not been discovered yet! Only Pokemon in Kanto have been discovered.");
        }

        var pokemon = await pokemonService.GetPokemonById(id);
        if (pokemon == null)
        {
            throw new GraphQLException($"Pokemon with ID {id} not found.");
        }

        return new PokemonDto
        {
            Id = pokemon.Id,
            Name = pokemon.Name,
            Types = pokemon.Types.Select(x => x.Type.Name).ToList(),
            Height = pokemon.Height,
            Weight = pokemon.Weight,
            Abilities = pokemon.Abilities.Select(x => x.Ability.Name).ToList(),
            SpriteUrl = pokemon.Sprites?.Versions?.GenerationI?.RedBlue?.FrontDefault ??
                        "No sprite available for Gen 1",
            BaseExperience = pokemon.BaseExperience,
        };
    }
}