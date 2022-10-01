using NextFap.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IBaseServices<T> : IDisposable where T : class
    {
        public Task<T> GetAsync(int id);
        public Task<PaginatedList<T>> GetAll();
    }
}
