using D2Soft.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace D2Soft.Application.FinancialAccounts.Queries
{
    public class GetAllFinancialAccounts : IRequest<List<FinancialAccount>>
    {
    }
}
