using Assets.src.g;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
public class ModGame : IActionListener
{
    public static int xNew;
    public static int yNew;
    public static bool isAutoAttackToggled = false;
    public static long lastAttackTime = 0;
    public static int currentSpeedGame = 1;
    public static Char targetLocked_Char = null;
    public static Mob targetLocked_Mob = null;
    public static int idLocked = -1;
    public static bool isLocked = false;
    public static bool isANhat = false;
    public static MyVector itemInMap;
    public static float distancePickItem = 30; //Khoảng cách player sẽ nhặt item
    public static bool isBatCo = false;

    public static bool temp = false;
    public static int endX;
    public static int endY;
    public static bool isMovingToTarget = false;
    public static int targetX;
    public static int targetY;
    public static float movementStep = 5f; // Điều chỉnh giá trị này để thay đổi tốc độ di chuyển
    private static bool IsTargetInRange(Char attacker, IMapObject target)
    {
        mFont font = mFont.tahoma_7_white;
        font.getHeight();
        if (attacker == null || target == null)
        {
            GameCanvas.startOKDlg("DEBUG: IsTargetInRange - attacker or target is null.");
            return false;
        }

        int targetX = 0;
        int targetY = 0;

        if (target is Mob)
        {
            Mob mobTarget = (Mob)target;
            targetX = mobTarget.x;
            targetY = mobTarget.y;
        }
        else if (target is Char)
        {
            Char charTarget = (Char)target;
            targetX = charTarget.cx;
            targetY = charTarget.cy;
        }
        else
        {
            GameCanvas.startOKDlg("DEBUG: IsTargetInRange - Unsupported target type.");
            return false;
        }

        int distance = Res.distance(attacker.cx, attacker.cy, targetX, targetY);

        int skillRange = 50;
        if (attacker.myskill != null && attacker.getRangeDamage() > 0)
        {
            skillRange = attacker.getRangeDamage();
        }

        int buffer = 24;

        bool inRange = distance <= skillRange + buffer;
        // GameCanvas.startOKDlg("DEBUG: IsTargetInRange - Distance: " + distance + ", Range: " + (skillRange + buffer) + ", InRange: " + inRange);
        return inRange;
    }

    public static void HandleKeyPress(int keyCode)
    {
        if (ChatTextField.gI().isChatting)
        {
            return;
        }
        else
        {
            if (keyCode == 120) //x
            {
                SupportMove.teleportToWpByIndex(0);
            }

            if (keyCode == 112) //p
            {
                Service.gI().requestChangeMap();
            }
            //press m de mo doi khu vuc
            if (keyCode == 109)
            {
                Service.gI().openUIZone();
            }
            //press a de bat tu dong danh
            if (keyCode == 97)
            {
                isAutoAttackToggled = !isAutoAttackToggled;

                if (isAutoAttackToggled)
                {
                    GameScr.info1.addInfo("Đã bật tự động đánh", 0);
                }
                else
                {
                    GameScr.info1.addInfo("Đã tắt tự động đánh", 0);
                }
            }
            //press 1 increase speed game
            if (keyCode == 49)
            {
                currentSpeedGame++;
                GameScr.info1.addInfo("Game speed: " + currentSpeedGame, 0);
                if (currentSpeedGame > 5)
                {
                    currentSpeedGame = 1;
                }
                SetSpeedGame(currentSpeedGame);
            }
            //press 2 to lock target
            if (keyCode == 50)
            {
                isLocked = !isLocked;
                if (isLocked == false)
                {
                    targetLocked_Char = null;
                    targetLocked_Mob = null;
                    return;
                }
                targetLocked_Char = Char.myCharz().charFocus;
                targetLocked_Mob = Char.myCharz().mobFocus;

                if (targetLocked_Char == null && targetLocked_Mob == null)
                {
                    isLocked = false;
                    return;
                }
                if (targetLocked_Char != null)
                {
                    idLocked = Char.myCharz().charFocus.charID;
                    GameScr.info1.addInfo("Đã khóa: " + targetLocked_Char.cName, 0);
                }
                if (targetLocked_Mob != null)
                {
                    idLocked = Char.myCharz().charFocus.charID;
                    GameScr.info1.addInfo("Đã khóa: " + targetLocked_Mob.mobName, 0);
                }
            }
            //press n to auto nhat
            if (keyCode == 110)
            {
                isANhat = !isANhat;
                if (isANhat)
                {
                    GameScr.info1.addInfo("Đã bật auto nhặt", 0);
                }
                if (!isANhat)
                {
                    GameScr.info1.addInfo("Đã tắt auto nhặt", 0);
                }
            }
            //press space de an dau than
            if (keyCode == 32)
            {
                GameScr.instance.doUseHP();
            }
            //press c de mo capsule
            if (keyCode == 99)
            {
                if (UseItemAt(193)) //id capsule thuong la 193
                {
                    GameScr.info1.addInfo("Đã sử dụng capsu", 0);
                }
            }
        }
    }
    public static void Update()
    {
        GameScr.info1.addInfo("Game tick: " + GameCanvas.gameTick, 0);
        GameCanvas.paintBG = false;
        if (Char.myCharz().isDie)
        {
            if (isAutoAttackToggled)
            {
                isAutoAttackToggled = false;
            }
            if (isANhat)
            {
                isANhat = false;
            }
        }
        else
        {
            itemInMap = GameScr.vItemMap;
        }
        if (isANhat)
        {
            PickItem();
        }
        if (isAutoAttackToggled)
        {
            AutoAttack();
        }
        if (isLocked)
        {
            LockTarget();
        }
    }
    public static void SetSpeedGame(int speed)
    {
        Time.timeScale = speed;
        currentSpeedGame = speed;
    }
    public static bool UseItemAt(int id)
    {
        bool foundItem = false;
        int foundItemIndex = -1;
        if (Char.myCharz() != null && Char.myCharz().arrItemBag != null)
        {
            for (int i = 0; i < Char.myCharz().arrItemBag.Length; i++)
            {
                Item item = Char.myCharz().arrItemBag[i];
                if (item != null && (item.template.id == id))
                {
                    foundItem = true;
                    foundItemIndex = i;
                    break;
                }
            }
        }

        if (foundItem)
        {
            Service.gI().useItem(0, 1, (sbyte)(foundItemIndex), -1);
        }
        else
        {
            GameScr.info1.addInfo("Not found id " + id, 0);
        }
        return foundItem;
    }
    public static void BuyItem(int id)
    {
        Service.gI().buyItem(0, id, 0);
    }
    public static void PickItem()
    {
        // Nếu không có vật phẩm nào trên bản đồ thì không làm gì cả
        if (GameScr.vItemMap == null || GameScr.vItemMap.size() == 0)
            return;

        // Lặp qua tất cả các vật phẩm trên bản đồ
        for (int i = 0; i < GameScr.vItemMap.size(); i++)
        {
            ItemMap itemMap = (ItemMap)GameScr.vItemMap.elementAt(i);

            // Chỉ nhặt vật phẩm của mình hoặc vật phẩm rơi tự do (playerId == -1)
            if (itemMap != null && (itemMap.playerId == Char.myCharz().charID || itemMap.playerId == -1))
            {
                // Tính khoảng cách từ nhân vật đến vật phẩm
                int distance = Res.distance(Char.myCharz().cx, Char.myCharz().cy, itemMap.x, itemMap.y);
                // Nếu đủ gần...
                if (distance < distancePickItem)
                {
                    // GỬI YÊU CẦU NHẶT ĐỒ LÊN SERVER
                    Service.gI().pickItem(itemMap.itemMapID);

                    // Thoát khỏi vòng lặp ngay sau khi nhặt 1 vật phẩm để tránh spam server
                    break;
                }
            }
        }
    }
    public static void LockTarget()
    {
        Char.myCharz().mobFocus = targetLocked_Mob;
        Char.myCharz().charFocus = targetLocked_Char;
    }
    public static void AutoAttack()
    {
        // Nếu chức năng không được bật, hoặc nhân vật không tồn tại/đã chết, thì không làm gì cả
        if (!isAutoAttackToggled || Char.myCharz() == null || Char.myCharz().isDie)
            return;

        // Lấy kỹ năng đang được chọn
        Skill currentSkill = Char.myCharz().myskill;
        if (currentSkill == null)
            return; // Không có kỹ năng nào được chọn

        // Kiểm tra thời gian hồi chiêu. Nếu chưa hồi xong thì không làm gì cả.
        if (mSystem.currentTimeMillis() - currentSkill.lastTimeUseThisSkill < currentSkill.coolDown)
            return;

        // Xác định mục tiêu hiện tại (ưu tiên quái vật)
        IMapObject target = (IMapObject)Char.myCharz().mobFocus ?? (IMapObject)Char.myCharz().charFocus;

        // Nếu không có mục tiêu, không làm gì cả
        if (target == null)
            return;

        // Kiểm tra xem mục tiêu có hợp lệ để tấn công không (còn sống)
        if (target is Mob mobTarget && (mobTarget.hp <= 0 || mobTarget.status == 1 || mobTarget.status == 0))
            return; // Quái đã chết

        if (target is Char charTarget && charTarget.isDie)
            return; // Người chơi đã chết

        // Kiểm tra xem mục tiêu có trong tầm đánh không. Nếu không, không làm gì cả (không di chuyển).
        if (!IsTargetInRange(Char.myCharz(), target))
            return;

        // --- Nếu tất cả điều kiện đều thỏa mãn, thực hiện tấn công "im lặng" ---

        // 1. Chuẩn bị các vector chứa mục tiêu để gửi đi
        MyVector vMob = new MyVector();
        MyVector vChar = new MyVector();
        if (target is Mob) vMob.addElement(target);
        if (target is Char) vChar.addElement(target);

        // 2. Gửi trực tiếp gói tin tấn công lên máy chủ mà không thay đổi trạng thái nhân vật
        if (vMob.size() > 0 || vChar.size() > 0)
        {
            Service.gI().sendPlayerAttack(vMob, vChar, 1);

            // 3. Cập nhật lại thời gian sử dụng kỹ năng ở phía client để bắt đầu đếm ngược cooldown
            currentSkill.lastTimeUseThisSkill = mSystem.currentTimeMillis();
        }
        else
        {
            GameScr.info1.addInfo("Nothing", 0);
        }
    }
    public static void BatCo(int idFlag)
    {
        Service.gI().getFlag(1, (sbyte)idFlag);
    }
    public static void Draw(mGraphics g)
    {
        DrawVang_Ngoc(g);
        //DrawInfoNPC(g);
        DrawInfoMap(g);
        DrawPlayerInMap(g);
        DrawPlayerPosition(g);
        DrawOnPlayer(g);
        DrawWaypointInMap(g);
    }
    public static void DrawVang_Ngoc(mGraphics g)
    {
        int vang = (int)Char.myCharz().xu;
        int ngoc = (int)Char.myCharz().yen;
        mFont.tahoma_7_white.drawString(g, "Vàng: " + vang, 100, 100, 0);
    }
    public static void DrawInfoNPC(mGraphics g)
    {
        for (int i = 0; i < GameScr.vNpc.size(); i++)
        {
            Npc npc = (Npc)GameScr.vNpc.elementAt(i);
            String text = "Name: " + npc.template.name + "(" + npc.template.npcTemplateId + ")";
            mFont.tahoma_7b_white.drawString(g, text, 100, (i + 1) * 10, 0);
        }
    }
    public static void DrawInfoMap(mGraphics g)
    {
        string text = "Map: " + TileMap.mapName + "(" + TileMap.mapID + ")";
        mFont.tahoma_7b_white.drawString(g, text, 0, 40, 0);
    }
    public static void DrawPlayerInMap(mGraphics g)
    {
        int currentI = 1;
        for (int i = 0; i < GameScr.vCharInMap.size(); i++)
        {
            currentI++;
            Char player = (Char)GameScr.vCharInMap.elementAt(i);
            if (player.getIsMiniPet() || player.getIsPet())
            {
                currentI--;
            }
            if (!player.getIsMiniPet() && !player.getIsPet()) //không vẽ đệ tử và pet đi theo
            {
                string text = player.cName + "(" + player.cHP + ")" + player.cTypePk + "_" + (player.isMob ? "Mob" : "NOT MOB");
                if (player.cTypePk != 0)
                {
                    mFont.tahoma_7_red.drawString(g, text, 390, 50 + (currentI) * 10, 0);
                }
                else
                {
                    mFont.tahoma_7b_white.drawString(g, text, 390, 50 + (currentI) * 10, 0);
                }
            }
        }
    }
    public static void DrawPlayerPosition(mGraphics g)
    {
        string text = "X: " + Char.myCharz().cx + " Y: " + Char.myCharz().cy;
        mFont.tahoma_7b_white.drawString(g, text, 250, 0, 0);
    }
    public static void DrawOnPlayer(mGraphics g)
    {
        //Vẽ đường kẻ màu đỏ đến boss
        int x1 = Char.myCharz().cx - GameScr.cmx;
        int y1 = Char.myCharz().cy - GameScr.cmy;
        g.setColor(UnityEngine.Color.blue);
        g.drawLine(x1, y1, x1 + 40, y1);
        g.drawLine(x1, y1, x1, y1 - 40);
        //for (int i = 0; i < GameScr.vCharInMap.size(); i++)
        //{
        //    Char player = (Char)GameScr.vCharInMap.elementAt(i);
        //    if (player != null && player.cTypePk == 5)
        //    {
        //        int x2 = player.cx - GameScr.cmx;
        //        int y2 = player.cy - GameScr.cmy;
        //        g.drawLine(x1, y1, x2, y2);
        //    }
        //}
        //// Draw circle
        //int centerX = x1;
        //int centerY = y1;
        //int radius = Char.myCharz().myskill.dx;
        //int segments = 160; // More segments = smoother circle

        //for (int j = 0; j < segments; j++)
        //{
        //    double angle1 = (double)j / segments * 2.0 * Math.PI;
        //    double angle2 = (double)(j + 1) / segments * 2.0 * Math.PI;

        //    int p1x = centerX + (int)(radius * Mathf.Cos((float)angle1));
        //    int p1y = centerY + (int)(radius * Mathf.Sin((float)angle1));
        //    int p2x = centerX + (int)(radius * Mathf.Cos((float)angle2));
        //    int p2y = centerY + (int)(radius * Mathf.Sin((float)angle2));
        //    g.setColor(UnityEngine.Color.blue);
        //    g.drawLine(p1x, p1y, p2x, p2y);
        //}

    }
    public static void DrawWaypointInMap(mGraphics g)
    {
        for (int i = 0; i < TileMap.vGo.size(); i++)
        {
            Waypoint wp = TileMap.vGo.elementAt(i) as Waypoint;
            if (wp != null)
            {
                g.setColor(UnityEngine.Color.red);
                g.drawRect(wp.minX - GameScr.cmx, wp.minY - GameScr.cmy, wp.maxX - wp.minX, wp.maxY - wp.minY);
            }
        }
    }
    public void perform(int idAction, object p)
    {
    }

}