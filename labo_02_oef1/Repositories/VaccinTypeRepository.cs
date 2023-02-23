namespace labo_02_oef1.Repositories;

public interface IVaccinationTypeRepository
{
  List<VaccinType> GetVaccinTypes();
}
public class VaccinTypeRepository : IVaccinationTypeRepository
{
  private static List<VaccinType> _vaccinTypes = new List<VaccinType>();

  public VaccinTypeRepository()
  {
    if (!(_vaccinTypes.Any()))
    {
      _vaccinTypes.Add(new VaccinType()
      {
        VaccinTypeId = Guid.Parse("2864f207-026d-42b3-ba88-3a4674197741"),
        Name = "J&J"
      });
      _vaccinTypes.Add(new VaccinType()
      {
        VaccinTypeId = Guid.Parse("847841dd-810f-4867-971d-2a1fbe9e04b9"),
        Name = "Pfizer"
      });
    }
  }

  public List<VaccinType> GetVaccinTypes()
  {
    return _vaccinTypes.ToList<VaccinType>();
  }
}



public class CsvVaccinationTypeRepository : IVaccinationTypeRepository
{

  private readonly CsvConfig _csvConfig;

  public CsvVaccinationTypeRepository(IOptions<CsvConfig> csvConfig)
  {
    _csvConfig = csvConfig.Value;
  }
  public List<VaccinType> GetVaccinTypes()
  {
    try
    {
      using var reader = new StreamReader(_csvConfig.CsvVaccins);
      var config = new CsvConfiguration(CultureInfo.InvariantCulture)
      {
        HasHeaderRecord = false,
        Delimiter = ";"
      };
      using var csv = new CsvReader(reader, config);
      var records = csv.GetRecords<VaccinType>();
      return records.ToList();
    }
    catch (System.Exception)
    {
      throw;
    }
  }
}