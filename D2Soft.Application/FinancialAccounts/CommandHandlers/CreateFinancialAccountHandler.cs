using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D2Soft.Application.Abstractions;
using D2Soft.Application.FinancialAccounts.Commands;
using D2Soft.Domain.Entities;
using MediatR;

namespace D2Soft.Application.FinancialAccounts.CommandHandlers
{
    public class CreateFinancialAccountHandler : IRequestHandler<CreateFinancialAccount, FinancialAccount>
    {
        private readonly IFinancialAccountRepository _financialAccountRepository;
        public CreateFinancialAccountHandler(IFinancialAccountRepository financialAccountRepository)
        {
            _financialAccountRepository = financialAccountRepository;
        }
        public async Task<FinancialAccount> Handle(CreateFinancialAccount request, CancellationToken cancellationToken)
        {
            var financialAccount = new FinancialAccount
            {
                AccountNumber = request.AccountNumber,
                AccountType = request.AccountType,
                Balance = request.Balance,
                OwnerId = request.OwnerId,
            };

            return await _financialAccountRepository.AddFinancialAccount(financialAccount);
        }
    }
}
