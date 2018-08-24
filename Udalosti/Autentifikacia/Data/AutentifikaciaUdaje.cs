using System.Threading.Tasks;

namespace Udalosti.Autentifikacia.Data
{
    class AutentifikaciaUdaje : AutentifikaciaImplementacia
    {
        public Task miestoPrihlaseniaAsync(string email, string heslo)
        {
            throw new System.NotImplementedException();
        }

        public Task prihlasenieAsync(string email, string heslo, string stat, string okres, string mesto)
        {
            throw new System.NotImplementedException();
        }

        public Task registraciaAsync(string meno, string email, string heslo, string potvrd)
        {
            throw new System.NotImplementedException();
        }

        public void ucetJeNePristupny(string email)
        {
            throw new System.NotImplementedException();
        }

        public void ulozPrihlasovacieUdajeDoDatabazy(string email, string heslo, string token)
        {
            throw new System.NotImplementedException();
        }
    }
}