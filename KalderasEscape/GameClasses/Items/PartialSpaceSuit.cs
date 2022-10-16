using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalderasEscape.GameClasses.Items
{
    internal class PartialSpaceSuit : Item
    {
        public override string Name { get; set; } = "Partial space suit";
        public override string Description { get; set; } = "A space suit without a helmet";

        public override List<string> GetOptions(Player player)
        {
            var currentActions = base.GetOptions(player);

            if (player.Inventory.Contains(this))
            {
                currentActions.Add("Combine");
            }

            return currentActions;
        }
    }
}
