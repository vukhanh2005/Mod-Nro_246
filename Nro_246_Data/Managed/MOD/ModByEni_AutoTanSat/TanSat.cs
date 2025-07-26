using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class TanSat
{
    public static Mob       target              = null;
    public static bool      isMovingToMob       = false;
    public static List<Mob> list;
    public static long      lastTimeDoubleClick = 0;
    public static int       currentZoneID       = -1;
    public static void Auto()
    {
        if (Data.isAutoNhat && (AutoNhat.isMovingToItem == true || AutoNhat.list.Count() > 0))
        {
            target = null;
            Char.myCharz().mobFocus = null;
            return; //Nếu đang nhặt đồ thì không đánh quái
        }
        target = FindMob();
        if (target == null)
        {
            return; //Hết quái để đập
        }
        if (target != null)
        {
            if (Res.distance(Char.myCharz().cx, Char.myCharz().cy, target.x, target.y) >= 50)
            {
                isMovingToMob = true;
                SupportMoving.MoveTo(target.x, target.y);
            }
            if (Res.distance(Char.myCharz().cx, Char.myCharz().cy, target.x, target.y) < 50)
            {
                isMovingToMob = false;
                if (mSystem.currentTimeMillis() - lastTimeDoubleClick >= 200)
                {
                    Char.myCharz().mobFocus = target;
                    GameScr.gI().doDoubleClickToObj(target);
                    lastTimeDoubleClick = mSystem.currentTimeMillis();
                }
            }
        }
    }
    public static Mob FindMob()
    {
        Mob result = null;

        //Thêm mob đủ tiêu chuẩn để đánh vào list
        list = new List<Mob>();
        for (int i = 0; i < GameScr.vMob.size(); i++)
        {
            Mob mob = GameScr.vMob.elementAt(i) as Mob;
            if (mob != null && mob.injureThenDie == false) 
            {
                list.Add(mob);
            }
        }

        //Tìm mob gần nhất
        int distanceMin = int.MaxValue;
        foreach (Mob mob in list)
        {
            int distance = Res.distance(Char.myCharz().cx, Char.myCharz().cy, mob.x, mob.y);
            if (distance < distanceMin)
            {
                distanceMin = distance;
                result = mob;
            }
        }
        return result;
    }
}

