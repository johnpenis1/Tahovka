using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace Tahovka
{
    internal class Player 
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
        public Player(int maxhp, int maxsp, int def, int atk, int sdef, int satk, int speed)
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

        }
        public static void Attack(int health, int basicdamage, int atkstat, int mult, int defense, int informativedmgvalue, string dialog) //damage 
        {
            {
                health -= basicdamage + ((atkstat * mult) - defense);
                informativedmgvalue = basicdamage + ((atkstat * mult) - defense);
            }
        }
        public static void EnemyAttack(int ehealth, int ebasicdamage, int eatkstat, int emult, int edefense,int informativedmgvalue, string dialog) //might change this later, unsure
        {
            {
                ehealth -= ebasicdamage + ((eatkstat * emult) - edefense);
                informativedmgvalue = ebasicdamage + ((eatkstat * emult) - edefense);
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
