using System;

namespace labo_03.GraphQL.Queries;
public class Queries
{
  public async Task<List<Car>> GetCars([Service] ICarService carService)
  {
    return await carService.GetAllCars();
  }


}

