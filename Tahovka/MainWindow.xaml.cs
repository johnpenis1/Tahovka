using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace Tahovka
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            Player Guillotina = new Player(10000,0,10,30,15,10,1 );
            Player player = new Player(2000, 400, 30, 50, 50, 30,50);
            ///
            ///
            ///
            Items Jerky = new Items("jerky", 1500, 0, 0, 3, "A piece of Jerky, Heals 1500 hp"); Items Soda = new Items("soda", 0, 200, 0, 2, "A Diet Soda, Restores 200 SP"); Items Dynamite = new Items("dynamite", 0, 0, 2000, 2, "A powerful stick of dynamite, deals 2000 damage"); Items Coffee = new Items("coffee", player.MAXHP, player.MAXSP, 0, 1,"A Quadruple Espresso, Restores SP and HP to max");
            ///
            ///
            ///
            Attacks Punch = new Attacks(20,2,0,false,1,"","You Punch with all your might"); Attacks Fireball = new Attacks(50,2,30,true,1,"a magic attack that uses SP damage, costs 30 mana", "You fire off a fireball, fire."); Attacks Flurry = new Attacks(15,2,50,false,5,"a weak attack that hits multiple times, Costs 50 Mana", "You unleash a flurry of blows"); Attacks FistOfFaith = new Attacks(100,3,100,false,1,"a High damage one hit attack", "You Channel all your Chi into the attack"); Attacks FivePointFingerHeartExplodingTechnique = new Attacks(999999,10,0,true,10,"attack that instantly kills the unit, costs nothing and is for testing (you cheater!)", "DIIIIIIIE!!!"); // these are your attacks
            Attacks Scratch = new Attacks(20, 2, 0, false, 1,"","Guillotina scratches you!");
        }
        public string dialogue = "";
        public string flavortext = "";
        public int helpfulvalue = 0;
        public int helpfulvariableforloops = 0;
        public int iwon = 0;
        public int ilost = 0;
        public int insight = 0;
        public string dmgvaluetranslator = "";
        public void HideCombatMenu(object sender, EventArgs e)
        {
            DefendButton.Visibility = Visibility.Collapsed;
            AttackButton.Visibility = Visibility.Collapsed;
            MagicButton.Visibility = Visibility.Collapsed;
            ItemsButton.Visibility = Visibility.Collapsed;
            MagicMenu.Visibility = Visibility.Collapsed;
            ItemMenu.Visibility = Visibility.Collapsed;
        }
        public void ShowCombatMenu(object sender, EventArgs e)
        {
            DefendButton.Visibility = Visibility.Visible;
            AttackButton.Visibility = Visibility.Visible;
            MagicButton.Visibility = Visibility.Visible;
            ItemsButton.Visibility = Visibility.Visible;
        }
        public void HideMagicMenu(object sender, EventArgs e)
        {
            MagicMenu.Visibility = Visibility.Collapsed;
        }
        public void ShowMagicMenu(object sender, EventArgs e)
        {
            MagicMenu.Visibility = Visibility.Visible;
        }
        public void MagicShowcase(object sender, EventArgs e)
        {
            ShowMagicMenu(sender, e);
            HideItemMenu(sender, e);
        }
        public void HideItemMenu(object sender, EventArgs e)
        {
            ItemMenu.Visibility = Visibility.Collapsed;
        }
        public void ShowItemMenu(object sender, EventArgs e)
        {
            ItemMenu.Visibility = Visibility.Visible;
        }
        public void ItemShowcase(object sender, EventArgs e)
        {
            ShowItemMenu(sender, e);
            HideMagicMenu(sender, e);
        }
        public static void Flavor(string desc, string flavortext)
        {
            desc = flavortext;
        }

        public void CombatProcess(object sender, EventArgs e, int Special,int ESpecial,int helpvalue, int loops, int helpfulloops, int health, int ehealth, int sp, int maxsp, int manacost, int basicdamage, int ebasicdamage,  int atkstat, int eatkstat, int mult, int emult, int defense, int edefense, int Sdefense, int esdefense, int Sattack, int esattack, int speed, int espeed, string dialog, int insight = 0 )
            {
            helpvalue = Special + ESpecial;
            if (sp-manacost >= 0)
            {
                sp -= manacost;
                switch (helpvalue)
                {
                    case 0:
                        if (speed >= espeed)//Checking for Speed
                        {
                            while (loops > helpfulloops)
                            {
                                Player.Attack(ehealth, basicdamage, atkstat, mult, edefense, insight, dialog); //this is the player attacking
                                Thread.Sleep();
                                helpfulloops++;
                            }
                            helpfulloops = 0;
                            Player.EnemyAttack(health, ebasicdamage, eatkstat, emult, defense, insight, dialog); //this is the enemy attacking

                        }
                        else
                        {
                            Player.EnemyAttack(health, ebasicdamage, eatkstat, emult, defense, insight, dialog);
                            while (loops > helpfulloops)
                            {
                                Player.Attack(ehealth, basicdamage, atkstat, mult, edefense, insight, dialog); //this is the player attacking
                                helpfulloops++;//this is the enemy attacking 
                            }
                            helpfulloops = 0;
                        }
                        break;

                    case 1:
                        if (Special == 1) // this is checking for who is using a special attack
                        {
                            if (speed >= espeed)//Checking for Speed
                            {
                                while (loops > helpfulloops)
                                {
                                    Player.Attack(ehealth, basicdamage, Sattack, mult, esdefense, insight, dialog); //this is the player attacking
                                    helpfulloops++;
                                    Console.WriteLine("Dealt " + insight + " damage'");
                                }
                                Player.EnemyAttack(health, ebasicdamage, eatkstat, emult, defense, insight, dialog); //this is the enemy attacking
                            }
                            else
                            {
                                Player.EnemyAttack(health, ebasicdamage, eatkstat, emult, defense, insight, dialog); //this is the enemy attacking
                                while (loops > helpfulloops)
                                {
                                    Player.Attack(ehealth, basicdamage, Sattack, mult, esdefense, insight, dialog); //this is the player attacking
                                    helpfulloops++;
                                }
                                helpfulloops = 0;
                            }
                            break;
                        }
                        else
                        {
                            if (speed >= espeed)//Checking for Speed
                            {
                                while (loops > helpfulloops)
                                {
                                    Player.Attack(ehealth, basicdamage, atkstat, mult, edefense, insight, dialog); //this is the player attacking
                                    helpfulloops++;
                                }
                                helpfulloops = 0;
                                Player.EnemyAttack(health, ebasicdamage, esattack, emult, Sdefense, insight, dialog); //this is the enemy attacking
                            }
                            else
                            {
                                Player.EnemyAttack(health, ebasicdamage, esattack, emult, Sdefense, insight, dialog); //this is the enemy attacking
                                while (loops > helpfulloops)
                                {
                                    Player.Attack(ehealth, basicdamage, atkstat, mult, edefense, insight, dialog); //this is the player attacking
                                    helpfulloops++;
                                }
                                helpfulloops = 0;
                            }
                            break;
                        }
                    case 2:
                        if (speed >= espeed)//Checking for Speed
                        {
                            while (loops > helpfulloops)
                            {
                                Player.Attack(ehealth, basicdamage, Sattack, mult, esdefense, insight, dialog); //this is the player attacking
                                helpfulloops++;
                            }
                            Player.EnemyAttack(health, ebasicdamage, esattack, emult, Sdefense, insight, dialog); //this is the enemy attacking
                            helpfulloops = 0;
                        }
                        else
                        {
                            Player.EnemyAttack(health, ebasicdamage, eatkstat, emult, esdefense, insight, dialog); //this is the enemy attacking
                            while (loops > helpfulloops)
                            {
                                Player.Attack(ehealth, basicdamage, atkstat, mult, Sdefense, insight, dialog); //this is the player attacking
                                helpfulloops++;
                            }
                            helpfulloops = 0;
                        }
                        break;
                    default:
                        break;
                }
            }
            else
            {
                dialog = "You dont have enough mana for this!";
            }
 
}
            
        }

        /// This is the Template player.HP = (Player.Attack(enemy.HP, Punch.BaseDmg, player.ATK, Punch.PowerMult, enemy.DEF));
        ///
        ///
    }
    
    

