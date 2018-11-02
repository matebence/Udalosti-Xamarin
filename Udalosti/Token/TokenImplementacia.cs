using System.Threading.Tasks;

namespace Udalosti.Token
{
    interface TokenImplementacia
    {
        Task zrusToken();

        Task novyToken();
    }
}