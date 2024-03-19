using D2Soft.Application.Abstractions;
using D2Soft.Application.FinancialAccounts.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2Soft.Application.FinancialAccounts.CommandHandlers
{
    public class UpdateFinancialAccountHandler : IRequestHandler<UpdateFinancialAccount, string>
    {
        private readonly IFinancialAccountRepository _financialAccountRepository;

        public UpdateFinancialAccountHandler(IFinancialAccountRepository financialAccountRepository)
        {
            _financialAccountRepository = financialAccountRepository;
        }

        public async Task<string> Handle(UpdateFinancialAccount request, CancellationToken cancellationToken)
        {
            try
            {
                var financialAccount = await _financialAccountRepository.GetFinancialAccountById(request.FinancialAccountId);

                if (financialAccount == null)
                {
                    return "Financial account not found.";
                }

                financialAccount.Id = request.FinancialAccountId;
                financialAccount.AccountNumber = request.UpdatedAccountNumber;
                financialAccount.AccountType = request.UpdatedAccountType;
                financialAccount.Balance = request.UpdatedBalance;

                await _financialAccountRepository.UpdateFinancialAccount(financialAccount);

                return "Financial account updated successfully.";
            }
            catch (Exception ex)
            {
                return $"Failed to update financial account: {ex.Message}";
            }
        }
    }
}
