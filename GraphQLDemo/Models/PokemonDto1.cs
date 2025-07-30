namespace PokedexGraph;

// Dtos/PokemonDto.cs
public class PokemonDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Height { get; set; }
    public int Weight { get; set; }
    public int BaseExperience { get; set; }
    public List<string> Abilities { get; set; }
    public List<string> Types { get; set; }
    public string SpriteUrl { get; set; }
}