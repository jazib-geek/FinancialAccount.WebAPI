using D2Soft.Domain.Entities;
using MediatR;

namespace D2Soft.Application.FinancialAccounts.Commands
{
    public class CreateFinancialAccount : IRequest<FinancialAccount>
    {
        public string? AccountNumber { get; set; }
        public string? AccountType { get; set; }
        public decimal? Balance { get; set; }
        public int? OwnerId { get; set; }
    }
}
