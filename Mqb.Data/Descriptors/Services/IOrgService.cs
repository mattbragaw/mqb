using Mqb.Descriptors.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mqb.Descriptors.Services
{
    public interface IOrgService : ICrudService<IOrg>
    {
        Task<IEnumerable<IOrg>> GetRootsAsync();
        Task<IPaged<IOrg>> GetPagedRootsAsync(int pageIndex, int pageSize);
        Task<IEnumerable<IOrg>> GetByParentOrgIdAsync(string parentOrgId);
        Task<IPaged<IOrg>> GetPagedByParentOrgIdAsync(string parentOrgId, int pageIndex, int pageSize);
        Task<IEnumerable<IOrg>> GetParentChainAsync(string id);
        Task<bool> DeleteByParentOrgIdAsync(string parentOrgId);
    }
}
