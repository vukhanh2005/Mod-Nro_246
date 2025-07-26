using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class HandleKeyPress
{
    public static void update()
    {
        if(GameCanvas.keyAsciiPress == 122)//z
        {
            Data.isAutoBanDo = !Data.isAutoBanDo;
            if(Data.isAutoBanDo)
            {
                GameScr.info1.addInfo("Tự động bán đồ đã bật", 0);
            }
            else
            {
                GameScr.info1.addInfo("Tự động bán đồ đã tắt", 0);
            }
        }
        if(GameCanvas.keyAsciiPress == 120) //x
        {
            Data.isTanSat = !Data.isTanSat;

            if(Data.isTanSat)
            {
                GameScr.info1.addInfo("Tự động tàn sát đã bật", 0);
            }
            else
            {
                GameScr.info1.addInfo("Tự động tàn sát đã tắt", 0);
            }
        }
        if(GameCanvas.keyAsciiPress == 110) //n
        {
            Data.isAutoNhat = !Data.isAutoNhat;
            if(Data.isAutoNhat)
            {
                GameScr.info1.addInfo("Tự động nhặt đã bật", 0);
            }
            else
            {
                GameScr.info1.addInfo("Tự động nhặt đã tắt", 0);
            }
        }
    }
    public static bool IsClick(int keyCode)
    {
        if (GameCanvas.keyAsciiPress == keyCode)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

