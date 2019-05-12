using Mqb.Descriptors.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mqb.Descriptors.Services
{
    public interface IDataTypeService : ICrudService<IDataType>
    {
        Task<IEnumerable<IDataType>> GetByParentOrgIdAsync(string parentOrgId);
        Task<IPaged<IDataType>> GetPagedByParentOrgIdAsync(string parentOrgId, int pageIndex, int pageSize);
        Task<IEnumerable<IDataType>> GetByParentSpaceIdAsync(string parentSpaceId);
        Task<IPaged<IDataType>> GetPagedByParentSpaceIdAsync(string parentSpaceId, int pageIndex, int pageSize);
        Task<IEnumerable<IDataType>> GetParentChainAsync(string id);
        Task<bool> DeleteByParentOrgIdAsync(string parentOrgId);
        Task<bool> DeleteByParentSpaceIdAsync(string parentSpaceId);
    }
}
