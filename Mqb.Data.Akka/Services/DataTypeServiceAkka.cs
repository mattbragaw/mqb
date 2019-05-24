using Akka.Actor;
using Mqb.Akka.Actors;
using Mqb.Descriptors;
using Mqb.Descriptors.Models;
using Mqb.Descriptors.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mqb.Services
{
    public class DataTypeServiceAkka : IDataTypeService
    {
        public DataTypeServiceAkka(IHasDataTypesRef_R hasDataTypesRef) : this(hasDataTypesRef.DataTypes) { }
        public DataTypeServiceAkka(IActorRef dataTypesRef)
        {
            DataTypesRef = dataTypesRef;
        }

        public IActorRef DataTypesRef { get; }

        public async Task<bool> CreateAsync(IDataType model)
        {
            var result = await DataTypesRef.Ask(new DataTypesActor.Create(model), TimeSpan.FromSeconds(3));

            if (result.GetType() == typeof(DataTypesActor.Create_Success))
                return true;
            else
                return false;
        }
        public async Task<bool> DeleteAllAsync()
        {
            var result = await DataTypesRef.Ask(new DataTypesActor.DeleteAll(), TimeSpan.FromSeconds(3));

            if (result.GetType() == typeof(DataTypesActor.DeleteAll_Success))
                return true;
            else
                return false;
        }
        public async Task<bool> DeleteByIdAsync(string id)
        {
            var result = await DataTypesRef.Ask(new DataTypesActor.DeleteById(id), TimeSpan.FromSeconds(3));

            if (result.GetType() == typeof(DataTypesActor.DeleteById_Success))
                return true;
            else
                return false;
        }
        public async Task<bool> DeleteByParentOrgIdAsync(string parentOrgId)
        {
            var result = await DataTypesRef.Ask(new DataTypesActor.DeleteByParentOrgId(parentOrgId), TimeSpan.FromSeconds(3));

            if (result.GetType() == typeof(DataTypesActor.DeleteByParentOrgId_Success))
                return true;
            else
                return false;
        }
        public async Task<bool> DeleteByParentSpaceIdAsync(string parentSpaceId)
        {
            var result = await DataTypesRef.Ask(new DataTypesActor.DeleteByParentSpaceId(parentSpaceId), TimeSpan.FromSeconds(3));

            if (result.GetType() == typeof(DataTypesActor.DeleteByParentSpaceId_Success))
                return true;
            else
                return false;
        }
        public async Task<bool> DeleteIfAsync(Predicate<IDataType> predicate)
        {
            var result = await DataTypesRef.Ask(new DataTypesActor.DeleteIf(predicate), TimeSpan.FromSeconds(3));

            if (result.GetType() == typeof(DataTypesActor.DeleteIf_Success))
                return true;
            else
                return false;
        }
        public async Task<IEnumerable<IDataType>> GetAllAsync()
        {
            var result = await DataTypesRef.Ask<IEnumerable<IDataType>>(new DataTypesActor.GetAll(), TimeSpan.FromSeconds(3));

            return result;
        }
        public async Task<IDataType> GetByIdAsync(string id)
        {
            var result = await DataTypesRef.Ask(new DataTypesActor.GetById(id), TimeSpan.FromSeconds(3));

            if (result.GetType() == typeof(IDataType))
                return (IDataType)result;
            else
                return null;
        }
        public async Task<IEnumerable<IDataType>> GetByParentOrgIdAsync(string parentOrgId)
        {
            var result = await DataTypesRef.Ask<IEnumerable<IDataType>>(new DataTypesActor.GetByParentOrgId(parentOrgId), TimeSpan.FromSeconds(3));

            return result;
        }
        public async Task<IEnumerable<IDataType>> GetByParentSpaceIdAsync(string parentSpaceId)
        {
            var result = await DataTypesRef.Ask<IEnumerable<IDataType>>(new DataTypesActor.GetByParentSpaceId(parentSpaceId), TimeSpan.FromSeconds(3));

            return result;
        }
        public async Task<IEnumerable<IDataType>> GetIfAsync(Predicate<IDataType> predicate)
        {
            var result = await DataTypesRef.Ask<IEnumerable<IDataType>>(new DataTypesActor.GetIf(predicate), TimeSpan.FromSeconds(3));

            return result;
        }
        public Task<IPaged<IDataType>> GetPagedAllAsync(int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }
        public Task<IPaged<IDataType>> GetPagedByParentOrgIdAsync(string parentOrgId, int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }
        public Task<IPaged<IDataType>> GetPagedByParentSpaceIdAsync(string parentSpaceId, int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }
        public Task<IPaged<IDataType>> GetPagedIfAsync(Predicate<IDataType> predicate, int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }
        public Task<IEnumerable<IDataType>> GetParentChainAsync(string id)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> UpdateAsync(IDataType model)
        {
            var result = await DataTypesRef.Ask(new DataTypesActor.Update(model), TimeSpan.FromSeconds(3));

            if (result.GetType() == typeof(DataTypesActor.Update_Success))
                return true;
            else
                return false;
        }
    }
}
