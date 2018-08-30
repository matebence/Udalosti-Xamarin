using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Udalosti.Udaje.Zdroje;

namespace Udalosti.Udaje.Siet.Model
{
    interface KommunikaciaData
    {
        Task dataZoServeraAsync(String odpoved, String od, List<ObsahUdalosti> udaje);
    }
}