namespace labo_02_oef1.Repositories;

public interface IVaccinationRegistrationRepository
{
  List<VaccinRegistration> GetVaccinRegistrations();
  void AddVaccinationRegistration(VaccinRegistration vaccinRegistration);

}

public class VaccinationRegistrationRepository : IVaccinationRegistrationRepository
{
  private static List<VaccinRegistration> _vaccinRegistration = new List<VaccinRegistration>();

  public List<VaccinRegistration> GetVaccinRegistrations()
  {
    return _vaccinRegistration.ToList<VaccinRegistration>();
  }

  public void AddVaccinationRegistration(VaccinRegistration vaccinRegistration)
  {
    _vaccinRegistration.Add(vaccinRegistration);
  }
}

public class CsvVaccinationRegistrationsRepository : IVaccinationRegistrationRepository
{

  private readonly CsvConfig _csvConfig;

  public CsvVaccinationRegistrationsRepository(IOptions<CsvConfig> csvConfig)
  {
    _csvConfig = csvConfig.Value;
  }

  public void AddVaccinationRegistration(VaccinRegistration vaccinRegistration)
  {
    var registrations = GetVaccinRegistrations();
    registrations.Add(vaccinRegistration);
    try
    {
      using var writer = new StreamWriter(_csvConfig.CsvRegistrations);
      var config = new CsvConfiguration(CultureInfo.InvariantCulture)
      {
        HasHeaderRecord = true,
        Delimiter = ";"
      };
      using var csv = new CsvWriter(writer, config);
      csv.WriteRecords(registrations);
    }
    catch (System.Exception)
    {

      throw;
    }
  }

  public List<VaccinRegistration> GetVaccinRegistrations()
  {
    try
    {
      using var reader = new StreamReader(_csvConfig.CsvRegistrations);
      var config = new CsvConfiguration(CultureInfo.InvariantCulture)
      {
        HasHeaderRecord = true,
        Delimiter = ";"
      };
      using var csv = new CsvReader(reader, config);
      var records = csv.GetRecords<VaccinRegistration>();
      return records.ToList();
    }
    catch (System.Exception)
    {
      throw;
    }
  }
}