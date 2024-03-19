using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2Soft.Application.FinancialAccounts.Commands
{
    public class UpdateFinancialAccount : IRequest<(bool, string)>
    {
        public int FinancialAccountId { get; set; }
        public string UpdatedAccountNumber { get; set; }
        public string UpdatedAccountType { get; set; }
        public decimal UpdatedBalance { get; set; }
    }
}
