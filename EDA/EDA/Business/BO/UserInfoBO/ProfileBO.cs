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
    public class ProfileBO
    {
        protected readonly ProfileDAO _dao;

        TransactionOptions transactionOptions = new TransactionOptions
        {
            IsolationLevel = IsolationLevel.ReadCommitted,
            Timeout = TimeSpan.FromSeconds(30)
        };

        public ProfileBO()
        {
            _dao = new ProfileDAO();
        }


        /*
         * CRUD + LIST
         */

        #region CREATE

        public virtual OperationResult Create(Profile profile)
        {
            try
            {
                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                _dao.Create(profile);
                transactionScope.Complete();

                return new OperationResult<List<Profile>>() { Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult<List<Profile>>() { Success = false, Exception = e };
            }
        }

        public async virtual Task<OperationResult> CreateAsync(Profile profile)
        {
            try
            {
                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                await _dao.CreateAsync(profile);
                transactionScope.Complete();

                return new OperationResult<List<Profile>>() { Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult<List<Profile>>() { Success = false, Exception = e };
            }
        }

        #endregion

        #region READ

        public virtual OperationResult<Profile> Read(Guid id)
        {
            try
            {
                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                var result = _dao.Read(id);
                transactionScope.Complete();

                return new OperationResult<Profile>() { Success = true, Result = result };
            }
            catch (Exception e)
            {
                return new OperationResult<Profile>() { Success = false, Exception = e };
            }
        }

        public async virtual Task<OperationResult<Profile>> ReadAsync(Guid id)
        {
            try
            {
                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                var result = await _dao.ReadAsync(id);
                transactionScope.Complete();

                return new OperationResult<Profile>() { Success = true, Result = result };
            }
            catch (Exception e)
            {
                return new OperationResult<Profile>() { Success = false, Exception = e };
            }
        }

        #endregion

        #region UPDATE

        public virtual OperationResult Update(Profile profile)
        {
            try
            {
                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                _dao.Update(profile);
                transactionScope.Complete();

                return new OperationResult<List<Profile>>() { Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult<List<Profile>>() { Success = false, Exception = e };
            }
        }

        public async virtual Task<OperationResult> UpdateAsync(Profile profile)
        {
            try
            {
                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                await _dao.UpdateAsync(profile);
                transactionScope.Complete();

                return new OperationResult<List<Profile>>() { Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult<List<Profile>>() { Success = false, Exception = e };
            }
        }

        #endregion

        #region DELETE

        public virtual OperationResult Delete(Profile profile)
        {
            try
            {
                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                _dao.Delete(profile);
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

        public async virtual Task<OperationResult> DeleteAsync(Profile profile)
        {
            try
            {
                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                await _dao.DeleteAsync(profile);
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

        public virtual OperationResult<List<Profile>> List()
        {
            try
            {
                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                var result = _dao.List();
                transactionScope.Complete();

                return new OperationResult<List<Profile>>() { Success = true, Result = result };
            }
            catch (Exception e)
            {
                return new OperationResult<List<Profile>>() { Success = false, Exception = e };
            }
        }

        public async virtual Task<OperationResult<List<Profile>>> ListAsync()
        {
            try
            {
                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                var result = await _dao.ListAsync();
                transactionScope.Complete();

                return new OperationResult<List<Profile>>() { Success = true, Result = result };
            }
            catch (Exception e)
            {
                return new OperationResult<List<Profile>>() { Success = false, Exception = e };
            }
        }

        #endregion

        #region LIST UNDELETED

        public OperationResult<List<Profile>> ListUndeleted()
        {
            try
            {
                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                var result = _dao.List().Where(x => !x.IsDeleted).ToList();
                transactionScope.Complete();

                return new OperationResult<List<Profile>>() { Success = true, Result = result };
            }
            catch (Exception e)
            {
                return new OperationResult<List<Profile>>() { Success = false, Exception = e };
            }
        }

        public async Task<OperationResult<List<Profile>>> ListUndeletedAsync()
        {
            try
            {
                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                var result = await _dao.ListAsync();
                result = result.Where(x => !x.IsDeleted).ToList();
                transactionScope.Complete();

                return new OperationResult<List<Profile>>() { Success = true, Result = result };
            }
            catch (Exception e)
            {
                return new OperationResult<List<Profile>>() { Success = false, Exception = e };
            }
        }

        #endregion

        #region FILTER

        public OperationResult<List<Profile>> Filter(Func<Profile, bool> predicate)
        {
            try
            {
                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                var result = _dao.List();
                result = result.Where(predicate).ToList();
                transactionScope.Complete();

                return new OperationResult<List<Profile>>() { Result = result, Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult<List<Profile>>() { Success = false, Exception = e };
            }
        }

        public async Task<OperationResult<List<Profile>>> FilterAsync(Func<Profile, bool> predicate)
        {
            try
            {
                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                var result = await _dao.ListAsync();
                result = result.Where(predicate).ToList();
                transactionScope.Complete();

                return new OperationResult<List<Profile>>() { Result = result, Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult<List<Profile>>() { Success = false, Exception = e };
            }
        }

        #endregion
    }
}