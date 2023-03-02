using System;

namespace labo03_security.Repositories;


public interface IOccasionRepository
{
  Task<List<Occasion>> GetAllOccasions();
  Task AddOccasions(List<Occasion> occasion);
}

public class OccasionRepository : IOccasionRepository
{
  private readonly IMongoContext _context;

  public OccasionRepository(IMongoContext context)
  {
    _context = context;
  }

  public async Task AddOccasions(List<Occasion> occasion)
  {
    await _context.OccasionCollection.InsertManyAsync(occasion);
  }

  public async Task<List<Occasion>> GetAllOccasions()
  {
    return await _context.OccasionCollection.Find(_ => true).ToListAsync();
  }
}

