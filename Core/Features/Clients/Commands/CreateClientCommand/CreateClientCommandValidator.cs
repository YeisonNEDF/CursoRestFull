using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Features.Commands.CreateClientCommand
{
    public class CreateClientCommandValidator : AbstractValidator<CreateClientCommand>
    {
        public CreateClientCommandValidator()
        {
            RuleFor(p => p.Nombre)
                 .NotEmpty().WithMessage("{PropertyName} No puede ser vacio.")
                 .MaximumLength(80).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres");

            RuleFor(p => p.Apellido)
               .NotEmpty().WithMessage("{PropertyName} No puede ser vacio.")
               .MaximumLength(80).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres");


            RuleFor(p => p.FechaNacimiento)
               .NotEmpty().WithMessage("Fecha de Nacimiento No puede ser vacia.");

            RuleFor(p => p.Telefono)
               .NotEmpty().WithMessage("{PropertyName} No puede ser vacio.")
               .Matches(@"^\d{4}-\d{4}$").WithMessage("{PropertyName} debe cumplir el formato 0000-0000")
               .MaximumLength(9).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres");

            RuleFor(p => p.Email)
                 .NotEmpty().WithMessage("{PropertyName} No puede ser vacio.")
                 .EmailAddress().WithMessage("{PropertyName} debe ser una direccion email valida")
                 .MaximumLength(100).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres");

            RuleFor(p => p.Direccion)
                 .NotEmpty().WithMessage("{PropertyName} No puede ser vacio.")
                 .MaximumLength(120).WithMessage("{PropertyName} no debe exceder de {MaxLength} caracteres");
        }
    }
}
