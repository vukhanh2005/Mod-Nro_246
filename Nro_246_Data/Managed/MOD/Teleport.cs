public class Teleport
{
	public static MyVector vTeleport = new MyVector();

	public int x;

	public int y;

	public int headId;

	public int type;

	public bool isMe;

	public int y2;

	public int id;

	public int dir;

	public int planet;

	public static Image[] maybay = new Image[5];

	public static Image hole;

	public bool isUp;

	public bool isDown;

	private bool createShip;

	public bool paintFire;

	private bool painHead;

	private int tPrepare;

	private int vy = 1;

	private int tFire;

	private int tDelayHole;

	private bool tHole;

	private bool isShock;

	public Teleport(int x, int y, int headId, int dir, int type, bool isMe, int planet)
	{
		this.x = x;
		this.y = 5;
		y2 = y;
		this.headId = headId;
		this.type = type;
		this.isMe = isMe;
		this.dir = dir;
		this.planet = planet;
		tPrepare = 0;
		int num = 0;
		while (num < 100)
		{
			num++;
			y2 += 12;
			if (TileMap.tileTypeAt(x, y2, 2))
			{
				if (y2 % 24 != 0)
				{
					y2 -= y2 % 24;
				}
				break;
			}
		}
		isDown = true;
		isUp = false;
		if (this.planet > 2)
		{
			y2 += 4;
			if (maybay[3] == null)
			{
				maybay[3] = GameCanvas.loadImage("/mainImage/myTexture2dmaybay4a.png");
			}
			if (maybay[4] == null)
			{
				maybay[4] = GameCanvas.loadImage("/mainImage/myTexture2dmaybay4b.png");
			}
			if (hole == null)
			{
				hole = GameCanvas.loadImage("/mainImage/hole.png");
			}
		}
		else if (maybay[planet] == null)
		{
			maybay[planet] = GameCanvas.loadImage("/mainImage/myTexture2dmaybay" + (planet + 1) + ".png");
		}
		if (x > GameScr.cmx && x < GameScr.cmx + GameCanvas.w && y2 > 100 && !SoundMn.gI().isPlayAirShip() && !SoundMn.gI().isPlayRain())
		{
			createShip = true;
			SoundMn.gI().airShip();
		}
	}

	public static void addTeleport(Teleport p)
	{
		vTeleport.addElement(p);
	}

	public void paintHole(mGraphics g)
	{
		if (planet > 2 && tHole)
		{
			g.drawImage(hole, x, y2 + 20, StaticObj.BOTTOM_HCENTER);
		}
	}

	public void paint(mGraphics g)
	{
		if (Char.isLoadingMap || x < GameScr.cmx || x > GameScr.cmx + GameCanvas.w)
		{
			return;
		}
		Part part = GameScr.parts[headId];
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		if (planet == 0)
		{
			num = 15;
			num2 = 40;
			num3 = 5;
		}
		if (planet == 1)
		{
			num = 7;
			num2 = 55;
			num3 = 20;
		}
		if (planet == 2)
		{
			num = 18;
			num2 = 52;
			num3 = 10;
		}
		if (painHead && planet < 3)
		{
			SmallImage.drawSmallImage(g, part.pi[Char.CharInfo[0][0][0]].id, x + ((dir != 1) ? (-num) : num), y - num2, (dir != 1) ? 2 : 0, StaticObj.TOP_CENTER);
		}
		if (planet < 3)
		{
			g.drawRegion(maybay[planet], 0, 0, mGraphics.getImageWidth(maybay[planet]), mGraphics.getImageHeight(maybay[planet]), (dir == 1) ? 2 : 0, x, y, StaticObj.BOTTOM_HCENTER);
		}
		else if (isDown)
		{
			if (tPrepare > 10)
			{
				g.drawRegion(maybay[4], 0, 0, mGraphics.getImageWidth(maybay[4]), mGraphics.getImageHeight(maybay[4]), (dir == 1) ? 2 : 0, (dir != 1) ? (x + 11) : (x - 11), y + 2, StaticObj.BOTTOM_HCENTER);
			}
			else
			{
				g.drawRegion(maybay[3], 0, 0, mGraphics.getImageWidth(maybay[3]), mGraphics.getImageHeight(maybay[3]), (dir == 1) ? 2 : 0, x, y, StaticObj.BOTTOM_HCENTER);
			}
		}
		else if (tPrepare < 20)
		{
			g.drawRegion(maybay[4], 0, 0, mGraphics.getImageWidth(maybay[4]), mGraphics.getImageHeight(maybay[4]), (dir == 1) ? 2 : 0, (dir != 1) ? (x + 11) : (x - 11), y + 2, StaticObj.BOTTOM_HCENTER);
		}
		else
		{
			g.drawRegion(maybay[3], 0, 0, mGraphics.getImageWidth(maybay[3]), mGraphics.getImageHeight(maybay[3]), (dir == 1) ? 2 : 0, x, y, StaticObj.BOTTOM_HCENTER);
		}
	}

	public void update()
	{
		if (isDown)
		{
			y = y2;
			if (y % 24 != 0)
			{
				y -= y % 24;
			}
			if (isMe && type == 1 && Char.myCharz().isTeleport)
			{
				Char.myCharz().cx = x;
				Char.myCharz().cy = y - 30;
				Char.myCharz().statusMe = 4;
				GameScr.cmtoX = x - GameScr.gW2;
				GameScr.cmtoY = y - GameScr.gH23 - 1;
				GameScr.info1.isUpdate = false;
			}
			if (GameScr.findCharInMap(id) != null && !isMe && type == 1 && GameScr.findCharInMap(id).isTeleport)
			{
				GameScr.findCharInMap(id).cx = x;
				GameScr.findCharInMap(id).cy = y - 30;
				GameScr.findCharInMap(id).statusMe = 4;
			}
			tHole = true;
			if (planet < 3)
			{
				SoundMn.gI().pauseAirShip();
			}
			else if (!isShock)
			{
				ServerEffect.addServerEffect(92, x + 4, y + 14, 1, 0);
				GameScr.shock_scr = 10;
				isShock = true;
			}
			if (type == 1)
			{
				if (isMe)
				{
					Char.myCharz().isTeleport = false;
				}
				else if (GameScr.findCharInMap(id) != null)
				{
					GameScr.findCharInMap(id).isTeleport = false;
				}
				painHead = false;
			}
			isDown = false;
			isUp = true;
			paintFire = false;
			tPrepare = 0;
		}
		else if (isUp)
		{
			if (createShip)
			{
				SoundMn.gI().resumeAirShip();
			}
			if (type == 0)
			{
				if (isMe)
				{
					Char.myCharz().isTeleport = false;
					if (Char.myCharz().statusMe != 14)
					{
						Char.myCharz().statusMe = 3;
					}
					Char.myCharz().cvy = -3;
				}
				else if (GameScr.findCharInMap(id) != null)
				{
					GameScr.findCharInMap(id).isTeleport = false;
					if (GameScr.findCharInMap(id).statusMe != 14)
					{
						GameScr.findCharInMap(id).statusMe = 3;
					}
					GameScr.findCharInMap(id).cvy = -3;
				}
				painHead = false;
				if (isMe)
				{
					Char.myCharz().isTeleport = true;
				}
				else if (GameScr.findCharInMap(id) != null)
				{
					GameScr.findCharInMap(id).cx = x;
					GameScr.findCharInMap(id).cy = y;
					GameScr.findCharInMap(id).isTeleport = true;
				}
				painHead = true;
			}
			if (isMe)
			{
				if (type == 0)
				{
					GameScr.cmtoX = x - GameScr.gW2;
					GameScr.cmtoY = y - GameScr.gH23 - 1;
				}
				if (type == 1)
				{
					GameScr.info1.isUpdate = true;
				}
			}
			y = -80;
			if (isMe && type == 0)
			{
				Controller.isStopReadMessage = false;
				Char.ischangingMap = true;
			}
			if (!isMe && GameScr.findCharInMap(id) != null && type == 0)
			{
				GameScr.vCharInMap.removeElement(GameScr.findCharInMap(id));
			}
			vTeleport.removeElement(this);
		}
	}
}
