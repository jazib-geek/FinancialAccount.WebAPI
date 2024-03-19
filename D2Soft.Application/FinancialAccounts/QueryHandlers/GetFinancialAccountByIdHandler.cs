using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D2Soft.Application.Abstractions;
using D2Soft.Application.FinancialAccounts.Commands;
using D2Soft.Application.FinancialAccounts.Queries;
using D2Soft.Domain.Entities;
using MediatR;

namespace D2Soft.Application.FinancialAccounts.QueryHandlers
{
    public class GetFinancialAccountByIdHandler : IRequestHandler<GetFinancialAccountById, FinancialAccount>
    {
        private readonly IFinancialAccountRepository _financialAccountRepository;
        public GetFinancialAccountByIdHandler(IFinancialAccountRepository financialAccountRepository)
        {
            _financialAccountRepository = financialAccountRepository;
        }
        public async Task<FinancialAccount> Handle(GetFinancialAccountById request, CancellationToken cancellationToken)
        {
            return await _financialAccountRepository.GetFinancialAccountById(request.Id);
        }
    }
}
