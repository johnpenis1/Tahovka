using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Tahovka
{
    public class Unit 
    {
        public string NAME;
        public int MAXHP;
        public int HP;
        public TextBlock healthDisplay;
        public TextBlock manaDisplay;
        public int MAXSP;
        public int SP;
        public int DEF;
        public int ATK;
        public int SDEF;
        public int SATK;
        public int SPEED;
        public Attack primaryAttack;
        public List<Attack> Spells = new List<Attack>();

        public Dictionary<string, Item> Items = new Dictionary<string, Item>();
        public Unit Target;

        public Unit(string name,int maxhp,TextBlock HealthDisplay,TextBlock ManaDisplay = null, int maxsp = 0, int def = 0, int atk = 1, int sdef = 0, int satk = 1, int speed = 1,Attack PrimaryAttack = null, List<Attack> spells = null, List<Item> items = null)
        {
            NAME = name;
            MAXHP = maxhp;
            HP = MAXHP;
            MAXSP = maxsp;
            SP = MAXSP;
            DEF = def;
            ATK = atk;
            SDEF = sdef;
            SATK = satk;
            SPEED = speed;
            Spells = spells;
            primaryAttack = PrimaryAttack;
            healthDisplay = HealthDisplay;
            manaDisplay = ManaDisplay;

            if (items != null) AddItems(items); 

            UpdateHealthDisplay();
            UpdateManaDisplay();
        }

        public Attack GetAttackFrom(string attackName)
        {


            foreach (Attack attack in Spells)
            {

                if (attack.Name.ToLower().Replace(" ","") == attackName.ToLower().Replace(" ", ""))
                {
                    Debug.WriteLine($"found attack: {attack.Name}");
                    return attack;
                }
            }

            return primaryAttack; // fallback
           
        }

        public void UpdateHealthDisplay()
        {
            HP = Utility.Clamp(HP, 0, MAXHP);
            healthDisplay.Text = $"{HP}/{MAXHP}";
        }
        public void UpdateManaDisplay()
        {
            if (manaDisplay == null) return; // if theres no mana textblock, dont try to update it

            SP = Utility.Clamp(SP,0, MAXSP);

            manaDisplay.Text = $"{SP}/{MAXSP}";
            
        }

        public void AddItem(Item item)
        {
            if (Items.ContainsKey(item.ID))
            {
                Items[item.ID].QUANTITY += item.QUANTITY; // if we already have the item, just increase the quantity
            }
            else Items[item.ID] = item;
            item.itemOwner = this;

        }

        public void AddItems(List<Item> items)
        {
            foreach (var item in items)
            {
                AddItem(item);
            }
        }

        public void TakeDamage(Unit attacker,Attack attack) 
        {

            int damageTaken = attack.BaseDmg + ((attack.Special ? attacker.SATK : attacker.ATK) * attack.PowerMult) - (attack.Special ? SDEF : DEF);
            HP -= damageTaken;
            HP = Math.Max(0, HP); // Clamp the health to be always above 0 (subnatica reference?)

            
            UpdateHealthDisplay();

            MainWindow.i.DisplayDialgue($"{attack.FlavorText}. The attack deals {damageTaken} damage to {NAME}.");

        }

        public async void AttackTarget(Attack attack)
        {

            if (HP < 1) return; // if your guts and organs are on the floor, you cant attack
             
            int loopsCompleted = 0;

            while (attack.Repeats > loopsCompleted)
            {
                Target.TakeDamage(this, attack); 
                                                                                                    
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
