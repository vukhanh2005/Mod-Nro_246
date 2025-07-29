using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

public class Mod_Manager
{
    public static List<int> listIdMob;
    public static void Update()
    {
        if(Data.isAutoTrain)
        {
            TanSat.AutoTanSat(listIdMob);
        }
        if(Data.isAutoPick)
        {
            AutoPick.AutoPickItem();
        }
    }
}

