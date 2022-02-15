﻿using Flone.Data.Repository.DAL;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Flone.Web.Helper
{
    public class ActionTransactionHelper : IActionTransactionHelper
    {
        private IUnitOfWork _uow;
        private ITransaction? _tx;
        private readonly ILogger _log;
        private bool TransactionHandled { get; set; }
        private bool SessionClosed { get; set; }

        public ActionTransactionHelper(IUnitOfWork uow, ILogger log)
        {
            _uow = uow;
            _log = log;
        }
        public void BeginTransaction()
        {
            _tx = _uow.BeginTransaction();
        }

        public void EndTransaction(ActionExecutedContext filterContext)
        {
            if (_tx == null) throw new NotSupportedException();
            if (filterContext.Exception == null)
            {
                _uow.Commit();
                _tx.Commit();
            }
            else
            {
                try
                {
                    _tx.Rollback();
                }
                catch (Exception ex)
                {
                    throw new AggregateException(filterContext.Exception, ex);
                }

            }

            TransactionHandled = true;
        }


        public void CloseSession()
        {
            if (_tx != null)
            {
                _tx.Dispose();
                _tx = null;
            }

            if (_uow != null)
            {
                _uow.Dispose();
                _uow = null;
            }

            SessionClosed = true;
        }

    }
}
