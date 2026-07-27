using FluentValidation;
using MultRH.Application.Users.Dtos;

namespace MultRH.Application.Users.Validators
{
    public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
    {
        public CreateUserDtoValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("O nome completo é obrigatório.")
                .MinimumLength(3).WithMessage("O nome completo deve ter no mínimo 3 caracteres.")
                .MaximumLength(100).WithMessage("O nome completo deve ter no máximo 100 caracteres.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("O email é obrigatório.")
                .EmailAddress().WithMessage("O email deve ser válido.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("A senha é obrigatória.")
                .MinimumLength(8).WithMessage("A senha deve ter no mínimo 8 caracteres.");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("A confirmação de senha é obrigatória.")
                .Equal(x => x.Password).WithMessage("A confirmação de senha deve ser igual à senha.");

            RuleFor(x => x.Cpf)
                .NotEmpty().WithMessage("O CPF é obrigatório.")
                .Must(CpfValidator.IsValid).WithMessage("O CPF deve ser válido.");
        }
    }
}
