using Microsoft.AspNetCore.Mvc;
using PokeApiNet;
using PokedexGraph.Services;

namespace GraphQLDemo.Controllers;

[ApiController]
[Route("[controller]")]
public class PokemonController : ControllerBase
{
    private readonly PokemonService _pokemonService;

    public PokemonController(PokemonService pokemonService)
    {
        _pokemonService = pokemonService;
    }

    [HttpGet("{name}")]
    public async Task<IActionResult> GetPokemon(string name)
    {
        var pokemon = await _pokemonService.GetPokemonByName(name);
        if (pokemon is null)
            return NotFound();

        return Ok(pokemon);
    }
} 