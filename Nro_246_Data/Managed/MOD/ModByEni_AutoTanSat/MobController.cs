using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

public class MobController
{
    public static long temp = 0;
    public static void update()
    {
        if(Data.isAutoBanDo)
        {
            new Thread(() =>
            {
                AutoBanDo.Auto();
            }).Start();
        }
        if (Data.isTanSat)
        {
            new Thread(() =>
            {
                TanSat.Auto();
            }).Start();
        }
        if(Data.isAutoNhat)
        {
            new Thread(() =>
            {
                AutoNhat.Auto();
            }).Start();
        }
    }
    public void perform()
    {

    }
}

