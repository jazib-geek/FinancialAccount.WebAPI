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
    public class DeleteFinancialAccountCommandHandler : IRequestHandler<DeleteFinancialAccount, string>
    {
        private readonly IFinancialAccountRepository _financialAccountRepository;

        public DeleteFinancialAccountCommandHandler(IFinancialAccountRepository financialAccountRepository)
        {
            _financialAccountRepository = financialAccountRepository;
        }

        public async Task<string> Handle(DeleteFinancialAccount request, CancellationToken cancellationToken)
        {
            try
            {
                await _financialAccountRepository.DeleteFinancialAccount(request.FinancialAccountId);
                return "Success";
            }
            catch (Exception ex)
            {
                return $"Failed to delete financial account: {ex.Message}";
            }
        }

    }
}
