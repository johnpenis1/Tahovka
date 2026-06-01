using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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

        public static MainWindow i;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            i = this;


            Attack Punch = new Attack("Punch",20,2,0,false,1,"","You Punch with all your might"); 
            Attack Fireball = new Attack("Fireball",50,2,30,true,1,"a magic attack that uses SP damage, costs 30 mana", "You fire off a fireball, fire."); 
            Attack Flurry = new Attack("Flurry", 15,2,50,false,5,"a weak attack that hits multiple times, Costs 50 Mana", "You unleash a flurry of blows.");
            Attack FistOfFaith = new Attack("Fist of Faith",100,3,100,false,1,"a High damage one hit attack", "You Channel all your Chi into the attack."); 
            Attack FivePointFingerHeartExplodingTechnique = new Attack("Five Point Palm Exploding Heart Technique", 999999,10,0,true,10,"attack that instantly kills the unit, costs nothing and is for testing (you cheater!)", "DIIIIIIIE!!!"); // these are your attacks
            Attack Scratch = new Attack("Scratch",20, 2, 0, false, 1,"","Guillotina scratches you!");


            Item Jerky = new Item("jerky", 3,"A piece of Jerky, Heals 1500 hp",(unit) => { unit.HP += 1500; });
            Item Soda = new Item("soda",2, "A Diet Soda, Restores 200 SP", (unit) => { unit.SP += 200; });
            Item Dynamite = new Item("dynamite", 2, "A powerful stick of dynamite, deals 2000 damage", (unit) => { unit.Target.HP -= 2000; });
            Item Coffee = new Item("coffee", 1, "A Quadruple Espresso, Restores SP and HP to max", (unit) => { unit.HP = unit.MAXHP; unit.SP = unit.MAXSP; });


            Unit Guillotina = new Unit("Guillotina", HealthDisplay: BossHP, maxhp: 10000, def: 10, atk: 30, sdef: 15, satk: 10, speed: 1, PrimaryAttack: Scratch);
            Unit player = new Unit("Player", HealthDisplay: PlayerHPAmount, ManaDisplay: ManaAmount, maxhp: 2000, maxsp: 400, def: 30, atk: 50, sdef: 50, satk: 30, speed: 50, PrimaryAttack: Punch, spells: new List<Attack>() { Fireball, FistOfFaith, FivePointFingerHeartExplodingTechnique, Flurry },items: Item.Items.Values.ToList());

            player.Target = Guillotina;
            Guillotina.Target = player;

            // Binding primary attack

            AttackButton.Click += (sender, e) =>
            {
                CombatProcess(player,player.primaryAttack,Guillotina);
            };
            // Binding Defend
            DefendButton.Click += (sender, e) =>
            {
                CombatProcess(player, player.primaryAttack, Guillotina, true);
            };


            // Binding spells

            foreach (UIElement child in MagicMenu.Children)
            {
                if (child is Button button)
                {
                    button.Click += (sender, e) => { CastMagicSpell(button.Content.ToString(), player, Guillotina); } ;
                }
            }

            // Binding items

            foreach (UIElement child in ItemMenu.Children)
            {
                if (child is Button button)
                {
                    string buttonName = Utility.CleanseString(button.Name);

                    if (!player.Items.ContainsKey(buttonName)) continue; // if the element we're looping through doesnt exist as an item, we go to the next element

                    button.Click += (sender, e) => { UseItem(buttonName, player); } ;
                    button.MouseEnter += (sender, e) => { DescriptionDisplay(player.Items[buttonName]); };
                }
            }
     

        }


        // UI

        public void DisplayDialgue(string dialogue)
        {
            Dialogue.Text = dialogue;
            
        }

        public void CastMagicSpell(string spellName, Unit player, Unit enemy)
        {

            CombatProcess(player, player.GetAttackFrom(spellName), enemy);
        }

        public void UseItem(string itemName, Unit player)
        {
            player.Items[itemName].Use();

        }


        public void DescriptionDisplay(Item item)
        {
            FlavorText.Text = item.FLAVOR + ", You have " + item.QUANTITY + " Left";
        }

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

       

        // Logic

        public bool turnInProgress = false;

        public async void CombatProcess(Unit player, Attack playerAttack, Unit enemy, bool defending = false)
        {

            Attack enemyAttack = enemy.primaryAttack;

            if (enemyAttack == null || playerAttack == null || turnInProgress) return;


            if (defending == true)
            {
                turnInProgress = true;
                player.Defend(enemy, enemyAttack);
            }
            else
            {
                if (player.SP - playerAttack.ManaValue >= 0)
                {
                    turnInProgress = true;
                    player.SP -= playerAttack.ManaValue;
                    player.UpdateManaDisplay();
                    if (player.SPEED >= enemy.SPEED) //Checking for Speed
                    {

                        player.AttackTarget(playerAttack); //this is the player attacking

                        await Task.Delay(2000);

                        enemy.AttackTarget(enemyAttack); //this is the enemy attacking
                    }
                    else
                    {
                        player.AttackTarget(playerAttack); //this is the player attacking

                        await Task.Delay(2000);

                        enemy.AttackTarget(enemyAttack); //this is the enemy attacking
                    }
                }
                else
                {
                    DisplayDialgue("You dont have enough mana for this!");
                }
            }
            turnInProgress = false;
            Item.UsedItem = false;
        }

    }

        /// This is the Template player.HP = (Player.Attack(enemy.HP, Punch.BaseDmg, player.ATK, Punch.PowerMult, enemy.DEF));
        ///
        ///
    }
    
    

