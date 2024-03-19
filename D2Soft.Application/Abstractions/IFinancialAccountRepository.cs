    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D2Soft.Domain.Entities;

namespace D2Soft.Application.Abstractions
{
    public interface IFinancialAccountRepository
    {
        Task<List<FinancialAccount>> GetAll();

        Task<FinancialAccount> GetFinancialAccountById(int FinancialAccountId);

        Task<FinancialAccount> AddFinancialAccount(FinancialAccount toCreate);

        Task<FinancialAccount> UpdateFinancialAccount(FinancialAccount toUpdate);

        Task DeleteFinancialAccount(int FinancialAccountId);
    }
}
