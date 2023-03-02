using System;

namespace labo_03.Services;

public interface ICarService
{
  Task<List<Brand>> GetAllBrands();
  Task<Brand> GetBrandById(string id);
  Task<Brand> UpdateBrand(string id, string name, string country);
  Task DeleteBrand(string id);
  Task<Brand> AddBrand(Brand brand);
  Task<List<Car>> GetAllCars();
  Task<Car> GetCarById(string id);
  Task<Car> AddCar(Car car);

  Task SetupDummyData();
}

public class CarService : ICarService
{
  private readonly IBrandRepository _brandRepository;
  private readonly ICarRepository _carRepository;

  public CarService(IBrandRepository brandRepository, ICarRepository carRepository)
  {
    _brandRepository = brandRepository;
    _carRepository = carRepository;
  }

  public async Task SetupDummyData()
  {
    if (!(await _brandRepository.GetAllBrands()).Any())
    {

      var brands = new List<Brand>(){
            new Brand()
            {
            Country = "Germany" , Name = "Volkswagen"
            },
            new Brand()
            {
           Country = "Germany" , Name = "BMW"
            },
            new Brand()
            {
         Country = "Germany" , Name = "Audi"
            },
            new Brand()
            {
              Country = "USA" , Name = "Tesla"
            }
        };

      foreach (var brand in brands)
        await _brandRepository.AddBrand(brand);
    }

    if (!(await _carRepository.GetAllCars()).Any())
    {
      var brands = await _brandRepository.GetAllBrands();
      var cars = new List<Car>()
        {
            new Car(){

                Name = "ID.3",
                Brand = brands[0],
            },
            new Car(){

                Name = "ID.4",
                Brand = brands[0],
            },
            new Car(){

                Name = "IX3",
                Brand = brands[1],
            },
            new Car(){

                Name = "E-Tron",
                Brand = brands[2],
            },
            new Car(){

                Name = "Model Y",
                Brand = brands[3],
            }
        };
      foreach (var car in cars)
        await _carRepository.AddCar(car);
    }
  }
  public Task<Brand> AddBrand(Brand brand)
  {
    throw new NotImplementedException();
  }

  public Task<Car> AddCar(Car car)
  {
    throw new NotImplementedException();
  }

  public Task DeleteBrand(string id)
  {
    throw new NotImplementedException();
  }

  public async Task<List<Brand>> GetAllBrands()
  {
    return await _brandRepository.GetAllBrands();
  }

  public async Task<List<Car>> GetAllCars()
  {
    return await _carRepository.GetAllCars();
  }

  public async Task<Brand> GetBrandById(string id)
  {
    return await _brandRepository.GetBrandById(id);
  }

  public async Task<Car> GetCarById(string id)
  {
    return await _carRepository.GetCarById(id);
  }

  public async Task<Brand> UpdateBrand(string id, string name, string country)
  {
    return await _brandRepository.UpdateBrand(id, name, country);
  }
}

