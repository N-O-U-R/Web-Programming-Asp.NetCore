using Proje.Models;

namespace Proje.Repositories.Abstract
{
    public interface IUserAuthenticationService
    {
        Task<Status> LoginAsync(Login model);
        Task<Status> RegisterAsync(Register model ); 
        Task LogoutAsync ();

        Task<Status> ChangePasswordAsync(ChangePasswordModel model, string username);
    }
}
