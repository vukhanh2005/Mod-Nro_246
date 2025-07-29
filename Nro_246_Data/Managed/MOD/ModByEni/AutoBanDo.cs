using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class AutoBanDo
{
    public static long speed = 100;
    public static long lastTimeBanDo = 0;
    public static void Auto()
    {
        if (mSystem.currentTimeMillis() - lastTimeBanDo > speed)
        {
            int itemIndex = GetItemIndex(new int[] {1, 22, 7, 225, 28, 12, 41, 46});
            if (itemIndex >= 0)
            {
                Service.gI().saleItem(0, 1, (short)itemIndex);
                Service.gI().saleItem(1, 1, (short)itemIndex);
                lastTimeBanDo = mSystem.currentTimeMillis();
            }
        }
    }
    public static int GetItemIndex(int[] idItem)
    {
        for (int i = 0; i < Char.myCharz().arrItemBag.Count(); i++)
        {
            Item item = Char.myCharz().arrItemBag[i];
            if (item != null && idItem.Contains(item.template.id) && item.itemOption.Length <= 1)
            {
                return i;
            }
        }
        return -1; // Không tìm thấy item
    }
}
