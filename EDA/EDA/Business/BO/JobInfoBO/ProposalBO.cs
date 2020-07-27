using Recodme.Rd.EDA.BusinessLayer.OperationResults;
using Recodme.Rd.EDA.Data.JobInfo;
using Recodme.Rd.EDA.DataAccess.DAO.JobInfoDAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace Recodme.Rd.EDA.BusinessLayer.BO.JobInfoBO
{
    public class ProposalBO
    {
        protected readonly ProposalDAO _dao;

        TransactionOptions transactionOptions = new TransactionOptions
        {
            IsolationLevel = IsolationLevel.ReadCommitted,
            Timeout = TimeSpan.FromSeconds(30)
        };

        public ProposalBO()
        {
            _dao = new ProposalDAO();
        }


        /*
         * CRUD + LIST
         */

        #region CREATE

        public virtual OperationResult Create(Proposal proposal)
        {
            try
            {
                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                _dao.Create(proposal);
                transactionScope.Complete();

                return new OperationResult<List<Proposal>>() { Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult<List<Proposal>>() { Success = false, Exception = e };
            }
        }

        public async virtual Task<OperationResult> CreateAsync(Proposal proposal)
        {
            try
            {
                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                await _dao.CreateAsync(proposal);
                transactionScope.Complete();

                return new OperationResult<List<Proposal>>() { Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult<List<Proposal>>() { Success = false, Exception = e };
            }
        }

        #endregion

        #region READ

        public virtual OperationResult<Proposal> Read(Guid id)
        {
            try
            {
                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                var result = _dao.Read(id);
                transactionScope.Complete();

                return new OperationResult<Proposal>() { Success = true, Result = result };
            }
            catch (Exception e)
            {
                return new OperationResult<Proposal>() { Success = false, Exception = e };
            }
        }

        public async virtual Task<OperationResult<Proposal>> ReadAsync(Guid id)
        {
            try
            {
                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                var result = await _dao.ReadAsync(id);
                transactionScope.Complete();

                return new OperationResult<Proposal>() { Success = true, Result = result };
            }
            catch (Exception e)
            {
                return new OperationResult<Proposal>() { Success = false, Exception = e };
            }
        }

        #endregion

        #region UPDATE

        public virtual OperationResult Update(Proposal proposal)
        {
            try
            {
                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                _dao.Update(proposal);
                transactionScope.Complete();

                return new OperationResult<List<Proposal>>() { Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult<List<Proposal>>() { Success = false, Exception = e };
            }
        }

        public async virtual Task<OperationResult> UpdateAsync(Proposal proposal)
        {
            try
            {
                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                await _dao.UpdateAsync(proposal);
                transactionScope.Complete();

                return new OperationResult<List<Proposal>>() { Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult<List<Proposal>>() { Success = false, Exception = e };
            }
        }

        #endregion

        #region DELETE

        public virtual OperationResult Delete(Proposal proposal)
        {
            try
            {
                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                _dao.Delete(proposal);
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

        public async virtual Task<OperationResult> DeleteAsync(Proposal proposal)
        {
            try
            {
                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                await _dao.DeleteAsync(proposal);
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

        public virtual OperationResult<List<Proposal>> List()
        {
            try
            {
                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                var result = _dao.List();
                transactionScope.Complete();

                return new OperationResult<List<Proposal>>() { Success = true, Result = result };
            }
            catch (Exception e)
            {
                return new OperationResult<List<Proposal>>() { Success = false, Exception = e };
            }
        }

        public async virtual Task<OperationResult<List<Proposal>>> ListAsync()
        {
            try
            {
                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                var result = await _dao.ListAsync();
                transactionScope.Complete();

                return new OperationResult<List<Proposal>>() { Success = true, Result = result };
            }
            catch (Exception e)
            {
                return new OperationResult<List<Proposal>>() { Success = false, Exception = e };
            }
        }

        #endregion

        #region LIST UNDELETED

        public OperationResult<List<Proposal>> ListUndeleted()
        {
            try
            {
                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                var result = _dao.List().Where(x => !x.IsDeleted).ToList();
                transactionScope.Complete();

                return new OperationResult<List<Proposal>>() { Success = true, Result = result };
            }
            catch (Exception e)
            {
                return new OperationResult<List<Proposal>>() { Success = false, Exception = e };
            }
        }

        public async Task<OperationResult<List<Proposal>>> ListUndeletedAsync()
        {
            try
            {
                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                var result = await _dao.ListAsync();
                result = result.Where(x => !x.IsDeleted).ToList();
                transactionScope.Complete();

                return new OperationResult<List<Proposal>>() { Success = true, Result = result };
            }
            catch (Exception e)
            {
                return new OperationResult<List<Proposal>>() { Success = false, Exception = e };
            }
        }

        #endregion

        #region FILTER

        public OperationResult<List<Proposal>> Filter(Func<Proposal, bool> predicate)
        {
            try
            {
                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                var result = _dao.List();
                result = result.Where(predicate).ToList();
                transactionScope.Complete();

                return new OperationResult<List<Proposal>>() { Result = result, Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult<List<Proposal>>() { Success = false, Exception = e };
            }
        }

        public async Task<OperationResult<List<Proposal>>> FilterAsync(Func<Proposal, bool> predicate)
        {
            try
            {
                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                var result = await _dao.ListAsync();
                result = result.Where(predicate).ToList();
                transactionScope.Complete();

                return new OperationResult<List<Proposal>>() { Result = result, Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult<List<Proposal>>() { Success = false, Exception = e };
            }
        }

        #endregion
    }
}