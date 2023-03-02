using System;

namespace labo_03.Repositories;

public interface ICarRepository
{
  Task<List<Car>> GetAllCars();
  Task<Car> GetCarById(string id);
  Task<Car> AddCar(Car car);
}
public class CarRepository : ICarRepository
{
  private readonly IMongoContext _context;

  public CarRepository(IMongoContext context)
  {
    _context = context;
  }
  public async Task<Car> AddCar(Car car)
  {
    car.CreatedOn = DateTime.Now;
    await _context.CarsCollection.InsertOneAsync(car);
    return car;
  }

  public async Task<List<Car>> GetAllCars()
  {
    return await _context.CarsCollection.Find(_ => true).ToListAsync();
  }

  public Task<Car> GetCarById(string id)
  {
    throw new NotImplementedException();
  }
}

