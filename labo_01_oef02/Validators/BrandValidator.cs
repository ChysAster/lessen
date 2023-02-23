namespace labo_01_oef02.Validators;

public class BrandValidator : AbstractValidator<Brand>
{
  public BrandValidator()
  {
    RuleFor(brand => brand.Name).NotEmpty().WithMessage("Name is required");
    RuleFor(brand => brand.Country).NotEmpty().WithMessage("Country is required");
  }
}