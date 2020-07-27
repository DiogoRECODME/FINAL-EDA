using Recodme.Rd.EDA.BusinessLayer.OperationResults;
using Recodme.Rd.EDA.Data.ExtraInfo;
using Recodme.Rd.EDA.Data.JobInfo;
using Recodme.Rd.EDA.DataAccess.DAO.ExtraInfoDAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Recodme.Rd.EDA.BusinessLayer.BO.ExtraInfoBO
{
    public class CommentBO
    {
        protected readonly CommentDAO _dao;

        TransactionOptions transactionOptions = new TransactionOptions
        {
            IsolationLevel = IsolationLevel.ReadCommitted,
            Timeout = TimeSpan.FromSeconds(30)
        };

        public CommentBO()
        {
            _dao = new CommentDAO();
        }


        /*
         * CRUD + LIST
         */

        #region CREATE

        public virtual OperationResult Create(Comment comment)
        {
            try
            {
                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                _dao.Create(comment);
                transactionScope.Complete();

                return new OperationResult<List<Comment>>() { Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult<List<Comment>>() { Success = false, Exception = e };
            }
        }

        public async virtual Task<OperationResult> CreateAsync(Comment comment)
        {
            try
            {
                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                await _dao.CreateAsync(comment);
                transactionScope.Complete();

                return new OperationResult<List<Comment>>() { Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult<List<Comment>>() { Success = false, Exception = e };
            }
        }

        #endregion

        #region READ

        public virtual OperationResult<Comment> Read(Guid id)
        {
            try
            {
                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                var result = _dao.Read(id);
                transactionScope.Complete();

                return new OperationResult<Comment>() { Success = true, Result = result };
            }
            catch (Exception e)
            {
                return new OperationResult<Comment>() { Success = false, Exception = e };
            }
        }

        public async virtual Task<OperationResult<Comment>> ReadAsync(Guid id)
        {
            try
            {
                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                var result = await _dao.ReadAsync(id);
                transactionScope.Complete();

                return new OperationResult<Comment>() { Success = true, Result = result };
            }
            catch (Exception e)
            {
                return new OperationResult<Comment>() { Success = false, Exception = e };
            }
        }

        #endregion

        #region UPDATE

        public virtual OperationResult Update(Comment comment)
        {
            try
            {
                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                _dao.Update(comment);
                transactionScope.Complete();

                return new OperationResult<List<Comment>>() { Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult<List<Comment>>() { Success = false, Exception = e };
            }
        }

        public async virtual Task<OperationResult> UpdateAsync(Comment comment)
        {
            try
            {
                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                await _dao.UpdateAsync(comment);
                transactionScope.Complete();

                return new OperationResult<List<Comment>>() { Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult<List<Comment>>() { Success = false, Exception = e };
            }
        }

        #endregion

        #region DELETE

        public virtual OperationResult Delete(Comment comment)
        {
            try
            {
                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                _dao.Delete(comment);
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

        public async virtual Task<OperationResult> DeleteAsync(Comment comment)
        {
            try
            {
                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                await _dao.DeleteAsync(comment);
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

        public virtual OperationResult<List<Comment>> List()
        {
            try
            {
                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                var result = _dao.List();
                transactionScope.Complete();

                return new OperationResult<List<Comment>>() { Success = true, Result = result };
            }
            catch (Exception e)
            {
                return new OperationResult<List<Comment>>() { Success = false, Exception = e };
            }
        }

        public async virtual Task<OperationResult<List<Comment>>> ListAsync()
        {
            try
            {
                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                var result = await _dao.ListAsync();
                transactionScope.Complete();

                return new OperationResult<List<Comment>>() { Success = true, Result = result };
            }
            catch (Exception e)
            {
                return new OperationResult<List<Comment>>() { Success = false, Exception = e };
            }
        }

        #endregion

        #region LIST UNDELETED

        public OperationResult<List<Comment>> ListUndeleted()
        {
            try
            {
                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                var result = _dao.List().Where(x => !x.IsDeleted).ToList();
                transactionScope.Complete();

                return new OperationResult<List<Comment>>() { Success = true, Result = result };
            }
            catch (Exception e)
            {
                return new OperationResult<List<Comment>>() { Success = false, Exception = e };
            }
        }

        public async Task<OperationResult<List<Comment>>> ListUndeletedAsync()
        {
            try
            {
                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                var result = await _dao.ListAsync();
                result = result.Where(x => !x.IsDeleted).ToList();
                transactionScope.Complete();

                return new OperationResult<List<Comment>>() { Success = true, Result = result };
            }
            catch (Exception e)
            {
                return new OperationResult<List<Comment>>() { Success = false, Exception = e };
            }
        }

        #endregion

        #region FILTER

        public OperationResult<List<Comment>> Filter(Func<Comment, bool> predicate)
        {
            try
            {
                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                var result = _dao.List();
                result = result.Where(predicate).ToList();
                transactionScope.Complete();

                return new OperationResult<List<Comment>>() { Result = result, Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult<List<Comment>>() { Success = false, Exception = e };
            }
        }

        public async Task<OperationResult<List<Comment>>> FilterAsync(Func<Comment, bool> predicate)
        {
            try
            {
                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                var result = await _dao.ListAsync();
                result = result.Where(predicate).ToList();
                transactionScope.Complete();

                return new OperationResult<List<Comment>>() { Result = result, Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult<List<Comment>>() { Success = false, Exception = e };
            }
        }

        #endregion
    }
}