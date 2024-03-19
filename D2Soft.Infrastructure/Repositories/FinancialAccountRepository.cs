using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D2Soft.Application.Abstractions;
using D2Soft.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace D2Soft.Infrastructure.Repositories
{
    public class FinancialAccountRepository : IFinancialAccountRepository
    {
        private readonly AppDbContext _context;

        public FinancialAccountRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<FinancialAccount> AddFinancialAccount(FinancialAccount toCreate)
        {
            _context.FinancialAccounts.Add(toCreate);
            await _context.SaveChangesAsync();

            return toCreate;
        }

        public async Task DeleteFinancialAccount(int FinancialAccountId)
        {
            var account = _context.FinancialAccounts
             .FirstOrDefault(p => p.Id == FinancialAccountId);

            if (account is null) return;

            _context.FinancialAccounts.Remove(account);

            await _context.SaveChangesAsync();
             
        }

        public async Task<List<FinancialAccount>> GetAll()
        {
            return await _context.FinancialAccounts.ToListAsync();
        }

        public async Task<FinancialAccount> GetFinancialAccountById(int FinancialAccountId)
        {
            return await _context.FinancialAccounts.Include(x => x.Owner).FirstOrDefaultAsync(p => p.Id == FinancialAccountId);
        }

        public async Task<FinancialAccount> UpdateFinancialAccount(FinancialAccount toUpdate)
        {
            var financialAccount = await _context.FinancialAccounts
                       .FirstOrDefaultAsync(p => p.Id == toUpdate.Id);

            financialAccount.AccountNumber = toUpdate.AccountNumber;
            financialAccount.AccountType = toUpdate.AccountType;
            financialAccount.Balance = toUpdate.Balance;

            await _context.SaveChangesAsync();

            return financialAccount;
        }
    }
}
