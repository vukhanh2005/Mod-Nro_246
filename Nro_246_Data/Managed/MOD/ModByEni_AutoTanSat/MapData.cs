using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class MapData
{
    public int mapID;
    public string mapName;
    public bool isMapHaveNpcCanChangePlanet;
    public bool isMapHaveNpcCanChangeFuture;
    public bool isMapHaveNpcCanChangeCold;
    public Waypoint nextMap_Point;
    public Waypoint previousMap_Point;
    public Waypoint middleMap_Point;
}

