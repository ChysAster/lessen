

namespace labo_02_oef1.Validators;

public class VaccinRegistrationValidator : AbstractValidator<VaccinRegistration>
{
  public VaccinRegistrationValidator()
  {
    RuleFor(reg => reg.Name).NotEmpty().WithMessage("Name is required");
  }
}

