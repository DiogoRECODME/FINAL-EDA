using Recodme.Rd.EDA.BusinessLayer.OperationResults;
using Recodme.Rd.EDA.Data.UserInfo;
using Recodme.Rd.EDA.DataAccess.DAO.UserInfoDAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Recodme.Rd.EDA.BusinessLayer.BO.UserInfoBO
{
    public class ClientBO
    {
        protected readonly ClientDAO _dao;

        TransactionOptions transactionOptions = new TransactionOptions
        {
            IsolationLevel = IsolationLevel.ReadCommitted,
            Timeout = TimeSpan.FromSeconds(30)
        };

        public ClientBO()
        {
            _dao = new ClientDAO();
        }


        /*
         * CRUD + LIST
         */

        #region CREATE

        public virtual OperationResult Create(Client client)
        {
            try
            {
                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                _dao.Create(client);
                transactionScope.Complete();

                return new OperationResult<List<Client>>() { Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult<List<Client>>() { Success = false, Exception = e };
            }
        }

        public async virtual Task<OperationResult> CreateAsync(Client client)
        {
            try
            {
                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                await _dao.CreateAsync(client);
                transactionScope.Complete();

                return new OperationResult<List<Client>>() { Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult<List<Client>>() { Success = false, Exception = e };
            }
        }

        #endregion

        #region READ

        public virtual OperationResult<Client> Read(Guid id)
        {
            try
            {
                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                var result = _dao.Read(id);
                transactionScope.Complete();

                return new OperationResult<Client>() { Success = true, Result = result };
            }
            catch (Exception e)
            {
                return new OperationResult<Client>() { Success = false, Exception = e };
            }
        }

        public async virtual Task<OperationResult<Client>> ReadAsync(Guid id)
        {
            try
            {
                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                var result = await _dao.ReadAsync(id);
                transactionScope.Complete();

                return new OperationResult<Client>() { Success = true, Result = result };
            }
            catch (Exception e)
            {
                return new OperationResult<Client>() { Success = false, Exception = e };
            }
        }

        #endregion

        #region UPDATE

        public virtual OperationResult Update(Client client)
        {
            try
            {
                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                _dao.Update(client);
                transactionScope.Complete();

                return new OperationResult<List<Client>>() { Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult<List<Client>>() { Success = false, Exception = e };
            }
        }

        public async virtual Task<OperationResult> UpdateAsync(Client client)
        {
            try
            {
                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                await _dao.UpdateAsync(client);
                transactionScope.Complete();

                return new OperationResult<List<Client>>() { Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult<List<Client>>() { Success = false, Exception = e };
            }
        }

        #endregion

        #region DELETE

        public virtual OperationResult Delete(Client client)
        {
            try
            {
                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                _dao.Delete(client);
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

        public async virtual Task<OperationResult> DeleteAsync(Client client)
        {
            try
            {
                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                await _dao.DeleteAsync(client);
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

        public virtual OperationResult<List<Client>> List()
        {
            try
            {
                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                var result = _dao.List();
                transactionScope.Complete();

                return new OperationResult<List<Client>>() { Success = true, Result = result };
            }
            catch (Exception e)
            {
                return new OperationResult<List<Client>>() { Success = false, Exception = e };
            }
        }

        public async virtual Task<OperationResult<List<Client>>> ListAsync()
        {
            try
            {
                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                var result = await _dao.ListAsync();
                transactionScope.Complete();

                return new OperationResult<List<Client>>() { Success = true, Result = result };
            }
            catch (Exception e)
            {
                return new OperationResult<List<Client>>() { Success = false, Exception = e };
            }
        }

        #endregion

        #region LIST UNDELETED

        public OperationResult<List<Client>> ListUndeleted()
        {
            try
            {
                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                var result = _dao.List().Where(x => !x.IsDeleted).ToList();
                transactionScope.Complete();

                return new OperationResult<List<Client>>() { Success = true, Result = result };
            }
            catch (Exception e)
            {
                return new OperationResult<List<Client>>() { Success = false, Exception = e };
            }
        }

        public async Task<OperationResult<List<Client>>> ListUndeletedAsync()
        {
            try
            {
                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                var result = await _dao.ListAsync();
                result = result.Where(x => !x.IsDeleted).ToList();
                transactionScope.Complete();

                return new OperationResult<List<Client>>() { Success = true, Result = result };
            }
            catch (Exception e)
            {
                return new OperationResult<List<Client>>() { Success = false, Exception = e };
            }
        }

        #endregion

        #region FILTER

        public OperationResult<List<Client>> Filter(Func<Client, bool> predicate)
        {
            try
            {
                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                var result = _dao.List();
                result = result.Where(predicate).ToList();
                transactionScope.Complete();

                return new OperationResult<List<Client>>() { Result = result, Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult<List<Client>>() { Success = false, Exception = e };
            }
        }

        public async Task<OperationResult<List<Client>>> FilterAsync(Func<Client, bool> predicate)
        {
            try
            {
                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                var result = await _dao.ListAsync();
                result = result.Where(predicate).ToList();
                transactionScope.Complete();

                return new OperationResult<List<Client>>() { Result = result, Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult<List<Client>>() { Success = false, Exception = e };
            }
        }

        #endregion
    }
}