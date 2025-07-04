using System;
using UnityEngine;

public class ModGame
{
    public static bool isAutoAttackToggled = false;
    public static long lastAttackTime = 0;
    public static int currentSpeedGame = 1;
    public static Char targetLocked_Char = null;
    public static Mob targetLocked_Mob = null;
    public static int idLocked = -1;
    public static bool isLocked = false;
    private static bool IsTargetInRange(Char attacker, IMapObject target)
    {
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
        //press m de mo doi khu vuc
        if (keyCode == 109)
        {
            Service.gI().openUIZone();
        }
        //press k fo tdlt
        if (keyCode == 107)
        {
            if(UseItemAt(521))
            {
                GameScr.isAutoPlay = true;
            }
        }
        //press a de bat tu dong danh
        if (keyCode == 97)
        {
            isAutoAttackToggled = !isAutoAttackToggled;

            if (isAutoAttackToggled)
            {
                GameScr.info1.addInfo("Đã bật tự động đánh", 1);
            }
            else
            {
                GameScr.info1.addInfo("Đã tắt tự động đánh", 1);
            }
        }
        //increase speed game
        if(keyCode == 49)
        {
            currentSpeedGame++;
            GameScr.info1.addInfo("Game speed: " + currentSpeedGame, 1);
            if(currentSpeedGame > 5)
            {
                currentSpeedGame = 1;
            }
            setSpeedGame(currentSpeedGame);
        }
        //press 2
        if(keyCode == 50)
        {
            isLocked = !isLocked;
            if(isLocked == false)
            {
                targetLocked_Char = null;
                targetLocked_Mob = null;
                return;
            }
            targetLocked_Char = Char.myCharz().charFocus;
            targetLocked_Mob = Char.myCharz().mobFocus;

            if(targetLocked_Char == null && targetLocked_Mob == null)
            {
                isLocked = false;
                return;
            }
            if(targetLocked_Char != null)
            {
                idLocked = Char.myCharz().charFocus.charID;
                GameScr.info1.addInfo("Đã khóa: " + targetLocked_Char.cName, 0);
            }
            if(targetLocked_Mob != null)
            {
                idLocked = Char.myCharz().charFocus.charID;
                GameScr.info1.addInfo("Đã khóa: " + targetLocked_Mob.mobName, 0);
            }
        }
    }
    public static void Update()
    {
        pickItem();
        setSpeedGame(currentSpeedGame);
        // GameCanvas.startOKDlg("DEBUG: ModGame.Update() called.");
        if (isAutoAttackToggled)
        {
            long currentTime = mSystem.currentTimeMillis();
            int attackCooldown = 100;

            if (currentTime - lastAttackTime >= attackCooldown)
            {
                Char myChar = Char.myCharz();
                if (myChar != null)
                {
                    // GameCanvas.startOKDlg("DEBUG: Update - myChar is not null.");
                    if (myChar.myskill != null)
                    {
                        // GameCanvas.startOKDlg("DEBUG: Update - myskill is not null. Skill ID: " + myChar.myskill.template.id);
                        if (myChar.myskill.template.isAttackSkill())
                        {
                            // GameCanvas.startOKDlg("DEBUG: Update - myskill is an attack skill.");
                            IMapObject target = null;

                            if (myChar.mobFocus != null)
                            {
                                target = myChar.mobFocus;
                                // GameCanvas.startOKDlg("DEBUG: Update - Target is mobFocus.");
                            }
                            else if (myChar.charFocus != null && myChar.isMeCanAttackOtherPlayer(myChar.charFocus))
                            {
                                target = myChar.charFocus;
                                // GameCanvas.startOKDlg("DEBUG: Update - Target is charFocus.");
                            }

                            if (target != null)
                            {
                                // GameCanvas.startOKDlg("DEBUG: Update - Target found.");
                                if (IsTargetInRange(myChar, target))
                                {
                                    // GameCanvas.startOKDlg("DEBUG: Update - Target is in range. Attacking...");
                                    MyVector vMob = new MyVector();
                                    MyVector vChar = new MyVector();

                                    if (target is Mob)
                                    {
                                        vMob.addElement(target);
                                    }
                                    else if (target is Char)
                                    {
                                        vChar.addElement(target);
                                    }

                                    Service.gI().sendPlayerAttack(vMob, vChar, 1);
                                    lastAttackTime = currentTime;
                                }
                                // else { GameCanvas.startOKDlg("DEBUG: Update - Target out of range."); }
                            }
                            // else { GameCanvas.startOKDlg("DEBUG: Update - No target focused."); }
                        }
                        // else { GameCanvas.startOKDlg("DEBUG: Update - myskill is not an attack skill."); }
                    }
                    // else { GameCanvas.startOKDlg("DEBUG: Update - myskill is null."); }
                }
                // else { GameCanvas.startOKDlg("DEBUG: Update - myChar is null."); }
            }
        }
        if(isLocked)
        {
            LockTarget();
        }
    }
    public static void setSpeedGame(int speed)
    {
        Time.timeScale = speed;
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

    public static void pickItem()
    {
        int pickedCount = 0;
        MyVector items = GameScr.vItemMap;
        for (int i = 0; i < items.size(); i++)
        {
            ItemMap itemMap = (ItemMap)items.elementAt(i);
            if (itemMap != null && itemMap.template != null)
            {
                // Check if the item is pickable (playerId is 0 for general drops, or matches current player's ID)
                if (itemMap.playerId == 0 || itemMap.playerId == Char.myCharz().charID)
                {
                    Service.gI().pickItem(itemMap.itemMapID);
                    pickedCount++;
                }
            }
        }
        if (pickedCount > 0)
        {
            //GameScr.info1.addInfo("Đã nhặt " + pickedCount + " vật phẩm có ID " + idItem, 1);
        }
        else
        {
            //GameScr.info1.addInfo("Không tìm thấy vật phẩm có ID " + idItem + " để nhặt.", 0);
        }
    }
    public static void LockTarget()
    {
        Char.myCharz().mobFocus = targetLocked_Mob;
        Char.myCharz().charFocus = targetLocked_Char;
    }
}