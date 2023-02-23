namespace labo_02_oef1.Services;

public interface IVaccinationService
{
  VaccinRegistration AddRegistration(VaccinRegistration registration);
  List<VaccinationLocation> GetLocations();
  List<VaccinRegistration> GetRegistrations();
  List<VaccinType> GetVaccins();
}

public class VaccinationService : IVaccinationService
{

  private readonly IVaccinationLocationRepository _locationRepository;
  private readonly IVaccinationRegistrationRepository _vaccinationRegistrationRepository;

  private readonly IVaccinationTypeRepository _vaccinationTypeRepository;

  public VaccinationService(IVaccinationLocationRepository locationRepository, IVaccinationTypeRepository vaccinationTypeRepository, IVaccinationRegistrationRepository vaccinationRegistrationRepository)
  {
    _locationRepository = locationRepository;
    _vaccinationRegistrationRepository = vaccinationRegistrationRepository;
    _vaccinationTypeRepository = vaccinationTypeRepository;
  }

  public VaccinRegistration AddRegistration(VaccinRegistration registration)
  {
    ArgumentNullException.ThrowIfNull(registration);
    _vaccinationRegistrationRepository.AddVaccinationRegistration(registration);
    return registration;
  }

  public List<VaccinationLocation> GetLocations()
  {
    return _locationRepository.GetLocations();
  }

  public List<VaccinRegistration> GetRegistrations()
  {
    return _vaccinationRegistrationRepository.GetVaccinRegistrations();
  }

  public List<VaccinType> GetVaccins()
  {
    return _vaccinationTypeRepository.GetVaccinTypes();
  }
}

