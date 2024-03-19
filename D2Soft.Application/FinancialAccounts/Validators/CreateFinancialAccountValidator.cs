using D2Soft.Application.FinancialAccounts.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2Soft.Application.FinancialAccounts.Validators
{
    public class CreateFinancialAccountValidator : AbstractValidator<CreateFinancialAccount>
    {
        public CreateFinancialAccountValidator()
        {
            RuleFor(command => command.AccountNumber).NotEmpty().WithMessage("Account number is required.");
            RuleFor(command => command.AccountType).NotEmpty().WithMessage("Account type is required.");
            RuleFor(command => command.Balance).GreaterThan(0).WithMessage("Balance must be greater than zero.");
        }
    }
}
