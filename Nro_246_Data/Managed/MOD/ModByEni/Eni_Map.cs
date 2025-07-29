using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Eni_Map
{
    string nameMap;
    int currentMapID;
    int nextMapID;
    int prevMapID;
    int midMapID;

    public Waypoint nextWP;
    public Waypoint prevWP;
    public Waypoint midWP;
    public Eni_Map(int currentMapID, int nextMapID, int prevMapID, int midMapID)
    {
        this.currentMapID = currentMapID;
        this.nextMapID = nextMapID;
        this.prevMapID = prevMapID;
        this.midMapID = midMapID;
    }
}
