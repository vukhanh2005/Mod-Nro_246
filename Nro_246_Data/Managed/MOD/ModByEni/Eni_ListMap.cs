using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Eni_ListMap
{
    public List<Eni_Map> list;
    public Eni_ListMap()
    {
        list = new List<Eni_Map>();
        list.Add(new Eni_Map(42, new Dictionary<int, int> { { 0, 0 } }, "Vách núi Aru", 0));
        list.Add(new Eni_Map(0, new Dictionary<int, int> { { 42, 1 }, {1, 0}}, "Làng Aru", 0));
        list.Add(new Eni_Map(1, new Dictionary<int, int> { { 0, 0 }, { 47, 2 }, { 2, 1 } }, "Đồi Hoa Cúc", 0));
        list.Add(new Eni_Map(2, new Dictionary<int, int> { { 1, 0}, { 24, 2 }, { 3, 1 } }, "Thung lũng tre", 0));
        list.Add(new Eni_Map(3, new Dictionary<int, int> { { 2, 0}, { 4, 1 }, { 27, 2 } }, "Rừng nấm", 0));
        list.Add(new Eni_Map(4, new Dictionary<int, int> { { 5, 1 }, { 3, 0 }}, "Rừng xương", 0));
        list.Add(new Eni_Map(5, new Dictionary<int, int> { { 4, 0 }, { 6, 1 }, { 29, 2 } }, "Đảo kame", 0));
        list.Add(new Eni_Map(6, new Dictionary<int, int> { { 5, 0 }}, "Đông Karin", 0));
        list.Add(new Eni_Map(47, new Dictionary<int, int> { { 0, 1 } }, "Karin", 0));
        list.Add(new Eni_Map(28, new Dictionary<int, int> { { 29, 1 }, { 27, 0 } }, "Rừng dương xỉ", 0));
        list.Add(new Eni_Map(29, new Dictionary<int, int> { { 5, 2 }, { 30, 1 }, { 28, 0 } }, "Nam kame", 0));
        list.Add(new Eni_Map(30, new Dictionary<int, int> { { 29, 0 } }, "Đảo bulong", 0));
        list.Add(new Eni_Map(27, new Dictionary<int, int> { { 28, 1 }, { 3, 0 } }, "Rừng bamboo", 0));
        list.Add(new Eni_Map(43, new Dictionary<int, int> { { 7, 0 } }, "Vách núi mori", 1));
        list.Add(new Eni_Map(7, new Dictionary<int, int> { { 43, 2 }, { 8, 0 } }, "Làng mori", 1));
        list.Add(new Eni_Map(8, new Dictionary<int, int> { { 7, 0 }, { 9, 1 } }, "Đồi nấm tím", 1));
        list.Add(new Eni_Map(9, new Dictionary<int, int> { { 8, 0 }, { 25, 2 }, { 11, 1 } }, "Thị trấn mori", 1));
        list.Add(new Eni_Map(11, new Dictionary<int, int> { { 9, 0 }, { 31, 2 }, { 12, 1 } }, "Thung lũng maima", 1));
        list.Add(new Eni_Map(12, new Dictionary<int, int> { { 11, 0 }, { 13, 1 }}, "Vực maima", 1));
        list.Add(new Eni_Map(13, new Dictionary<int, int> { { 12, 0 }, { 33, 2 }, { 10, 1 } }, "Đảo guru", 1));
        list.Add(new Eni_Map(10, new Dictionary<int, int> { { 13, 0 }}, "Thung lũng namek", 1));
        list.Add(new Eni_Map(33, new Dictionary<int, int> { { 32, 0 }, { 13, 1 }, { 34, 2 } }, "Nam Guru", 1));
        list.Add(new Eni_Map(34, new Dictionary<int, int> { { 33, 0 }}, "Đảo bulong", 1));
        list.Add(new Eni_Map(32, new Dictionary<int, int> { { 33, 1 }, { 31, 0 }}, "Núi hóa tím", 1));
        list.Add(new Eni_Map(31, new Dictionary<int, int> { { 32, 1 }, { 11, 0 }}, "Núi hoa vàng", 1));
        list.Add(new Eni_Map(44, new Dictionary<int, int> { { 14, 0 }}, "Vách núi kakalot", 2));
        list.Add(new Eni_Map(14, new Dictionary<int, int> { { 44, 1 }, { 15, 0 }}, "Làng kakalot", 2));
        list.Add(new Eni_Map(15, new Dictionary<int, int> { { 14, 0 }, { 16, 1 }}, "Đồi hoang", 2));
        list.Add(new Eni_Map(16, new Dictionary<int, int> { { 15, 0 }, { 26, 2 }, { 17, 1 } }, "Làng Plant", 2));
        list.Add(new Eni_Map(17, new Dictionary<int, int> { { 16, 0 }, { 35, 2 }, { 18, 1 } }, "Rừng nguyên sinh", 2));
        list.Add(new Eni_Map(18, new Dictionary<int, int> { { 17, 0 }, { 20, 1 }}, "Rừng thông xayda", 2));
        list.Add(new Eni_Map(20, new Dictionary<int, int> { { 18, 0 }, { 37, 2 }, { 19, 1 } }, "Vách núi đen", 2));
        list.Add(new Eni_Map(19, new Dictionary<int, int> { { 20, 0 }}, "Thành phố vegeta", 2));
        list.Add(new Eni_Map(37, new Dictionary<int, int> { { 20, 0 }, { 38, 1 }, { 36, 2 } }, "Thung lũng đen", 2));
        list.Add(new Eni_Map(38, new Dictionary<int, int> { { 37, 0 }}, "Bờ vực đen", 2));
        list.Add(new Eni_Map(36, new Dictionary<int, int> { { 37, 1 }, { 35, 0 }}, "Rừng đá", 2));
        list.Add(new Eni_Map(35, new Dictionary<int, int> { { 36, 1 }, { 17, 0 }}, "Rừng cọ", 2));
        list.Add(new Eni_Map(24, new Dictionary<int, int> { { 2, 0 }, {25, -1}, { 26, -2 } }, "Trạm tàu vũ trụ", 0));
        list.Add(new Eni_Map(25, new Dictionary<int, int> { { 9, 0 }, { 24, -3 }, { 26, -2 } }, "Trạm tàu vũ trụ", 1));
        list.Add(new Eni_Map(26, new Dictionary<int, int> { { 16, 0 }, { 24, -3 }, { 25, -1 } }, "Trạm tàu vũ trụ", 2));
    }
}
