using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Eni_Map
{
    public int mapID;
    public string mapName;
    public int planetID;
    public Dictionary<int, int> listWaypoint;
    public Eni_Map(int mapID, Dictionary<int, int> listWaypoint, string mapName, int planetID)
    {
        this.mapID = mapID;
        this.listWaypoint = listWaypoint;
        this.mapName = mapName;
        this.planetID = planetID;
    }
}
