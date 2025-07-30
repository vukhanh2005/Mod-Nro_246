using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

public class Painting
{
    public static void update(mGraphics g)
    {
        PaintInfoMap(g);
        //PaintWaypointInfo(g);
        //PaintPlayerPosition(g);
        //PaintInfoItemInBag(g);
        //PaintInfoMobInMap(g);
        //PaintNpcInfo(g);
    }
    public static void PaintInfoMap(mGraphics g)
    {
        mFont.tahoma_7_blue1.drawString(g, "Map: " + TileMap.mapName + "(" + TileMap.mapID + ")" + "(" + TileMap.planetID + ")", 250, 0, 0);
    }
    public static void PaintInfoItemInBag(mGraphics g)
    {
        Item[][] list = new Item[10][];
        int index = 0;

        // Lấy các cặp item từ Bag và Body để tạo list[][]
        for (int i = 0; i < Char.myCharz().arrItemBag.Length && index < list.Length; i += 2)
        {
            list[index] = new Item[2];

            // Item từ arrItemBag
            if (i < Char.myCharz().arrItemBag.Length)
                list[index][0] = Char.myCharz().arrItemBag[i];

            // Item tiếp theo (nếu có)
            if (i + 1 < Char.myCharz().arrItemBag.Length)
                list[index][1] = Char.myCharz().arrItemBag[i + 1];

            index++;
        }

        // Vẽ thông tin dạng 2 cột
        int startX = 120;
        int colWidth = 50;
        int lineHeight = 10;

        for (int row = 0; row < list.Length; row++)
        {
            for (int col = 0; col < 2; col++)
            {
                Item item = list[row][col];
                if (item != null)
                {
                    int x = startX + col * colWidth;
                    int y = row * lineHeight;
                    mFont.tahoma_7b_white.drawString(g, item.template.name + " ", x, y, 0);
                }
            }
        }
    }
    public static void PaintInfoMobInMap(mGraphics g)
    {
        int currI = 0;
        for (int i = 0; i < GameScr.vMob.size(); i++)
        {
            Mob mob = GameScr.vMob.elementAt(i) as Mob;
            if (mob != null)
            {
                mFont.tahoma_7b_white.drawString(g, mob.getTemplate().name + "(" + mob.hp + "/" + mob.maxHp + ")"
                    + " ID: " + mob.getTemplate().mobTemplateId + " Type: " + mob.getTemplate().type + " IsDead: " + (mob.injureThenDie ? "True" : "False")
                    + " Loai quai: " + (mob.checkIsBoss() ? "SQ" : "QT"), 100, currI * 10, 0);
                currI++;
            }
        }
    }
    public static void PaintNpcInfo(mGraphics g)
    {
        for(int i = 0; i < GameScr.vNpc.size(); i++)
        {
            Npc npc = GameScr.vNpc.elementAt(i) as Npc;
            if (npc != null)
            {
                mFont.tahoma_7b_white.drawString(g, npc.template.name + " ID: " + npc.template.npcTemplateId, 100, i * 10, 0);
            }
        }
    }
    public static void PaintPlayerPosition(mGraphics g)
    {
        if (Char.myCharz() != null)
        {
            mFont.tahoma_7b_white.drawString(g, "X: " + Char.myCharz().cx + " Y: " + Char.myCharz().cy, 200, 200, 0);
        }
    }
    public static void PaintWaypointInfo(mGraphics g)
    {
        for(int i = 0; i < TileMap.vGo.size(); i++)
        {
            Waypoint waypoint = TileMap.vGo.elementAt(i) as Waypoint;
            if (waypoint != null)
            {
                string info = "Waypoint: " + waypoint.minX + ", " + waypoint.minY + " - " + waypoint.maxX + ", " + waypoint.maxY + i;

                mFont.tahoma_7b_white.drawString(g, info, 100, i * 10, 0);
            }
        }
    }
}
