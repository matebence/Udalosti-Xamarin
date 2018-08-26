using System.Collections.Generic;
using Udalosti.Udaje.Zdroje;

namespace Udalosti.Udaje.Siet.Model.Obsah
{
    public class Obsah
    {
        public Obsah(List<ObsahUdalosti> udalosti)
        {
            this.udalosti = udalosti;
        }

        public List<ObsahUdalosti> udalosti { get; set; }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}