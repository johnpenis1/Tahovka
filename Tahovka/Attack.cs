using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tahovka
{
    public class Attack
    {
        public int BaseDmg { get; set; }
        public int PowerMult { get; set; }
        public int ManaValue { get; set; }
        public bool Special { get; set; }
        public int Repeats { get; set; }
        public string FlavorText { get; set; }
        public string Description { get; set; }
        public Attack(int basedmg, int powermult, int manavalue, bool special, int repeats, string desc, string flavor )
        {
            BaseDmg = basedmg;
            PowerMult = powermult;
            ManaValue = manavalue;
            Special = special;
            Repeats = repeats;
            Description = desc;
            FlavorText = flavor;
            
        }
    }
}
