using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2Soft.Infrastructure.Repositories
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync();
        void Rollback();
    }

}
