using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Mod_Menu
{
    public MyVector listCommand;
    public static List<int> listId;
    public Mod_Menu()
    {
        listCommand = new MyVector();
        if (listId == null)
        {
            listId = new List<int>();
        }
    }
    
    public void addCommand(Command cmd)
    {
        if (listCommand == null)
        {
            listCommand = new MyVector();
        }
        listCommand.addElement(cmd);
    }

    public static void perform(int idAction, object p)
    {
        //241120050 Map
        if(idAction == 241120050)
        {
            Mod_Menu listMenu = new Mod_Menu();
            listMenu.addCommand(new Command("Trái Đất", 112233, null));
            listMenu.addCommand(new Command("Namek", 112234, null));
            listMenu.addCommand(new Command("Xayda", 112235, null));
            listMenu.show();
        }
        //Trái đất : 112233
        if(idAction == 112233)
        {
            Mod_Menu listMenu = new Mod_Menu();
            for(int i = 0; i < Data.listMap.list.Count; i++)
            {
                if (Data.listMap.list[i].planetID == 0)
                {
                    listMenu.addCommand(new Command(Data.listMap.list[i].mapName, Data.listMap.list[i].mapID + 999888, null));
                }
            }
            listMenu.show();
        }
        //Namek : 112234
        if (idAction == 112234)
        {
            Mod_Menu listMenu = new Mod_Menu();

            for (int i = 0; i < Data.listMap.list.Count; i++)
            {
                if (Data.listMap.list[i].planetID == 1)
                {
                    listMenu.addCommand(new Command(Data.listMap.list[i].mapName, Data.listMap.list[i].mapID + 999888, null));
                }
            }
            listMenu.show();
        }
        //Xayda : 112235
        if (idAction == 112235)
        {
            Mod_Menu listMenu = new Mod_Menu();

            for (int i = 0; i < Data.listMap.list.Count; i++)
            {
                if (Data.listMap.list[i].planetID == 2)
                {
                    listMenu.addCommand(new Command(Data.listMap.list[i].mapName, Data.listMap.list[i].mapID + 999888, null));
                }
            }
            listMenu.show();
        }
        for(int i = 0; i < Data.listMap.list.Count; i++)
        {
            //Chuyen den map
            if (idAction == Data.listMap.list[i].mapID + 999888)
            {
                GraphMap graphMap = new GraphMap();
                Mod_Manager.bestWay = graphMap.FindBestWay(TileMap.mapID, Data.listMap.list[i].mapID);
                SupportGoMap.init(Mod_Manager.bestWay);
                string result = "";
                for(int j = 0; j < Mod_Manager.bestWay.Length; j++)
                {
                    result += Mod_Manager.bestWay[j] + " ";
                }
                SPC.chat("Đường đi: " + result);
                Data.isAutoMap = true;
            }
        }
        //Tan sat
        if (idAction == 241120051)
        {
            Mod_Menu listMenu = new Mod_Menu();
            if(Data.isAutoTrain)
            {
                listMenu.addCommand(new Command("TRAINING: ON", 20052001, null));
            }
            listMenu.addCommand(new Command("Tàn sát tất cả", 20052003, null));
            listMenu.addCommand(new Command("Tàn sát theo ID", 20052004, null));
            listMenu.addCommand(new Command("Thêm id", 20052005, null));
            listMenu.addCommand(new Command("Clear danh sách", 20052006, null));
            listMenu.show();
        }
        //Auto nhat
        if(idAction == 241120053)
        {
            Data.isAutoPick = !Data.isAutoPick;
        }
        //Go Map
        //Off training
        if( idAction == 20052001)
        {
            Data.isAutoTrain = false;
        }
        //Tan sat tat ca
        if (idAction == 20052003)
        {
            SPC.chat("Tàn sát tất cả: " + GameScr.vMob.size() + " mobs");
            Mod_Manager.listIdMob = null;
            Data.isAutoTrain = !Data.isAutoTrain;
        }
        //Tan sat theo id
        if (idAction == 20052004)
        {
            if (listId.Count > 0)
            {
                Mod_Manager.listIdMob = listId;
                Data.isAutoTrain = !Data.isAutoTrain;
            }
            else
            {
                SPC.chat("Chưa có ID nào trong danh sách");
            }
        }
        //Them id
        if (idAction == 20052005)
        {
            Mob mobFocus = Char.myCharz().mobFocus;
            if (mobFocus != null && !listId.Contains(mobFocus.templateId))
            {
                listId.Add(mobFocus.templateId);
                SPC.chat("Đã thêm ID: " + mobFocus.templateId);
            }
            else if (mobFocus == null)
            {
                SPC.chat("Chưa focus mob nào");
            }
            else if (listId.Contains(mobFocus.templateId))
            {
                SPC.chat("ID đã có trong danh sách");
            }
        }
        //Clear danh sach
        if (idAction == 20052006)
        {
            listId.Clear();
            SPC.chat("Đã xóa danh sách ID");
        }
}
    public void show()
    {
        if ((listCommand == null))
        {
            return;
        }
        GameCanvas.menu.startAt(listCommand, 250);
    }
}

