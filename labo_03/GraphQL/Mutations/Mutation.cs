using System;

namespace labo_03.GraphQL.Mutations;
public class Mutation
{
  public async Task<AddBrandPayload> AddBrand([Service] ICarService carService, AddBrandInput input)
  {
    var newBrand = new Brand()
    {
      Name = input.name,
      Country = input.country
    };
    var created = await carService.AddBrand(newBrand);
    return new AddBrandPayload(created);
  }
}

public record AddBrandInput(string name, string country); //imutable, kan niet meer gewijzigd worden
public record AddBrandPayload(Brand Brand);