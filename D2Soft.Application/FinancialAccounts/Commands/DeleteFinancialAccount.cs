using MediatR;

namespace D2Soft.Application.FinancialAccounts.Commands
{
    public class DeleteFinancialAccount : IRequest<string>
    {
        public int FinancialAccountId { get; set; }
    }
}
