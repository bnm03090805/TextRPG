using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace TextRPG2
{
    public class stat
    {
        private int level;
        private string name;
        private string jopClass;
        private int atk;
        private int def;
        private float hp;
        private float gold;
        private int resultAtk;
        private int resultDef;
        private float resultHp;
        private float nowHp;
        private int exp;

        public int Level { get { return level; } set { level = value; } }
        public string Name { get { return name; } set { name = value; } }
        public string JopClass { get { return jopClass; } set { jopClass = value; } }
        public int Atk { get { return atk; } set { atk = value; } }
        public int Def { get { return def; } set { def = value; } }
        public float Hp { get { return hp; } set { hp = value; } }
        public float Gold { get { return gold; } set { gold = value; } }
        public int ResultAtk { get { return resultAtk; } set { resultAtk = value; } }
        public int ResultDef { get { return resultDef; } set { resultDef = value; } }
        public float ResultHp { get { return resultHp; } set { resultHp = value; } }
        public float NowHp { get { return nowHp; } set { nowHp = value; } }
        public int Exp { get { return exp; } set { exp = value; } }
    }

    public abstract class item
    {
        private string itemName;
        private int itemID;
        private string itemDescription;
        private int sellingGold;
        private int itemType;
        private bool eItem; // 장비장착

        public string ItemName { get { return itemName; } set { itemName = value; } }
        public int ItemID { get { return itemID; } set { itemID = value; } }
        public string ItemDescription { get { return itemDescription; } set { itemDescription = value; } }
        public int SellingGold { get { return sellingGold; } set { sellingGold = value; } }
        public int ItemType { get { return itemType; } set { if (value == 1 || value == 2 || value == 3) itemType = value; } } // 1 무기 2 방어구 3 신발
        public bool EItem { get { return eItem; } set { eItem = value; } }

        //자식 클래스의 능력치 관련 함수
        public abstract int PlusValue();

    }

    public class weapon : item
    {
        private int plusAtk;
        public int PlusAtk { get { return plusAtk; } set { plusAtk = value; } }

        public override int PlusValue()
        {
            //throw new NotImplementedException();

            return PlusAtk;
        }
    }

    public class armor : item
    {
        private int plusDef;
        public int PlusDef { get { return plusDef; } set { plusDef = value; } }

        public override int PlusValue()
        {
            //throw new NotImplementedException();

            return PlusDef;
        }
    }

    public class shoes : item
    {
        private int plusHP;
        public int PlusHP { get { return plusHP; } set { plusHP = value; } }

        public override int PlusValue()
        {
            //throw new NotImplementedException();

            return plusHP;
        }

    }
    public class SaveItemData
    {
        public int itemID;
        public bool Eitem;
    }

    public class SaveData
    {
        public stat myStat;
        public List<SaveItemData> saveItemData;
    }

    public class SaveLoadSys
    {

        public static void SaveData(SaveData data, String FilePath)
        {
            try
            {
                string userData = JsonConvert.SerializeObject(data, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(FilePath, userData);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public static SaveData LoadData(String FilePath)
        {
            if (File.Exists(FilePath))
            {
                string userData = File.ReadAllText(FilePath);

                SaveData Save =  JsonConvert.DeserializeObject<SaveData>(userData);

                return Save;
            }
            else
            {
                return null;
            }

        }

    }
    class Program
    {
        //명령어 입력
        static int Command()
        {
            int cmd;

            int.TryParse(Console.ReadLine(), out cmd);

            return cmd;
        }

        //예외처리 텍스트부분
        static void WrongCommand()
        {
            Console.WriteLine();
            Console.WriteLine("잘못된 입력입니다");
            Console.WriteLine();
            Console.WriteLine();
        }

        //스테이터스 갱신
        static void StatusChcek(stat myStat, List<item> myItems)
        {
            int atkUp = 0;
            int defUp = 0;
            float hpUp = 0;

            for (int i = 0; i < myItems.Count; i++)
            {
                if (myItems[i].EItem == true)
                {
                    if (myItems[i].ItemType == 1)
                    {
                        atkUp = atkUp + myItems[i].PlusValue();
                    }
                    else if (myItems[i].ItemType == 2)
                    {
                        defUp = defUp + myItems[i].PlusValue();
                    }
                    else if (myItems[i].ItemType == 3)
                    {
                        hpUp = hpUp + myItems[i].PlusValue();
                    }
                }
            }

            myStat.ResultAtk = myStat.Atk + atkUp;
            myStat.ResultDef = myStat.Def + defUp;
            myStat.ResultHp = myStat.Hp + hpUp;

            Console.WriteLine("Lv.{0}", myStat.Level);
            Console.WriteLine("{0} ({1})", myStat.Name, myStat.JopClass);
            Console.WriteLine("공격력 : {0} (+{1})", myStat.ResultAtk, atkUp);
            Console.WriteLine("방어력 : {0} (+{1})", myStat.ResultDef, defUp);
            Console.WriteLine("최대체력 : {0} (+{1})", myStat.ResultHp, hpUp);
            Console.WriteLine("현재체력 : {0}", myStat.NowHp);
            Console.WriteLine("Gold : {0} G", myStat.Gold);
            Console.WriteLine("경험치 : {0} / {1}", myStat.Exp,myStat.Level*2);
        }

        //스테이터스 확인 기능
        static void Status(stat myStat, List<item> myItems)
        {
            while (true)
            {
                int cmd;

                Console.WriteLine("상태 보기");
                Console.WriteLine("캐릭터의 정보가 표시됩니다");
                Console.WriteLine();
                StatusChcek(myStat, myItems);
                Console.WriteLine();
                Console.WriteLine("0. 나가기");
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">>");
                Console.WriteLine();
                cmd = Command();

                if (cmd == 0)
                {
                    break;
                }
                else
                {
                    WrongCommand();
                    continue;
                }
            }
        }

        //전체 아이템 세팅기능
        static void EntireItem(List<item> items)
        {
            items.Add(new weapon() { ItemID = 2, ItemName = "스파르타의 창", PlusAtk = 7, ItemDescription = "스파르타의 전사들이 사용했다는 전설의 창입니다", SellingGold = 2500, ItemType = 1, EItem = false });
            items.Add(new weapon() { ItemID = 3, ItemName = "낡은 검", PlusAtk = 2, ItemDescription = "쉽게 볼 수 있는 낡은 검 입니다.", SellingGold = 600, ItemType = 1, EItem = false });
            items.Add(new weapon() { ItemID = 6, ItemName = "청동 도끼", PlusAtk = 5, ItemDescription = "어디선가 사용햇던거 같은 도끼입니다.", SellingGold = 1500, ItemType = 1, EItem = false });
            items.Add(new weapon() { ItemID = 10, ItemName = "치트 검", PlusAtk = 999, ItemDescription = "돈복사 버그를 사용하게 해주는 검입니다.", SellingGold = 99999, ItemType = 1, EItem = false });
            items.Add(new armor() { ItemID = 1, ItemName = "무쇠갑옷", PlusDef = 8, ItemDescription = "무쇠로 만들어져 튼튼한 갑옷입니다", SellingGold = 1500, ItemType = 2, EItem = false });
            items.Add(new armor() { ItemID = 4, ItemName = "수련자갑옷", PlusDef = 5, ItemDescription = "수련에 도움을 주는 갑옷입니다.", SellingGold = 1000, ItemType = 2, EItem = false });
            items.Add(new armor() { ItemID = 5, ItemName = "스파르타갑옷", PlusDef = 15, ItemDescription = "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", SellingGold = 3500, ItemType = 2, EItem = false });
            items.Add(new armor() { ItemID = 11, ItemName = "치트 방어구", PlusDef = 999, ItemDescription = "모든 던전을 통과하게 해주는 방어구입니다.", SellingGold = 99999, ItemType = 2, EItem = false });
            items.Add(new shoes() { ItemID = 7, ItemName = "낡은 신발", PlusHP = 3, ItemDescription = "낡은 신발입니다.", SellingGold = 300, ItemType = 3, EItem = false });
            items.Add(new shoes() { ItemID = 8, ItemName = "천 신발", PlusHP = 8, ItemDescription = "천으로 된 신발입니다.", SellingGold = 800, ItemType = 3, EItem = false });
            items.Add(new shoes() { ItemID = 9, ItemName = "고급 신발", PlusHP = 15, ItemDescription = "고급지게 생긴 신발입니다.", SellingGold = 1500, ItemType = 3, EItem = false });

            //아이템 ID로 정렬
            items.Sort((x1, x2) => x1.ItemID.CompareTo(x2.ItemID));
        }
        //현재 현재인벤토리 확인기능
        static void NowInventory(List<item> myItems)
        {
            int cmd;
            myItems.Sort((x1, x2) => x1.ItemID.CompareTo(x2.ItemID));

            while (true)
            {
                Console.WriteLine("인벤토리");
                Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다");
                Console.WriteLine();
                Console.WriteLine("[아이템 목록]");
                Console.WriteLine();
                for (int i = 0; i < myItems.Count; i++)
                {
                    Console.Write("- ");
                    if (myItems[i].EItem == true)
                    {
                        Console.Write("[E]");
                    }
                    Console.Write(myItems[i].ItemName);
                    Console.Write(" | ");
                    if (myItems[i].ItemType == 1)
                    {
                        Console.Write("공격력 + {0} | ", myItems[i].PlusValue());
                    }
                    else if (myItems[i].ItemType == 2)
                    {
                        Console.Write("방어력 + {0} | ", myItems[i].PlusValue());
                    }

                    else if (myItems[i].ItemType == 3)
                    {
                        Console.Write("체력 + {0} | ", myItems[i].PlusValue());
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">>");
                Console.WriteLine();
                Console.WriteLine("1. 장착 관리");
                Console.WriteLine("0. 나가기");

                cmd = Command();
                if (cmd == 0)
                {
                    break;
                }
                else if (cmd == 1)
                {
                    EquimentSetting(myItems);
                }

                else
                {
                    WrongCommand();
                }
            }
        }
        //장비 장착 기능
        static void EquimentSetting(List<item> myItems)
        {
            int cmd;
            while (true)
            {
                Console.WriteLine("인벤토리 - 장착 관리");
                Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다");
                Console.WriteLine();
                Console.WriteLine("[아이템 목록]");
                Console.WriteLine();
                //장비표시
                for (int i = 0; i < myItems.Count; i++)
                {
                    Console.Write("- ");
                    Console.Write("{0} ", myItems[i].ItemID);
                    //장착한 장비표시
                    if (myItems[i].EItem == true)
                    {
                        Console.Write("[E]");
                    }
                    Console.Write(myItems[i].ItemName);
                    Console.Write(" | ");
                    //무기타입별로 표시되는 능력치 표시
                    if (myItems[i].ItemType == 1)
                    {
                        Console.Write("공격력 + {0} | ", myItems[i].PlusValue());
                    }
                    else if (myItems[i].ItemType == 2)
                    {
                        Console.Write("방어력 + {0} | ", myItems[i].PlusValue());
                    }

                    else if (myItems[i].ItemType == 3)
                    {
                        Console.Write("체력 + {0} | ", myItems[i].PlusValue());
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">>");
                Console.WriteLine();
                Console.WriteLine("0. 나가기");

                cmd = Command();

                if (cmd == 0)
                {
                    break;
                }
                else
                {
                    //입력한 커맨드가 없는 장비번호일경우
                    if (myItems.FindIndex(x => x.ItemID.Equals(cmd)) == -1)
                    {
                        Console.WriteLine();
                        Console.WriteLine("잘못된 입력입니다");
                        Console.WriteLine();
                    }
                    else
                    {
                        if (myItems[myItems.FindIndex(x => x.ItemID.Equals(cmd))].EItem == false)
                        {
                            Console.WriteLine();
                            Console.WriteLine();

                            //장비장착전 장착할려는 장비와 같은타입의 모든 장비를 장착 해제
                            var find = myItems.FindAll(x => x.ItemType == myItems[myItems.FindIndex(y => y.ItemID.Equals(cmd))].ItemType);
                            foreach (var item in find)
                            {
                                item.EItem = false;
                            }
                            //장비 장착
                            Console.WriteLine("{0} 장착", myItems[myItems.FindIndex(x => x.ItemID.Equals(cmd))].ItemName);
                            myItems[myItems.FindIndex(x => x.ItemID.Equals(cmd))].EItem = true;
                            Console.WriteLine();
                        }

                        else if(myItems[myItems.FindIndex(x => x.ItemID.Equals(cmd))].EItem == true)
                        {
                            Console.WriteLine();
                            Console.WriteLine("{0} 해제", myItems[myItems.FindIndex(x => x.ItemID.Equals(cmd))].ItemName);
                            myItems[myItems.FindIndex(x => x.ItemID.Equals(cmd))].EItem = false;
                            Console.WriteLine();
                        }
                    }

                }

            }
        }

        //상점기능
        static void Shop(stat myStat, List<item> items, List<item> myItems)
        {
            int cmd;
            var compare = items.Intersect(myItems);
            var expect = items.Except(myItems);

            while (true)
            {
                Console.WriteLine("상점");
                Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
                Console.WriteLine();
                Console.WriteLine("[보유골드]");
                Console.WriteLine("{0} G", myStat.Gold);
                Console.WriteLine();
                Console.WriteLine("[아이템 목록]");

                //보유하지 않은 장비 표시
                foreach (var item in expect)
                {
                    Console.Write("- ");
                    Console.Write(item.ItemName);
                    Console.Write(" | ");
                    if (item.ItemType == 1)
                    {
                        Console.Write("공격력 + {0} | ", item.PlusValue());
                    }
                    else if (item.ItemType == 2)
                    {
                        Console.Write("방어력 + {0} | ", item.PlusValue());
                    }
                    else if (item.ItemType == 3)
                    {
                        Console.Write("체력 + {0} | ", item.PlusValue());
                    }
                    Console.Write(item.ItemDescription);
                    Console.Write(" | ");
                    Console.Write("{0} G", item.SellingGold);
                    Console.WriteLine();
                }

                //보유한 장비 표시
                Console.WriteLine("=================================구매완료=================================");
                foreach (var item in compare)
                {
                    Console.Write("- ");
                    Console.Write(item.ItemName);
                    Console.Write(" | ");
                    if (item.ItemType == 1)
                    {
                        Console.Write("공격력 + {0} | ", item.PlusValue());
                    }
                    else if (item.ItemType == 2)
                    {
                        Console.Write("방어력 + {0} | ", item.PlusValue());
                    }
                    else if (item.ItemType == 3)
                    {
                        Console.Write("체력 + {0} | ", item.PlusValue());
                    }
                    Console.Write(item.ItemDescription);
                    Console.Write(" | ");
                    Console.Write("구매완료");
                    Console.WriteLine();
                }

                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">>");
                Console.WriteLine();
                Console.WriteLine("1. 아이템 구매");
                Console.WriteLine("2. 아이템 판매");
                Console.WriteLine("0. 나가기");

                cmd = Command();
                if (cmd == 0)
                {
                    break;
                }
                else if (cmd == 1)
                {
                    ItemBuy(myStat, items, myItems);
                }
                else if (cmd == 2)
                {
                    ItemSell(myStat, items, myItems);
                }
                else
                {
                    WrongCommand();
                    continue;
                }
            }

        }

        //아이템 구매기능
        static void ItemBuy(stat myStat, List<item> items, List<item> myItems)
        {
            int cmd;
            var compare = items.Intersect(myItems);
            var expect = items.Except(myItems);

            while (true)
            {
                Console.WriteLine("상점 - 아이템 구매");
                Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
                Console.WriteLine();
                Console.WriteLine("[보유골드]");
                Console.WriteLine("{0} G", myStat.Gold);
                Console.WriteLine();
                Console.WriteLine("[아이템 목록]");

                //미보유 장비 표시
                foreach (var item in expect)
                {
                    Console.Write("- ");
                    Console.Write(" {0} ", item.ItemID);
                    Console.Write(item.ItemName);
                    Console.Write(" | ");
                    if (item.ItemType == 1)
                    {
                        Console.Write("공격력 + {0} | ", item.PlusValue());
                    }
                    else if (item.ItemType == 2)
                    {
                        Console.Write("방어력 + {0} | ", item.PlusValue());
                    }
                    else if (item.ItemType == 3)
                    {
                        Console.Write("체력 + {0} | ", item.PlusValue());
                    }
                    Console.Write(item.ItemDescription);
                    Console.Write(" | ");
                    Console.Write("{0} G", item.SellingGold);
                    Console.WriteLine();
                }

                //보유장비 표시
                Console.WriteLine("=================================구매완료=================================");
                foreach (var item in compare)
                {
                    Console.Write("- ");
                    //Console.Write(" {0} ", myItems.IndexOf(item)+1);
                    Console.Write(item.ItemName);
                    Console.Write(" | ");
                    if (item.ItemType == 1)
                    {
                        Console.Write("공격력 + {0} | ", item.PlusValue());
                    }
                    else if (item.ItemType == 2)
                    {
                        Console.Write("방어력 + {0} | ", item.PlusValue());
                    }
                    else if (item.ItemType == 3)
                    {
                        Console.Write("체력 + {0} | ", item.PlusValue());
                    }
                    Console.Write(item.ItemDescription);
                    Console.Write(" | ");
                    Console.Write("구매완료");
                    Console.WriteLine();
                }

                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">>");
                Console.WriteLine();
                Console.WriteLine("0. 나가기");

                cmd = Command();
                if (cmd == 0)
                {
                    break;
                }

                else
                {                    
                    if (items.FindIndex(x => x.ItemID.Equals(cmd)) == -1)
                    {
                        Console.WriteLine("잘못된 입력입니다");
                    }

                    else if (myStat.Gold >= items[items.FindIndex(x => x.ItemID.Equals(cmd))].SellingGold)
                    {
                        myStat.Gold = myStat.Gold - items[items.FindIndex(x => x.ItemID.Equals(cmd))].SellingGold;
                        myItems.Add(items[items.FindIndex(Item => Item.ItemID.Equals(cmd))]);
                        Console.WriteLine("구매를 완료했습니다");
                    }
                    else if (myStat.Gold < items[items.FindIndex(x => x.ItemID.Equals(cmd))].SellingGold)
                    {
                        Console.WriteLine("Gold 가 부족합니다.");
                    }                    
                }
            }
        }

        //아이템 판매기능
        static void ItemSell(stat myStat, List<item> items, List<item> myItems)
        {
            int cmd;
            var compare = items.Intersect(myItems);
            var expect = items.Except(myItems);

            while (true)
            {
                Console.WriteLine("상점 - 아이템 판매");
                Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
                Console.WriteLine();
                Console.WriteLine("[보유골드]");
                Console.WriteLine("{0} G", myStat.Gold);
                Console.WriteLine();
                Console.WriteLine("[아이템 목록]");


                foreach (var item in compare)
                {
                    Console.Write("- ");
                    Console.Write(" {0} ", item.ItemID);
                    Console.Write(item.ItemName);
                    Console.Write(" | ");
                    if (item.ItemType == 1)
                    {
                        Console.Write("공격력 + {0} | ", item.PlusValue());
                    }
                    else if (item.ItemType == 2)
                    {
                        Console.Write("방어력 + {0} | ", item.PlusValue());
                    }
                    else if (item.ItemType == 3)
                    {
                        Console.Write("체력 + {0} | ", item.PlusValue());
                    }
                    Console.Write(item.ItemDescription);
                    Console.Write(" | ");
                    Console.Write("판매가 : {0} G", item.SellingGold * 0.85);
                    Console.WriteLine();
                }

                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">>");
                Console.WriteLine();
                Console.WriteLine("0. 나가기");

                cmd = Command();
                if (cmd == 0)
                {
                    break;
                }

                else
                {
                    if (myItems.FindIndex(x => x.ItemID.Equals(cmd)) == -1)
                    {
                        Console.WriteLine("잘못된 입력입니다");
                    }

                    else
                    {
                        myStat.Gold = (float)(myStat.Gold + (myItems[myItems.FindIndex(x => x.ItemID.Equals(cmd))].SellingGold * 0.85));
                        myItems.Remove(myItems[myItems.FindIndex(x => x.ItemID.Equals(cmd))]);
                        Console.WriteLine("판매를 완료했습니다");
                    }
                }
            }

        }

        //휴식기능
        static void Rest(stat myStat)
        {
            int cmd;

            while (true)
            {
                Console.WriteLine("휴식하기");
                Console.WriteLine("500 G 를 내면 체력을 회복할 수 있습니다 (현재체력 : {0} , 보유골드 : {1} G", myStat.NowHp, myStat.Gold);
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">>");
                Console.WriteLine();
                Console.WriteLine("1. 휴식하기");
                Console.WriteLine("0. 나가기");
                cmd = Command();
                if (cmd == 1 && myStat.Gold >= 500)
                {
                    Console.WriteLine("체력이 회복됩니다.");
                    myStat.NowHp = myStat.NowHp + 50;
                    myStat.Gold = myStat.Gold - 500;
                    if (myStat.NowHp > myStat.ResultHp)
                    {
                        myStat.NowHp = myStat.ResultHp;
                    }
                }
                else if (cmd == 1 && myStat.Gold < 500)
                {
                    Console.WriteLine("Gold 가 부족합니다.");
                }
                else if (cmd == 0)
                {
                    break;
                }
                else
                {
                    WrongCommand();
                    continue;
                }
            }
        }

        //경험치상승 및 레벨업
        static void ExpUp(stat myStat)
        {
            myStat.Exp++;
            if (myStat.Exp == myStat.Level*2)
            {
                myStat.Level++;
                myStat.Exp = 0;
                myStat.Atk++;
                myStat.Def++;
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("레벨이 상승했습니다");
                Console.WriteLine("레벨 : {0} -> {1}", myStat.Level - 1, myStat.Level);
                Console.WriteLine("공격력 :  {0} -> {1}", myStat.Atk - 1, myStat.Atk);
                Console.WriteLine("방어력 :  {0} -> {1}", myStat.Def - 1, myStat.Def);
            }
        }

        //던전입장전
        static void EnterDungeon(stat myStat, List<item> myItems)
        {
            int cmd;

            while (true)
            {
                Console.WriteLine("던전입장");
                Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다");
                Console.WriteLine();
                StatusChcek(myStat, myItems);
                Console.WriteLine();
                Console.WriteLine("1. 쉬운 던전     | 방어력 5 이상 권장");
                Console.WriteLine("2. 일반 던전     | 방어력 11 이상 권장");
                Console.WriteLine("3. 어려운 던전     | 방어력 17 이상 권장");
                Console.WriteLine("0. 나가기");

                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">>");
                Console.WriteLine();

                cmd = Command();
                if (cmd == 0)
                {
                    break;
                }
                else if (cmd == 1 && myStat.NowHp >= 40)
                {
                    //쉬운던전
                    Dungen(myStat, cmd);
                    //Console.WriteLine("쉬운던전 입장");
                }
                else if (cmd == 2 && myStat.NowHp >= 40)
                {
                    Dungen(myStat, cmd);
                    // Console.WriteLine("일반던전 입장");
                }
                else if (cmd == 3 && myStat.NowHp >= 40)
                {
                    Dungen(myStat, cmd);
                    //Console.WriteLine("어려운던전 입장");
                }
                else if (myStat.NowHp < 40)
                {
                    //던전 입장 불가
                    Console.WriteLine("체력이 낮아 입장이 불가능합니다.");
                }
                else
                {
                    WrongCommand();
                    continue;
                }
            }
        }

        //던전 진입시
        static void Dungen(stat myStat, int cmd)
        {
            int SuccessProbability = new Random().Next(1, 100);

            if (cmd == 1 && myStat.Def >= 5)
            {
                //던전 클리어
                DungenClear(myStat, cmd);
            }
            else if (cmd == 1 && myStat.Def < 5 && SuccessProbability > 40)
            {
                //던전 클리어
                DungenClear(myStat, cmd);
            }
            else if (cmd == 1 && myStat.Def < 5 && SuccessProbability < 40)
            {
                //던전 실패
                DungeonFail(myStat);
            }
            else if (cmd == 2 && myStat.Def >= 11)
            {
                // 던전 클리어
                DungenClear(myStat, cmd);
            }
            else if (cmd == 2 && myStat.Def < 11 && SuccessProbability > 40)
            {
                //던전 클리어
                DungenClear(myStat, cmd);
            }
            else if (cmd == 2 && myStat.Def < 11 && SuccessProbability < 40)
            {
                //던전 실패
                DungeonFail(myStat);
            }
            else if (cmd == 3 && myStat.Def >= 17)
            {
                // 던전 클리어
                DungenClear(myStat, cmd);
            }
            else if (cmd == 3 && myStat.Def < 17 && SuccessProbability > 40)
            {
                //던전 클리어
                DungenClear(myStat, cmd);
            }
            else if (cmd == 3 && myStat.Def < 17 && SuccessProbability < 40)
            {
                DungeonFail(myStat);
            }

            else
            {
                Console.WriteLine("이상한경우");
            }
        }

        //던전클리어
        static void DungenClear(stat myStat, int cmd)
        {
            int command;
            float hptemp = myStat.NowHp;
            float goldtemp = myStat.Gold;


            while (true)
            {
                Console.WriteLine("던전 클리어");
                Console.WriteLine("축하합니다!!");
                if (cmd == 1)
                {
                    Console.WriteLine("쉬운 던전을 클리어 하였습니다");
                    int hppro = new Random().Next(20 - (myStat.Def - 5), 35 - (myStat.Def - 5));
                    int goldluk = new Random().Next(myStat.ResultAtk, myStat.ResultAtk * 2);
                    myStat.Gold = (float)(myStat.Gold + 1000 + (1000 * (goldluk * 0.01)));
                    myStat.NowHp = myStat.NowHp - hppro;
                    Console.WriteLine("[탐험 결과]");
                    Console.WriteLine("체력 {0} -> {1}", hptemp, myStat.NowHp);
                    Console.WriteLine("Gold {0} -> {1}", goldtemp, myStat.Gold);


                }
                else if (cmd == 2)
                {
                    Console.WriteLine("일반 던전을 클리어 하였습니다");
                    int hppro = new Random().Next(20 - (myStat.Def - 11), 35 - (myStat.Def - 11));
                    int goldluk = new Random().Next(myStat.ResultAtk, myStat.ResultAtk * 2);
                    myStat.Gold = (float)(myStat.Gold + 1700 + (1700 * (goldluk * 0.01)));
                    myStat.NowHp = myStat.NowHp - hppro;
                    Console.WriteLine("[탐험 결과]");
                    Console.WriteLine("체력 {0} -> {1}", hptemp, myStat.NowHp);
                    Console.WriteLine("Gold {0} -> {1}", goldtemp, myStat.Gold);
                }
                else if (cmd == 3)
                {
                    Console.WriteLine("어려운 던전을 클리어 하였습니다");
                    int hppro = new Random().Next(20 - (myStat.Def - 17), 35 - (myStat.Def - 17));
                    int goldluk = new Random().Next(myStat.ResultAtk, myStat.ResultAtk * 2);
                    myStat.Gold = (float)(myStat.Gold + 2500 + (2500 * (goldluk * 0.01)));
                    myStat.NowHp = myStat.NowHp - hppro;
                    Console.WriteLine("[탐험 결과]");
                    Console.WriteLine("체력 {0} -> {1}", hptemp, myStat.NowHp);
                    Console.WriteLine("Gold {0} -> {1}", goldtemp, myStat.Gold);
                }
                else
                {
                    Console.WriteLine("이상한 던전을 클리어 하였습니다");
                }

                //경험치상승
                ExpUp(myStat);

                Console.WriteLine();
                Console.WriteLine("0. 나가기");

                command = Command();
                if (command == 0)
                {
                    break;
                }
                else
                {
                    WrongCommand();
                    break;
                }
            }

        }

        //던전실패
        static void DungeonFail(stat myStat)
        {
            int cmd;
            float hptemp = myStat.NowHp;

            while (true)
            {
                myStat.NowHp = myStat.NowHp - (myStat.NowHp / 2);
                Console.WriteLine("던전 실패하셧습니다");
                Console.WriteLine("hp : {0} -> {1}", hptemp, myStat.NowHp);
                Console.WriteLine();
                Console.WriteLine("0. 나가기");

                cmd = Command();
                {
                    if (cmd == 0)
                    {
                        break;
                    }
                    else
                    {
                        WrongCommand();
                        break;
                    }
                }
            }

        }

        static void Main(string[] args)
        {
            string FilePath = @"C:\Users\user\source\repos\TextRPG\TextRPG\save.json";
            int command;
            stat myStat = new stat();
            List<item> entireItems = new List<item>();
            List<item> myItems = new List<item>();
            List<SaveItemData> saveItemData = new List<SaveItemData>();
            EntireItem(entireItems);

            //저장된 파일이 있다면 불러오기
            if (File.Exists(FilePath))
            {                
                SaveData loadData = SaveLoadSys.LoadData(FilePath);
                myStat = loadData.myStat;
                saveItemData = loadData.saveItemData;
                int t = 0;
                while (true)
                {
                    

                    if (saveItemData[t].itemID == 0)
                    {
                        break;
                    }
                    else
                    {
                        myItems.Add(entireItems[entireItems.FindIndex(item => item.ItemID.Equals(saveItemData[t].itemID)) + 1]);
                        myItems[t].EItem = saveItemData[t].Eitem;

                        t = t + 1;
                    }

                }
                for(int i = 0; i < saveItemData.Count; i++)
                {
                    //myItems.Add(entireItems[entireItems.FindIndex(item => item.ItemID.Equals(i+1))]);
                    //myItems[i].EItem = true;
                   
                }
            }
            //없다면 초기세팅
            else
            {
                myStat.Level = 1;
                myStat.Name = "kim";
                myStat.JopClass = "전사";
                myStat.Atk = 10;
                myStat.Def = 5;
                myStat.Hp = 100;
                myStat.Gold = 1500;
                myStat.ResultAtk = 10;
                myStat.ResultDef = 5;
                myStat.ResultHp = 100;
                myStat.NowHp = 100;
                myStat.Exp = 0;
                
                myItems.Add(entireItems[entireItems.FindIndex(item => item.ItemID.Equals(1))]);
                myItems.Add(entireItems[entireItems.FindIndex(item => item.ItemID.Equals(2))]);

                for (int i = 0; i < entireItems.Count; i++)
                {
                    saveItemData.Add(new SaveItemData { Eitem = false, itemID = 0 });
                }
            }
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
                Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");
                Console.WriteLine("1. 상태 보기");
                Console.WriteLine("2. 인벤토리");
                Console.WriteLine("3. 상점");
                Console.WriteLine("4. 던전입장");
                Console.WriteLine("5. 휴식하기");
                Console.WriteLine("6. 저장하기");
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">>");
                Console.WriteLine();
                command = Command();

                if (command == 1)
                {
                    Console.WriteLine();
                    Console.WriteLine();
                    Status(myStat, myItems);
                    continue;
                }
                if (command == 2)
                {
                    Console.WriteLine();
                    Console.WriteLine();
                    NowInventory(myItems);
                    continue;
                }

                if (command == 3)
                {
                    Console.WriteLine();
                    Console.WriteLine();
                    Shop(myStat, entireItems, myItems);
                    continue;
                }

                if (command == 4)
                {
                    Console.WriteLine();
                    Console.WriteLine();
                    EnterDungeon(myStat, myItems);
                    continue;
                }

                if (command == 5)
                {
                    Console.WriteLine();
                    Console.WriteLine();
                    Rest(myStat);
                    continue;
                }

                if (command == 6)
                {
                    Console.WriteLine();
                    Console.WriteLine();
                    for (int i = 0; i < myItems.Count; i++)
                    {
                        saveItemData[i].itemID = myItems[i].ItemID;
                        saveItemData[i].Eitem = myItems[i].EItem;
                    }
                    SaveData saveData = new SaveData { myStat = myStat, saveItemData = saveItemData};
                    SaveLoadSys.SaveData(saveData, FilePath);
                    Console.WriteLine("저장이 완료 되었습니다");
                    Console.WriteLine();
                    Console.WriteLine();
                    continue;
                }

                else
                {
                    WrongCommand();
                    continue;
                }
            }

        }
    }
}
