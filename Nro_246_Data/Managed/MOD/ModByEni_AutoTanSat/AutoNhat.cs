using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class AutoNhat
{
    public static bool          isMovingToItem          = false;
    public static ItemMap       target                  = null;
    public static List<ItemMap> list;
    public static long          lastTimeRequestPickItem = 0;

    public static void Auto()
    {
        target = FindItem();
        if(list.Count() == 0)
        {
            target = null;
            isMovingToItem = false;
            return;
        }
        if(target == null)
        {
            return;
        }
        else if(target != null)
        {
            if (Res.distance(Char.myCharz().cx, Char.myCharz().cy, target.x, target.y) >= 30)
            {
                isMovingToItem = true;
                SupportMoving.MoveTo(target.x, target.y);
            }
            if (Res.distance(Char.myCharz().cx, Char.myCharz().cy, target.x, target.y) < 30)
            {
                isMovingToItem = false;
                if (mSystem.currentTimeMillis() - lastTimeRequestPickItem > 1000)
                {
                    Service.gI().pickItem(target.itemMapID);
                    lastTimeRequestPickItem = mSystem.currentTimeMillis();
                }
            }
        }
    }
    public static ItemMap FindItem()
    {
        ItemMap result = null;
        //Thêm item đủ tiêu chuẩn để nhặt vào list
        list = new List<ItemMap>();
        for (int i = 0; i < GameScr.vItemMap.size(); i++)
        {
            ItemMap item = GameScr.vItemMap.elementAt(i) as ItemMap;
            if (item != null && item.playerId == Char.myCharz().charID)
            {
                list.Add(item);
            }
        }
        //Tìm item gần nhất
        int distanceMin = int.MaxValue;
        foreach (ItemMap item in list)
        {
            int distance = Res.distance(Char.myCharz().cx, Char.myCharz().cy, item.x, item.y);
            if (distance < distanceMin)
            {
                distanceMin = distance;
                result = item;
            }
        }
        return result;
    }
}
