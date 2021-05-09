using FluentValidation;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsFromWest_master.Validators
{
    public class RegisterValidator : AbstractValidator<MainWindow>
    {
        public RegisterValidator()
        {
            RuleFor(p => p.Login).NotEmpty();
        }
    }
}
