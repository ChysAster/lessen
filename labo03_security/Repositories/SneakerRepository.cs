using System;

namespace labo03_security.Repositories;


public interface ISneakerRepository
{
  Task<Sneaker> GetSneakerById(string id);

  Task<Sneaker> AddSneaker(Sneaker sneaker);
}

public class SneakerRepository : ISneakerRepository
{

  private readonly IMongoContext _context;

  public SneakerRepository(IMongoContext context)
  {
    _context = context;
  }
  public async Task<Sneaker> AddSneaker(Sneaker sneaker)
  {
    await _context.SneakerCollection.InsertOneAsync(sneaker);
    return sneaker;
  }

  public async Task<Sneaker> GetSneakerById(string id)
  {
    return await _context.SneakerCollection.Find(_ => _.SneakerId == id).FirstOrDefaultAsync();

  }
}




