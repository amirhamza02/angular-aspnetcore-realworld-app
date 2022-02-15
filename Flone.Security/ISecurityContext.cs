using Flone.Data.Models;

namespace Flone.Security
{
    public interface ISecurityContext
    {
        User User { get; }
        bool IsAdministrator { get; }
    }
}
