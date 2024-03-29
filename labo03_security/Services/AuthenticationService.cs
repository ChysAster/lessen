using System;

namespace labo03_security.Services;
public record UserInfo(string username, string name, string city);
public record AuthenticationRequestBody(string username, string password);

public interface IAuthenticationService
{
  UserInfo ValidateUser(string username, string password);
}

public class AuthenticationService : IAuthenticationService
{
  public UserInfo ValidateUser(string username, string password)
  {
    return new UserInfo("dieterp", "De Preester Dieter", "Gent");
  }
}
