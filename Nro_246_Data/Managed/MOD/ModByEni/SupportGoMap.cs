using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class SupportGoMap
{
    public static int[] wayToGo;
    public static bool isComing;
    public static int index = 1;
    public static int currentIndexMap = -1;
    public static void init(int[] way)
    {
        wayToGo = way;
        isComing = true;
        currentIndexMap = 0;
    }
    public static void go()
    {
        if(index >= wayToGo.Length)
        {
            isComing = false;
            index = 0;
            Data.isAutoMap = false;
            SPC.chat("Đã đến đích!");
            return;
        }
        Waypoint waypoint = FindWaypoint();
        if(waypoint != null)
        {
            EnterWaypoint(waypoint);
        }
    }
    public static Waypoint FindWaypoint()
    {
        Waypoint result = null;
        for (int i = 0; i < Data.listMap.list.Count; i++)
        {
            if(Data.listMap.list[i].mapID == TileMap.mapID)
            {
                if(Data.listMap.list[i].listWaypoint.ContainsKey(wayToGo[index]))
                {
                    result = TileMap.vGo.elementAt(Data.listMap.list[i].listWaypoint[wayToGo[index]]) as Waypoint;
                    index++;
                }
            }
        }
        return result;
    }
    public static void EnterWaypoint(Waypoint waypoint)
    {
        int endX = waypoint.minX + (waypoint.maxX - waypoint.minX) / 2;
        int endY = waypoint.maxY;

        SupportMoving.TeleportTo(endX, endY);
        Service.gI().requestChangeMap();
    }
}
