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
    public static void AutoTanSat(List<int> listId)
    {
        if(AutoPick.isMovingToItem || Char.myCharz().isDie || AutoPick.target != null || AutoPick.list.Count > 0)
        {
            return;
        }
        target = FindMob(listId);
        if (target == null)
        {
            return; //Hết quái để đập
        }
        if (target != null && !target.injureThenDie)
        {
            SPC.chat("Current target: " + target.getTemplate().name);
            if (Res.distance(Char.myCharz().cx, 0, target.x, 0) > 50)
            {
                isMovingToMob = true;
                SupportMoving.MoveTo(target.x, target.y);
            }
            if (Res.distance(Char.myCharz().cx, 0, target.x, 0) <= 50)
            {
                isMovingToMob = false;
                if (mSystem.currentTimeMillis() - lastTimeDoubleClick >= 300)
                {
                    Char.myCharz().mobFocus = target;
                    GameScr.gI().doDoubleClickToObj(target);
                    lastTimeDoubleClick = mSystem.currentTimeMillis();
                }
            }
        }
    }
    public static Mob FindMob(List<int> listId)
    {
        Mob result = null;

        //Thêm mob đủ tiêu chuẩn để đánh vào list
        list = new List<Mob>();
        if(listId != null)
        {
            for (int i = 0; i < GameScr.vMob.size(); i++)
            {
                Mob mob = GameScr.vMob.elementAt(i) as Mob;
                if (mob != null && mob.injureThenDie == false && !mob.isMobMe && listId.Contains(mob.templateId))
                {
                    list.Add(mob);
                }
            }
        }
        else if(listId == null)
        {
            for (int i = 0; i < GameScr.vMob.size(); i++)
            {
                Mob mob = GameScr.vMob.elementAt(i) as Mob;
                if (mob != null && mob.injureThenDie == false && !mob.isMobMe)
                {
                    list.Add(mob);
                }
            }
        }
        if (list.Count == 0)
        {
            return null;
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

