using Mqb.Descriptors.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mqb.Descriptors.Services
{
    public interface IDataRowService : ICrudService<IDataRow>
    {
        Task<IEnumerable<IDataRow>> GetByDataTypeIdAsync(string dataTypeId);
        Task<IPaged<IDataRow>> GetPagedByDataTypeIdAsync(string dataTypeId, int pageIndex, int pageSize);
        Task<bool> DeleteByDataTypeIdAsync(string dataTypeId);
    }
}
