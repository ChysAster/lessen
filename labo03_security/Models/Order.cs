using System;

namespace labo03_security.Models;
public class Order
{
  [BsonId]
  [BsonRepresentation(BsonType.ObjectId)]
  public string? OrderId { get; set; }
  public string? Email { get; set; }
  public string? SneakerId { get; set; }
  public int NumberOfItems { get; set; }
}
