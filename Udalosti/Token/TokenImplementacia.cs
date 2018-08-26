using System.Threading.Tasks;

namespace Udalosti.Token
{
    interface TokenImplementacia
    {
        Task zrusTokenAsync();
        Task novyTokenAsync();
    }
}