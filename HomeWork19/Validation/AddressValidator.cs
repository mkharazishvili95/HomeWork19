using FluentValidation;
using HomeWork19.Models;

namespace HomeWork19.Validation
{
    public class AddressValidator : AbstractValidator<Address>
    {
        public AddressValidator() {
            RuleFor(address => address.Country).NotEmpty().WithMessage("Enter Country!");
            RuleFor(address => address.City).NotEmpty().WithMessage("Enter City!");
            RuleFor(address => address.HomeNumber).NotEmpty().WithMessage("Enter HomeNumber!");
        }
    }
}
