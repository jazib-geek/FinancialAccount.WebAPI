using D2Soft.Application.Abstractions;
using D2Soft.Application.FinancialAccounts.Commands;
using D2Soft.Application.FinancialAccounts.Queries;
using D2Soft.Domain.Entities;
using MediatR;

namespace D2Soft.Application.FinancialAccounts.QueryHandlers
{
    public class GetAllFinancialAccountsHandler : IRequestHandler<GetAllFinancialAccounts, List<FinancialAccount>>
    {
        private readonly IFinancialAccountRepository _financialAccountRepository;
        public GetAllFinancialAccountsHandler(IFinancialAccountRepository financialAccountRepository)
        {
            _financialAccountRepository = financialAccountRepository;
        }
        public async Task<List<FinancialAccount>> Handle(GetAllFinancialAccounts request, CancellationToken cancellationToken)
        {
            return await _financialAccountRepository.GetAll();
        }
    }
}
