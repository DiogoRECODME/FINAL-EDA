using Recodme.Rd.EDA.BusinessLayer.OperationResults;
using Recodme.Rd.EDA.Data.ServiceInfo;
using Recodme.Rd.EDA.DataAccess.DAO.ServiceInfoDAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Recodme.Rd.EDA.BusinessLayer.BO.ServiceInfoBO
{
    public class ServiceProvidedBO
    {
        protected readonly ServiceProvidedDAO _dao;

        TransactionOptions transactionOptions = new TransactionOptions
        {
            IsolationLevel = IsolationLevel.ReadCommitted,
            Timeout = TimeSpan.FromSeconds(30)
        };

        public ServiceProvidedBO()
        {
            _dao = new ServiceProvidedDAO();
        }


        /*
         * CRUD + LIST
         */

        #region CREATE

        public virtual OperationResult Create(ServiceProvided serviceProvided)
        {
            try
            {
                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                _dao.Create(serviceProvided);
                transactionScope.Complete();

                return new OperationResult<List<Service>>() { Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult<List<Service>>() { Success = false, Exception = e };
            }
        }

        public async virtual Task<OperationResult> CreateAsync(ServiceProvided serviceProvided)
        {
            try
            {
                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                await _dao.CreateAsync(serviceProvided);
                transactionScope.Complete();

                return new OperationResult<List<Service>>() { Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult<List<Service>>() { Success = false, Exception = e };
            }
        }

        #endregion

        #region READ

        public virtual OperationResult<ServiceProvided> Read(Guid id)
        {
            try
            {
                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                var result = _dao.Read(id);
                transactionScope.Complete();

                return new OperationResult<ServiceProvided>() { Success = true, Result = result };
            }
            catch (Exception e)
            {
                return new OperationResult<ServiceProvided>() { Success = false, Exception = e };
            }
        }

        public async virtual Task<OperationResult<ServiceProvided>> ReadAsync(Guid id)
        {
            try
            {
                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                var result = await _dao.ReadAsync(id);
                transactionScope.Complete();

                return new OperationResult<ServiceProvided>() { Success = true, Result = result };
            }
            catch (Exception e)
            {
                return new OperationResult<ServiceProvided>() { Success = false, Exception = e };
            }
        }

        #endregion

        #region UPDATE

        public virtual OperationResult Update(ServiceProvided serviceProvided)
        {
            try
            {
                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                _dao.Update(serviceProvided);
                transactionScope.Complete();

                return new OperationResult<List<Service>>() { Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult<List<Service>>() { Success = false, Exception = e };
            }
        }

        public async virtual Task<OperationResult> UpdateAsync(ServiceProvided serviceProvided)
        {
            try
            {
                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                await _dao.UpdateAsync(serviceProvided);
                transactionScope.Complete();

                return new OperationResult<List<ServiceProvided>>() { Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult<List<ServiceProvided>>() { Success = false, Exception = e };
            }
        }

        #endregion

        #region DELETE

        public virtual OperationResult Delete(ServiceProvided serviceProvided)
        {
            try
            {
                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                _dao.Delete(serviceProvided);
                transactionScope.Complete();

                return new OperationResult() { Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult() { Success = false, Exception = e };
            }
        }

        public virtual OperationResult Delete(Guid id)
        {
            try
            {
                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                _dao.Delete(id);
                transactionScope.Complete();

                return new OperationResult() { Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult() { Success = false, Exception = e };
            }
        }

        public async virtual Task<OperationResult> DeleteAsync(ServiceProvided serviceProvided)
        {
            try
            {
                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                await _dao.DeleteAsync(serviceProvided);
                transactionScope.Complete();

                return new OperationResult() { Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult() { Success = false, Exception = e };
            }
        }

        public async virtual Task<OperationResult> DeleteAsync(Guid id)
        {
            try
            {
                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                await _dao.DeleteAsync(id);
                transactionScope.Complete();

                return new OperationResult() { Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult() { Success = false, Exception = e };
            }
        }

        #endregion

        #region LIST

        public virtual OperationResult<List<ServiceProvided>> List()
        {
            try
            {
                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                var result = _dao.List();
                transactionScope.Complete();

                return new OperationResult<List<ServiceProvided>>() { Success = true, Result = result };
            }
            catch (Exception e)
            {
                return new OperationResult<List<ServiceProvided>>() { Success = false, Exception = e };
            }
        }

        public async virtual Task<OperationResult<List<ServiceProvided>>> ListAsync()
        {
            try
            {
                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                var result = await _dao.ListAsync();
                transactionScope.Complete();

                return new OperationResult<List<ServiceProvided>>() { Success = true, Result = result };
            }
            catch (Exception e)
            {
                return new OperationResult<List<ServiceProvided>>() { Success = false, Exception = e };
            }
        }

        #endregion

        #region LIST UNDELETED

        public OperationResult<List<ServiceProvided>> ListUndeleted()
        {
            try
            {
                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                var result = _dao.List().Where(x => !x.IsDeleted).ToList();
                transactionScope.Complete();

                return new OperationResult<List<ServiceProvided>>() { Success = true, Result = result };
            }
            catch (Exception e)
            {
                return new OperationResult<List<ServiceProvided>>() { Success = false, Exception = e };
            }
        }

        public async Task<OperationResult<List<ServiceProvided>>> ListUndeletedAsync()
        {
            try
            {
                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                var result = await _dao.ListAsync();
                result = result.Where(x => !x.IsDeleted).ToList();
                transactionScope.Complete();

                return new OperationResult<List<ServiceProvided>>() { Success = true, Result = result };
            }
            catch (Exception e)
            {
                return new OperationResult<List<ServiceProvided>>() { Success = false, Exception = e };
            }
        }

        #endregion

        #region FILTER

        public OperationResult<List<ServiceProvided>> Filter(Func<ServiceProvided, bool> predicate)
        {
            try
            {
                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                var result = _dao.List();
                result = result.Where(predicate).ToList();
                transactionScope.Complete();

                return new OperationResult<List<ServiceProvided>>() { Result = result, Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult<List<ServiceProvided>>() { Success = false, Exception = e };
            }
        }

        public async Task<OperationResult<List<ServiceProvided>>> FilterAsync(Func<ServiceProvided, bool> predicate)
        {
            try
            {
                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                var result = await _dao.ListAsync();
                result = result.Where(predicate).ToList();
                transactionScope.Complete();

                return new OperationResult<List<ServiceProvided>>() { Result = result, Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult<List<ServiceProvided>>() { Success = false, Exception = e };
            }
        }

        #endregion
    }
}