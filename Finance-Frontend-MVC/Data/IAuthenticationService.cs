using IdentityModel.Client;
using System.Net.Http;
using System.Threading.Tasks;

namespace Finance_Frontend_MVC.Data
{
    public interface IAuthenticationService
    {
       

        Task<TokenResponse> GetAPIToken();
        Task<HttpClient> GetClient();
    }
}