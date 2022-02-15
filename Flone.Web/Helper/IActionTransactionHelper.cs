using Microsoft.AspNetCore.Mvc.Filters;

namespace Flone.Web.Helper
{
    public interface IActionTransactionHelper
    {
        void BeginTransaction();
        void EndTransaction(ActionExecutedContext filterContext);
        void CloseSession();
    }
}
