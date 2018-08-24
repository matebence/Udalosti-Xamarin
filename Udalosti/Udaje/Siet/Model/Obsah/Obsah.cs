using System.Collections.Generic;
using Udalosti.Udaje.Siet.Model.Udalost;

namespace Udalosti.Udaje.Siet.Model.Obsah
{
    public class Obsah
    {
        public Obsah(List<ZoznamUdalosti> udalosti)
        {
            this.udalosti = udalosti;
        }

        public List<ZoznamUdalosti> udalosti { get; set; }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}