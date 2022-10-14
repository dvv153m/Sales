using FluentValidation;
using Sales.Contracts.Request.Order;

namespace Sales.Order.Api.V1.Validators
{
    public class AddProductToOrderValidator : AbstractValidator<AddProductToOrderRequest>
    {
        public AddProductToOrderValidator()
        {
            RuleFor(x => x.Promocode)
               .NotEmpty().WithMessage("{PropertyName} is Empty");

            RuleFor(x => x.ProductId)
                .GreaterThan(0);

            RuleFor(x => x.Quantity)
                .GreaterThan(0);
        }
    }
}
