using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;

namespace Tahovka
{
    public class Item
    {

        public static Dictionary<string, Item> Items = new Dictionary<string, Item>();

        public string ID { get; set; }

        public int QUANTITY { get; set; }
        public string FLAVOR { get; set; }
        public Action<Unit> onUse { get; set; }
        public bool UsedItem = false;

        public Unit itemOwner;

        public Item(string id, int quantity, string flavor, Action<Unit> onuse )
        {
            ID = id;
            QUANTITY = quantity;
            FLAVOR = flavor;
            onUse = onuse;

            Items.Add(id, this);

        }

        public void Use()
        {
            if (UsedItem == true)
            {
                MainWindow.i.DisplayDialgue($"You already used an item this turn...");
                return;
            }
            if (QUANTITY > 0)
            {
                onUse.Invoke(itemOwner);

                itemOwner.UpdateHealthDisplay();
                itemOwner.UpdateManaDisplay();

                MainWindow.i.DisplayDialgue($"{itemOwner.NAME} used {ID}.");
                UsedItem = true;
                QUANTITY--;
            } else MainWindow.i.DisplayDialgue($"You are out of {ID}...");

        }

       
    }
}
