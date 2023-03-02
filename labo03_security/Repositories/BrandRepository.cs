using System;

namespace labo03_security.Repositories;

public interface IBrandRepository
{
  Task<List<Brand>> GetAllBrands();

  Task AddBrands(List<Brand> brand);
}


public class BrandRepository : IBrandRepository
{

  private readonly IMongoContext _context;

  public BrandRepository(IMongoContext context)
  {
    _context = context;
  }

  public async Task AddBrands(List<Brand> brand)
  {
    await _context.BrandsCollection.InsertManyAsync(brand);
  }

  public async Task<List<Brand>> GetAllBrands()
  {
    return await _context.BrandsCollection.Find(_ => true).ToListAsync();
  }
}

