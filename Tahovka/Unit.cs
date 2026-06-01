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

                if (Utility.CleanseString(attackName) == Utility.CleanseString(attack.Name))
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
        public void TakeDamage(int baseDamage,bool special = false) 
        {

            int damageTaken = baseDamage - (special ? SDEF : DEF);
            HP -= damageTaken;
            HP = Math.Max(0, HP); // Clamp the health to be always above 0 (subnatica reference?)

            
            UpdateHealthDisplay();


        }

        public async void AttackTarget(Attack attack)
        {

            if (HP < 1) return; // if your guts and organs are on the floor, you cant attack
             
            int loopsCompleted = 0;

            while (attack.Repeats > loopsCompleted)
            {
                Target.TakeDamage(this, attack); 
                                                                                                    
                loopsCompleted++;

                await Task.Delay(200);
            }

           
        }

        public async void Defend(Unit attacker, Attack attack)
        {
            {
                int damageTaken = attack.BaseDmg + ((attack.Special ? attacker.SATK : attacker.ATK) * attack.PowerMult) / 3 - (attack.Special ? SDEF : DEF);
                HP -= damageTaken;
                HP = Math.Max(0, HP); // its DEFO a subnautica reference

                UpdateHealthDisplay();

                MainWindow.i.DisplayDialgue($"{attack.FlavorText} but you guard! Dealt {damageTaken} damage to {NAME}!");
            } // i could probably just make the text not account for the enemy guarding since they have no need for that
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

        public override string ToString()
        {
            return NAME;
        }
    }
}
