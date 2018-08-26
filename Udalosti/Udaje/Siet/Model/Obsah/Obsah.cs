using System.Collections.Generic;
using Udalosti.Udaje.Siet.Model.Udalost;

namespace Udalosti.Udaje.Siet.Model.Obsah
{
    public class Obsah
    {
        public Obsah(List<Udalost.Udalost> udalosti)
        {
            this.udalosti = udalosti;
        }

        public List<Udalost.Udalost> udalosti { get; set; }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}