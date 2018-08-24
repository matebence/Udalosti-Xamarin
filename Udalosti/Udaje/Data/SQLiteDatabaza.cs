using System.Collections.Generic;
using System.Threading.Tasks;
using Udalosti.Udaje.Data.Tabulka;

namespace Udalosti.Udaje.Data
{
    class SQLiteDatabaza : SQLDataImplementacia
    {
        public void aktualizujMiestoPrihlasenia(Miesto miesto, int idMiesto)
        {
            throw new System.NotImplementedException();
        }

        public void aktualizujPouzivatelskeUdaje(Pouzivatelia pouzivatelia, string email)
        {
            throw new System.NotImplementedException();
        }

        public bool miestoPrihlasenia()
        {
            throw new System.NotImplementedException();
        }

        public void noveMiestoPrihlasenia(Miesto miesto)
        {
            throw new System.NotImplementedException();
        }

        public void novePouzivatelskeUdaje(Pouzivatelia pouzivatelia)
        {
            throw new System.NotImplementedException();
        }

        public void odstranMiestoPrihlasenia(int idMiesto)
        {
            throw new System.NotImplementedException();
        }

        public void odstranPouzivatelskeUdaje(string email)
        {
            throw new System.NotImplementedException();
        }

        public bool pouzivatelskeUdaje()
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> tabulkaExistuje(string fileName)
        {
            throw new System.NotImplementedException();
        }

        public Dictionary<string, string> vratAktualnehoPouzivatela()
        {
            throw new System.NotImplementedException();
        }

        public Dictionary<string, string> vratMiestoPrihlasenia()
        {
            throw new System.NotImplementedException();
        }

        public void VyvorDatabazu()
        {
            throw new System.NotImplementedException();
        }
    }
}