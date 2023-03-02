using System;

namespace labo03_security.Services;

public interface ISneakerService
{
  Task<List<Brand>> GetAllBrands();
  Task<List<Occasion>> GetAllOccasions();
  Task<Sneaker> GetSneakerById(string id);
  Task<Sneaker> AddSneaker(Sneaker sneaker);
  Task SetupData();
}

public class SneakerService : ISneakerService
{
  private readonly IBrandRepository _brandRepository;
  private readonly ISneakerRepository _sneakerRepository;
  private readonly IOccasionRepository _occasionRepository;

  public SneakerService(IBrandRepository brandRepository, ISneakerRepository sneakerRepository, IOccasionRepository occasionRepository)
  {
    _brandRepository = brandRepository;
    _sneakerRepository = sneakerRepository;
    _occasionRepository = occasionRepository;
  }
  public async Task<Sneaker> AddSneaker(Sneaker sneaker)
  {
    return await _sneakerRepository.AddSneaker(sneaker);
  }

  public async Task<List<Brand>> GetAllBrands()
  {
    return await _brandRepository.GetAllBrands();
  }

  public async Task<List<Occasion>> GetAllOccasions()
  {
    return await _occasionRepository.GetAllOccasions();
  }

  public async Task<Sneaker> GetSneakerById(string id)
  {
    return await _sneakerRepository.GetSneakerById(id);
  }

  public async Task SetupData()
  {
    try
    {
      if (!(await _brandRepository.GetAllBrands()).Any())
        await _brandRepository.AddBrands(new List<Brand>() { new Brand() { Name = "ASICS" }, new Brand() { Name = "CONVERSE" }, new Brand() { Name = "JORDAN" }, new Brand() { Name = "PUMA" } });

      if (!(await _occasionRepository.GetAllOccasions()).Any())
        await _occasionRepository.AddOccasions(new List<Occasion>() { new Occasion() { Description = "Sports" }, new Occasion() { Description = "Casual" }, new Occasion() { Description = "Skate" }, new Occasion() { Description = "Diner" } });
    }
    catch (System.Exception ex)
    {
      Console.WriteLine(ex);
      throw;
    }
  }

  public Task SetupDummyData()
  {
    throw new NotImplementedException();
  }
}

