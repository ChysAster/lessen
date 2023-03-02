using System;

namespace labo03_security.Models;
public class Occasion
{
  [BsonId]
  [BsonRepresentation(BsonType.ObjectId)]
  public string? OccasionId { get; set; }
  public string? Description { get; set; }
}


