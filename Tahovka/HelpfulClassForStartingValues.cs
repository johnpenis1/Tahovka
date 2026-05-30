using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;

namespace Tahovka
{
    internal class Items
    {
        public string ID { get; set; }
        public int HPVALUE { get; set; }
        public int SPVALUE { get; set; }
        public int DMGVALUE { get; set; }
        public int QUANTITY { get; set; }
        public string FLAVOR { get; set; }
        public Items(string id, int hp, int sp, int dmg, int quantity, string flavor )
        {
            ID = id;
            HPVALUE = hp;
            SPVALUE = sp;
            DMGVALUE = dmg;
            QUANTITY = quantity;
            FLAVOR = flavor;
        }
        public static void UseItem(int hp, int sp, int dmg, int playersp, int playermaxsp, int playerhp, int maxhp, int enemyhp, int enemymaxhp, string dialog, string id, bool iwon, bool ilost)
        {
            switch(id)
            {
                case "jerky":
                    dialog = "You used a Jerky! Healed" + hp;
                    playerhp += hp;
                    Player.CheckCondition(playerhp, maxhp, playersp, playermaxsp, enemyhp, enemymaxhp, iwon, ilost);
                    break;
                case "soda":
                    dialog = "You drank a soda! Healed" + sp;
                    playersp += sp;
                    Player.CheckCondition(playerhp, maxhp, playersp, playermaxsp, enemyhp, enemymaxhp, iwon, ilost);
                    break;
                case "coffee":
                    dialog = "You Drank coffee! SP and HP fully restored!";
                    playersp += sp;
                    playerhp += hp;
                    Player.CheckCondition(playerhp, maxhp, playersp, playermaxsp, enemyhp, enemymaxhp, iwon, ilost);
                    break;
                case "dynamite":
                    dialog ="You threw a stick of dynamite! enemy took "+ dmg + "damage!";
                    enemyhp -= dmg;
                    Player.CheckCondition(playerhp, maxhp, playersp, playermaxsp, enemyhp, enemymaxhp, iwon, ilost);
                    break;
                default: break;
            }
                
        }
    }
}
