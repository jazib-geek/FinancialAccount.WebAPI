using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D2Soft.Domain.Entities;
using MediatR;

namespace D2Soft.Application.FinancialAccounts.Queries
{
    public class GetFinancialAccountById : IRequest<FinancialAccount>
    {
        public int Id { get; set; }
    }
}
