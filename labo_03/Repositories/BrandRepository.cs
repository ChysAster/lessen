using System;

namespace labo_03.Repositories;
public interface IBrandRepository
{
  Task<List<Brand>> GetAllBrands();
  Task<Brand> GetBrandById(string id);
  Task<Brand> UpdateBrand(string id, string name, string country);
  Task DeleteBrand(string id);
  Task<Brand> AddBrand(Brand brand);

}

public class BrandRepository : IBrandRepository
{
  private readonly IMongoContext _context;

  public BrandRepository(IMongoContext context)
  {
    _context = context;
  }
  public async Task<Brand> AddBrand(Brand brand)
  {
    brand.CreatedOn = DateTime.Now;
    await _context.BrandsCollection.InsertOneAsync(brand);
    return brand;
  }

  public async Task<List<Brand>> GetAllBrands()
  {
    return await _context.BrandsCollection.Find(_ => true).ToListAsync();
  }

  public Task<Brand> GetBrandById(string id)
  {
    throw new NotImplementedException();
  }

  public Task<Brand> UpdateBrand(string id, string name, string country)
  {
    throw new NotImplementedException();
  }

  Task IBrandRepository.DeleteBrand(string id)
  {
    throw new NotImplementedException();
  }
}

