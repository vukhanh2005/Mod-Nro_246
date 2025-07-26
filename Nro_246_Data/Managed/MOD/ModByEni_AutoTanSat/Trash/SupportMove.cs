using System;
using System.Threading;

public class SupportMove
{
    public static void MoveTo(int x, int y)
    {
        int segment = 50;
        int cxx = Char.myCharz().cx;
        int cyy = Char.myCharz().cy;
        int segmentX = (cxx - x) / segment;
        int segmentY = (y - cyy) / segment;

        for(int i = 1; i <= segment; i++)
        {
            Char.myCharz().cx += segmentX;
            Char.myCharz().cy += segmentY;
            Thread.Sleep(10);
            Service.gI().charMove();
        }
    }
}
