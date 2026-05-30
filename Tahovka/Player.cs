using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace Tahovka
{
    public class Player 
    {
        public int MAXHP;
        public int HP;
        public int MAXSP;
        public int SP;
        public int DEF;
        public int ATK;
        public int SDEF;
        public int SATK;
        public int SPEED;

        public List<Attack> Attacks = new List<Attack>();

        public Player(int maxhp, int maxsp, int def, int atk, int sdef, int satk, int speed, List<Attack> attacks)
        {
            MAXHP = maxhp;
            HP = MAXHP;
            MAXSP = maxsp;
            SP = MAXSP;
            DEF = def;
            ATK = atk;
            SDEF = sdef;
            SATK = satk;
            SPEED = speed;
            Attacks = attacks;

        }
        public void TakeDamage(Player attacker,Attack attack, int mult, ref int damageTaken) //damage 
        {
            damageTaken = attack.BaseDmg + ((attack.Special ? attacker.SATK : attacker.ATK) * mult) - (attack.Special ? SDEF : DEF);
            HP -= damageTaken;
        }

        public void Attack(Player target,Attack attack, ref int insight)
        {
            int loopsCompleted = 0;

            while (attack.Repeats > loopsCompleted)
            {
                target.TakeDamage(this, attack, attack.PowerMult, ref insight); 
                                                                                                    
                loopsCompleted++;
            }

           
        }

        public static void Defend(int health, int basicdamage, int atkstat, int mult, int defense, int informativedamagevalue = 0)
        {
            {
                health -= basicdamage + ((atkstat * mult) - defense)/3;
                informativedamagevalue = basicdamage + ((atkstat * mult) - defense)/ 3;
            }
        }
        public static void CheckCondition(int playerhp, int playermaxhp, int playersp, int playermaxsp, int enemyhp, int enemymaxhp, bool iwon, bool ilost)
        {
            if (playerhp > playermaxhp)
            {
                 playerhp = playermaxhp;
            }
            else if (playerhp <= 0)
            {
                 ilost = true;
            }
            if (playersp > playermaxsp)
            {
                 playersp = playermaxsp;
            }
            if (enemyhp > enemymaxhp)
            {
                 enemyhp = enemymaxhp;
            }
            else if (enemyhp <= 0)
            {
                  iwon = true;
            }
            else
            {
                 
            }
        }
        public static void DidWeWin(bool iwon, bool ilost)
        {
            if (iwon == true)
            {

            }
            else if (ilost == true)
            {

            }
            else
            {

            }
        }
    }
}
