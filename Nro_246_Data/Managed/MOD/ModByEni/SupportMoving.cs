using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class SupportMoving
{
    public static void TeleportTo(int x, int y)
    {
        Char.myCharz().cx = x;
        Char.myCharz().cy = y;
        Service.gI().charMove();
        Char.myCharz().cx = x + 1;
        Char.myCharz().cy = y;
        Service.gI().charMove();
        Char.myCharz().cx = x;
        Char.myCharz().cy = y;
        Service.gI().charMove();
    }
    public static void MoveTo(int x, int y)
    {
        if(x - Char.myCharz().cx < 0)
        {
            Char.myCharz().currentMovePoint = new MovePoint(Char.myCharz().cx - 48, GetY(Char.myCharz().cx - 48));
        }
        if (x - Char.myCharz().cx > 0)
        {
            Char.myCharz().currentMovePoint = new MovePoint(Char.myCharz().cx + 48, GetY(Char.myCharz().cx + 48));
        }
    }
    public static int GetY(int x)
    {
        int mapHeight = TileMap.pxh;
        int tileSize = 24;
        for (int i = 0; i < mapHeight; i += tileSize)
        {
            if (TileMap.tileTypeAt(x, i + 5, 2)) // 5: lệch giữa ô
            {
                return (i);
            }
        }
        return -1;
    }

    public static void DrawRect(mGraphics g, int x1, int y1, int x2, int y2)
    {
        g.setColor(UnityEngine.Color.blue);
        g.drawRect(x1, y1, x2, y2);
    }
}

