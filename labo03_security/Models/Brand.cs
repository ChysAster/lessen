using System;


namespace labo03_security.Models;

public class Brand
{
  [BsonId]
  [BsonRepresentation(BsonType.ObjectId)]
  public string? BrandId { get; set; }
  public string? Name { get; set; }
}

