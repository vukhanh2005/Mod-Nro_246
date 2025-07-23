
using System;

public class SupportMove
{
    public static void teleportToWpByIndex(int index)
    {
        if (TileMap.vGo != null)
        {
            if (index >= 0 && index < TileMap.vGo.size())
            {
                Waypoint waypoint = (Waypoint)TileMap.vGo.elementAt(index);
                if (waypoint != null)
                {
                    // Bước 1: Thay đổi tọa độ nhân vật ở phía client
                    Char.myCharz().cx = waypoint.minX + (waypoint.maxX - waypoint.minX) / 2;
                    Char.myCharz().cy = waypoint.minY + 5;

                    // Bước 2: Gửi yêu cầu đổi map ngay lập tức
                    Service.gI().requestChangeMap();

                    // Hiển thị thông báo (tùy chọn)
                    if (waypoint.popup != null)
                        GameScr.info1.addInfo("Đang dịch chuyển đến: " + waypoint.popup.says[0], 0);
                    else
                        GameScr.info1.addInfo("Đang dịch chuyển...", 0);
                }
                else
                {
                    GameScr.info1.addInfo("Waypoint tại index " + index + " không hợp lệ.", 0);
                }
            }
            else
            {
                GameScr.info1.addInfo("Index waypoint không hợp lệ: " + index, 0);
            }
        }
        else
        {
            GameScr.info1.addInfo("Bản đồ hiện tại không có waypoint.", 0);
        }
    }
}
