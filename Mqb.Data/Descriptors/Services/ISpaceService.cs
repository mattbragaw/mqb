using Mqb.Descriptors.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mqb.Descriptors.Services
{
    public interface ISpaceService : ICrudService<ISpace>
    {
        Task<IEnumerable<ISpace>> GetByParentOrgIdAsync(string parentOrgId);
        Task<IPaged<ISpace>> GetPagedByParentOrgIdAsync(string parentOrgId, int pageIndex, int pageSize);
        Task<IEnumerable<ISpace>> GetByParentSpaceIdAsync(string parentSpaceId);
        Task<IPaged<ISpace>> GetPagedByParentSpaceIdAsync(string parentSpaceId, int pageIndex, int pageSize);
        Task<IEnumerable<ISpace>> GetParentChainAsync(string id);
        Task<bool> DeleteByParentOrgIdAsync(string parentOrgId);
        Task<bool> DeleteByParentSpaceIdAsync(string parentSpaceId);
    }
}
