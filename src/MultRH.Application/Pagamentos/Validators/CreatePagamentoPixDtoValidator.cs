using FluentValidation;
using MultRH.Application.Pagamentos.Dtos;
using MultRH.Application.Users.Validators;

namespace MultRH.Application.Pagamentos.Validators
{
    public class CreatePagamentoPixDtoValidator : AbstractValidator<CreatePagamentoPixDto>
    {
        public CreatePagamentoPixDtoValidator()
        {
            RuleFor(x => x.PlanoId)
                .GreaterThan(0).WithMessage("O plano é obrigatório!");

            RuleFor(x => x.PayerEmail)
                .NotEmpty().WithMessage("O e-mail é obrigatório!")
                .EmailAddress().WithMessage("O e-mail deve ser válido!");

            RuleFor(x => x.PayerCpf)
                .NotEmpty().WithMessage("O CPF é obrigatório!")
                .Must(CpfValidator.IsValid).WithMessage("O CPF deve ser válido!");

        }
    }
}
