using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

public class Handle_KeyPress
{
    public static void update()
    {
        if(GameCanvas.keyAsciiPress == 120)
        {
            GameCanvas.keyAsciiPress = 0; // Reset key press
            Mod_Menu listMenu = new Mod_Menu();
            listMenu.addCommand(new Command("Train Mob", 241120051, null));
            listMenu.addCommand(new Command("Auto Bán Đồ \n", 241120052, null));
            listMenu.addCommand(new Command("Auto Nhặt \n" + (Data.isAutoPick ? "ON" : "OFF"), 241120053, null));
            new Thread(() => listMenu.show()).Start();
            GameScr.info1.addInfo("Mod Menu Opened", 0);
        }
    }
}
