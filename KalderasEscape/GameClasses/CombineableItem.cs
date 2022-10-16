using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalderasEscape.GameClasses
{
    public class CombineableItem : Item
    {
        public virtual CombineableItem CombineItem { get; set; }
        public virtual Item CompleteItem { get; set; }

        public Item? CombineWith(CombineableItem item)
        {
            if (item.CombineItem == this)
            {
                return CompleteItem;
            }
            else
            {
                return null;
            }
        }
    }
}
