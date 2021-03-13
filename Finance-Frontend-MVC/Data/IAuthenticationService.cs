using IdentityModel.Client;
using System.Net.Http;
using System.Threading.Tasks;

namespace Finance_Frontend_MVC.Data
{
    public interface IAuthenticationService
    {
        HttpClient httpClient { get; set; }

        Task<TokenResponse> GetAPIToken();
    }
}