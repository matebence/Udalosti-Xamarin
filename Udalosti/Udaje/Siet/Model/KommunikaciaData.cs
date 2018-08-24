using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Udalosti.Udaje.Siet.Model.Udalost;

namespace Udalosti.Udaje.Siet.Model
{
    interface KommunikaciaData
    {
        Task dataZoServeraAsync(String odpoved, String od, List<ZoznamUdalosti> udaje);
    }
}