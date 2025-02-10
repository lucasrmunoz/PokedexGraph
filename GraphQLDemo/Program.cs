using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;

using GraphQLDemo.Services;
using GraphQLDemo.GraphQL;

var builder = WebApplication.CreateBuilder(args);

// Add GraphQL client as singleton
builder.Services.AddSingleton<GraphQLHttpClient>(sp => 
    new GraphQLHttpClient("https://beta.pokeapi.co/graphql/v1beta", new NewtonsoftJsonSerializer())
);

// Configure GraphQL server
builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddType<PokemonType>();

builder.Services.AddScoped<PokemonService>();

var app = builder.Build();

app.UseRouting();
app.UseWebSockets();
app.MapGraphQL();

app.Run();
