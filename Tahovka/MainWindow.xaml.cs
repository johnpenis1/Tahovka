using System;
using System.Collections.Generic;
using System.ComponentModel;
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
            DataContext = this;



            Attack Punch = new Attack(20,2,0,false,1,"","You Punch with all your might"); 
            Attack Fireball = new Attack(50,2,30,true,1,"a magic attack that uses SP damage, costs 30 mana", "You fire off a fireball, fire."); 
            Attack Flurry = new Attack(15,2,50,false,5,"a weak attack that hits multiple times, Costs 50 Mana", "You unleash a flurry of blows");
            Attack FistOfFaith = new Attack(100,3,100,false,1,"a High damage one hit attack", "You Channel all your Chi into the attack"); 
            Attack FivePointFingerHeartExplodingTechnique = new Attack(999999,10,0,true,10,"attack that instantly kills the unit, costs nothing and is for testing (you cheater!)", "DIIIIIIIE!!!"); // these are your attacks
            Attack Scratch = new Attack(20, 2, 0, false, 1,"","Guillotina scratches you!");

            Player Guillotina = new Player(10000, 0, 10, 30, 15, 10, 1, new List<Attack>() { Scratch });
            Player player = new Player(2000, 400, 30, 50, 50, 30, 50, new List<Attack>() { Punch,Fireball,FistOfFaith,FivePointFingerHeartExplodingTechnique });


            Items Jerky = new Items("jerky", 1500, 0, 0, 3, "A piece of Jerky, Heals 1500 hp");
            Items Soda = new Items("soda", 0, 200, 0, 2, "A Diet Soda, Restores 200 SP");
            Items Dynamite = new Items("dynamite", 0, 0, 2000, 2, "A powerful stick of dynamite, deals 2000 damage");
            Items Coffee = new Items("coffee", player.MAXHP, player.MAXSP, 0, 1, "A Quadruple Espresso, Restores SP and HP to max");

            AttackButton.Click += (sender, e) =>
            {
                CombatProcess(player,1,Guillotina,"");
            };
        }

        public int GuillotinaHP { get; set; } = 0;

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

        public void CombatProcess(Player player, int playerAttackIndex, Player enemy, string dialog, int insight = 0 )
        {
            Random random = new Random();

            Attack playerAttack = player.Attacks[playerAttackIndex];
            Attack enemyAttack = enemy.Attacks[random.Next(0,enemy.Attacks.Count - 1)];

            if (player.SP-playerAttack.ManaValue >= 0)
            {
                player.SP -= playerAttack.ManaValue;
                if (player.SPEED >= enemy.SPEED)//Checking for Speed
                {

                    player.Attack(enemy, playerAttack, ref insight); //this is the player attacking

                    enemy.Attack(player, enemyAttack, ref insight); //this is the enemy attacking

                }
                else
                {
                    player.Attack(enemy, playerAttack, ref insight); //this is the player attacking

                    enemy.Attack(player, enemyAttack, ref insight); //this is the enemy attacking
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
    
    

