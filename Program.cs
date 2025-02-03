using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    //스탯 클래스
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

    //아이템 스탯 클래스
    public class itemStat
    {
        private int plusAtk;
        private int plusDef;
        private float plusHP;
        private string itemName;
        private int itemID;
        private bool hasItem;
        private bool equippedItem;
        private string itemDescription;
        private int sellingGold;

        public int PlusAtk { get { return plusAtk; } set { plusAtk = value; } }
        public int PlusDef { get { return plusDef; } set { plusDef = value; } }
        public float PlusHP { get { return plusHP; } set { plusHP = value; } }
        public string ItemName { get { return itemName; } set { itemName = value; } }
        public int ItemID { get { return itemID; } set { itemID = value; } }
        public bool HasItem { get { return hasItem; } set { hasItem = value; } }
        public bool EquippedItem { get { return equippedItem; } set { equippedItem = value; } }
        public string ItemDescription { get { return itemDescription; } set { itemDescription = value; } }
        public int SellingGold { get { return sellingGold; } set { sellingGold = value; } }

      
    }

    internal class Program
    { 
        //명령어 함수
        static public int Command()
        {
            int cmd;

            int.TryParse(Console.ReadLine(), out cmd);

            return cmd;
        }

        //명령어 잘못입력시
        static public void WrongCommand()
        {
            Console.WriteLine();
            Console.WriteLine("잘못된 입력입니다");
            Console.WriteLine();
            Console.WriteLine();
        }

        static public void StatusChcek(stat myStat, List<itemStat> itemList)
        {
            int atkUp = 0;
            int defUp = 0;
            float hpUp = 0;

            //장착한 장비가 잇는지 확인
            for (int i = 0; i < itemList.Count; i++)
            {
                if (itemList[i].EquippedItem == true)
                {
                    if (itemList[i].PlusAtk != 0)
                    {
                        atkUp = atkUp + itemList[i].PlusAtk;
                    }
                    if (itemList[i].PlusDef != 0)
                    {
                        defUp = defUp + itemList[i].PlusDef;
                    }
                    if (itemList[i].PlusHP != 0)
                    {
                        hpUp = hpUp + itemList[i].PlusHP;
                    }

                }
            }

            myStat.ResultAtk = myStat.Atk + atkUp;
            myStat.ResultDef = myStat.Def + defUp;
            myStat.ResultHp = myStat.Hp + hpUp;

            Console.WriteLine("공격력 : {0} (+{1})", myStat.ResultAtk, atkUp);
            Console.WriteLine("방어력 : {0} (+{1})", myStat.ResultDef, defUp);
            Console.WriteLine("최대체력 : {0} (+{1})", myStat.ResultHp, hpUp);
            Console.WriteLine("현재체력 : {0}", myStat.NowHp);
            Console.WriteLine("Gold : {0} G", myStat.Gold);
        }
        //유저 스탯 확인기능
        static void Status(stat myStat, List<itemStat> itemList)
        {
            while (true)
            {
                int cmd;
                
                Console.WriteLine("상태 보기");
                Console.WriteLine("캐릭터의 정보가 표시됩니다");
                Console.WriteLine();
                Console.WriteLine("Lv.{0}", myStat.Level);
                Console.WriteLine("{0} ({1})", myStat.Name, myStat.JopClass);
                StatusChcek(myStat, itemList);
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
        //장비세팅기능
        static List<itemStat> ItemSet(List<itemStat> itemList)
        {
            itemStat Amomr1;
            itemStat Spear1;
            itemStat Sword1;
            itemStat Amomr2;
            itemStat Amomr3;
            itemStat Axe1;
            itemStat shoes1;
            itemStat shoes2;
            itemStat shoes3;
            itemStat Sword2;

            Amomr1 = new itemStat() { ItemID = 1, ItemName = "무쇠갑옷", PlusAtk = 0, PlusDef = 9, PlusHP = 0, HasItem = true, ItemDescription = "무쇠로 만들어져 튼튼한 갑옷입니다", EquippedItem = false, SellingGold = 1500 };
            Spear1 = new itemStat() { ItemID = 2, ItemName = "스파르타의 창", PlusAtk = 7, PlusDef = 0, PlusHP = 0, HasItem = false, ItemDescription = "스파르타의 전사들이 사용했다는 전설의 창입니다", EquippedItem = false, SellingGold = 2500 };
            Sword1 = new itemStat() { ItemID = 3, ItemName = "낡은 검", PlusAtk = 2, PlusDef = 0, PlusHP = 0, HasItem = true, ItemDescription = "쉽게 볼 수 있는 낡은 검 입니다.", EquippedItem = false, SellingGold = 600 };
            Amomr2 = new itemStat() { ItemID = 4, ItemName = "수련자갑옷", PlusAtk = 0, PlusDef = 5, PlusHP = 0, HasItem = false, ItemDescription = "수련에 도움을 주는 갑옷입니다.", EquippedItem = false, SellingGold = 1000 };
            Amomr3 = new itemStat() { ItemID = 5, ItemName = "스파르타갑옷", PlusAtk = 0, PlusDef = 15, PlusHP = 0, HasItem = false, ItemDescription = "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", EquippedItem = false, SellingGold = 3500 };
            Axe1 = new itemStat() { ItemID = 6, ItemName = "청동 도끼", PlusAtk = 5, PlusDef = 0, PlusHP = 0, HasItem = false, ItemDescription = "어디선가 사용햇던거 같은 도끼입니다.", EquippedItem = false, SellingGold = 1500 };
            shoes1 = new itemStat() { ItemID = 7, ItemName = "낡은 신발", PlusAtk = 0, PlusDef = 0, PlusHP = 3, HasItem = false, ItemDescription = "낡은 신발입니다.", EquippedItem = false, SellingGold = 300 };
            shoes2 = new itemStat() { ItemID = 8, ItemName = "천 신발", PlusAtk = 0, PlusDef = 0, PlusHP = 8, HasItem = false, ItemDescription = "천으로 된 신발입니다.", EquippedItem = false, SellingGold = 800 };
            shoes3 = new itemStat() { ItemID = 9, ItemName = "고급 신발", PlusAtk = 0, PlusDef = 0, PlusHP = 15, HasItem = false, ItemDescription = "고급지게 생긴 신발입니다.", EquippedItem = false, SellingGold = 1500 };
            Sword2 = new itemStat() { ItemID = 10, ItemName = "치트 검", PlusAtk = 999, PlusDef = 0, PlusHP = 0, HasItem = false, ItemDescription = "모든 던전을 통과하게 해주는 검입니다.", EquippedItem = false, SellingGold = 9000 };

            itemList.Add(Amomr1);
            itemList.Add(Spear1);
            itemList.Add(Sword1);
            itemList.Add(Amomr2);
            itemList.Add(Amomr3);
            itemList.Add(Axe1);
            itemList.Add(shoes1);
            itemList.Add(shoes2);
            itemList.Add(shoes3);
            itemList.Add(Sword2);

            return itemList;
        }

        //장비 장착 기능
        static void EquimentSetting(List<itemStat> itemList)
        {
            int cmd;
            while (true)
            {
                Console.WriteLine("인벤토리 - 장착 관리");
                Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다");
                Console.WriteLine();
                Console.WriteLine("[아이템 목록]");
                Console.WriteLine();
                for (int i = 0; i < itemList.Count; i++)
                {
                    //보유한 아이템 표시
                    if (itemList[i].HasItem == true)
                    {
                        Console.Write("- ");
                        Console.Write(" {0} ", itemList[i].ItemID);
                        //아이템 장착 표시
                        if (itemList[i].EquippedItem == true)
                        {
                            Console.Write("[E]");
                        }
                        Console.Write(itemList[i].ItemName);
                        Console.Write(" | ");
                        //장비가 실제로 보유한 능력치만 표기
                        if (itemList[i].PlusAtk != 0)
                        {
                            Console.Write("공격력 + {0} | ", itemList[i].PlusAtk);
                        }

                        if (itemList[i].PlusDef != 0)
                        {
                            Console.Write("방어력 + {0} | ", itemList[i].PlusDef);
                        }

                        if (itemList[i].PlusHP != 0)
                        {
                            Console.Write("체력 + {0} | ", itemList[i].PlusHP);
                        }
                        Console.Write(itemList[i].ItemDescription);
                        Console.WriteLine();

                    }
                }

                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">>");
                Console.WriteLine();
                Console.WriteLine("0. 나가기");

                cmd = Command();
                //장비 장착 해제 기능
                for (int i = 0; i < itemList.Count; i++)
                {
                    if (cmd == itemList[i].ItemID && itemList[i].EquippedItem == false)
                    {
                        itemList[i].EquippedItem = true;
                    }

                    else if (cmd == itemList[i].ItemID && itemList[i].EquippedItem == true)
                    {
                        itemList[i].EquippedItem = false;
                    }
                }
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

        //인벤토리 표시기능
        static void NowInventory(stat myStat, List<itemStat> itemList)
        {
            int cmd;

            while (true)
            {
                Console.WriteLine("인벤토리");
                Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다");
                Console.WriteLine();
                Console.WriteLine("[아이템 목록]");
                Console.WriteLine();
                for (int i = 0; i < itemList.Count; i++)
                {
                    //보유한 아이템 표시
                    if (itemList[i].HasItem == true)
                    {
                        Console.Write("- ");
                        if (itemList[i].EquippedItem == true)
                        {
                            Console.Write("[E]");
                        }
                        Console.Write(itemList[i].ItemName);
                        Console.Write(" | ");
                        //실제로 보유한 능력치만 표기
                        if (itemList[i].PlusAtk != 0)
                        {
                            Console.Write("공격력 + {0} | ", itemList[i].PlusAtk);
                        }

                        if (itemList[i].PlusDef != 0)
                        {
                            Console.Write("방어력 + {0} | ", itemList[i].PlusDef);
                        }

                        if (itemList[i].PlusHP != 0)
                        {
                            Console.Write("체력 + {0} | ", itemList[i].PlusHP);
                        }
                        Console.Write(itemList[i].ItemDescription);
                        Console.WriteLine();

                    }
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
                    //장비장착관리기능으로 이동
                    EquimentSetting(itemList);                    
                }

                else
                {
                    WrongCommand();
                    continue;
                }
            }
        }

        //상점 구매기능
        static void ItemBuy(stat myStat, List<itemStat> itemList)
        {
            int cmd;

            while (true)
            {
                Console.WriteLine("상점 - 아이템 구매");
                Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
                Console.WriteLine();
                Console.WriteLine("[보유골드]");
                Console.WriteLine("{0} G", myStat.Gold);
                Console.WriteLine();
                Console.WriteLine("[아이템 목록]");

                for (int i = 0; i < itemList.Count; i++)
                {
                    Console.Write("- ");
                    Console.Write(" {0} ", itemList[i].ItemID);
                    Console.Write(itemList[i].ItemName);
                    Console.Write(" | ");
                    //실제로 보유한 능력치만 표기
                    if (itemList[i].PlusAtk != 0)
                    {
                        Console.Write("공격력 + {0} | ", itemList[i].PlusAtk);
                    }
                    if (itemList[i].PlusDef != 0)
                    {
                        Console.Write("방어력 + {0} | ", itemList[i].PlusDef);
                    }
                    if (itemList[i].PlusHP != 0)
                    {
                        Console.Write("체력 + {0} | ", itemList[i].PlusHP);
                    }
                    Console.Write(itemList[i].ItemDescription);
                    Console.Write(" | ");
                    if (itemList[i].HasItem == true)
                    {
                        Console.Write("구매완료");
                    }
                    else
                    {
                        Console.Write("{0} G", itemList[i].SellingGold);
                    }
                    Console.WriteLine();

                }
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">>");
                Console.WriteLine();
                Console.WriteLine("0. 나가기");

                cmd = Command();
                for (int i = 0; i < itemList.Count; i++)
                {
                    if (cmd == itemList[i].ItemID && itemList[i].HasItem == false && myStat.Gold >= itemList[i].SellingGold)
                    {
                        itemList[i].HasItem = true;
                        myStat.Gold = myStat.Gold - itemList[i].SellingGold;
                        Console.WriteLine("구매를 완료했습니다");
                    }

                    if (cmd == itemList[i].ItemID && itemList[i].HasItem == false && myStat.Gold < itemList[i].SellingGold)
                    {
                        Console.WriteLine("Gold 가 부족합니다.");
                    }
                    if (cmd == 0)
                    {
                        break;
                    }

                }
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

        //상점 판매기능
        static void ItemSell(stat myStat, List<itemStat> itemList)
        {
            int cmd;

            while (true)
            {
                Console.WriteLine("상점 - 아이템 판매");
                Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
                Console.WriteLine();
                Console.WriteLine("[보유골드]");
                Console.WriteLine("{0} G", myStat.Gold);
                Console.WriteLine();
                Console.WriteLine("[아이템 목록]");

                for (int i = 0; i < itemList.Count; i++)
                {
                    //보유한 아이템 표시
                    if (itemList[i].HasItem == true)
                    {
                        Console.Write("- ");
                        Console.Write(" {0} ", itemList[i].ItemID);
                        //아이템 장착 표시
                        if (itemList[i].EquippedItem == true)
                        {
                            Console.Write("[E]");
                        }
                        Console.Write(itemList[i].ItemName);
                        Console.Write(" | ");
                        //장비가 실제로 보유한 능력치만 표기
                        if (itemList[i].PlusAtk != 0)
                        {
                            Console.Write("공격력 + {0} | ", itemList[i].PlusAtk);
                        }

                        if (itemList[i].PlusDef != 0)
                        {
                            Console.Write("방어력 + {0} | ", itemList[i].PlusDef);
                        }

                        if (itemList[i].PlusHP != 0)
                        {
                            Console.Write("체력 + {0} | ", itemList[i].PlusHP);
                        }
                        Console.Write(itemList[i].ItemDescription);
                        Console.WriteLine();

                    }
                }
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">>");
                Console.WriteLine();
                Console.WriteLine("0. 나가기");

                cmd = Command();
                for (int i = 0; i < itemList.Count; i++)
                {
                    if (cmd == itemList[i].ItemID && itemList[i].HasItem == true)
                    {
                        itemList[i].HasItem = false;
                        myStat.Gold = (float)(myStat.Gold + (itemList[i].SellingGold * 0.85));
                        Console.WriteLine("구매를 완료했습니다");
                    }
                    if (cmd == 0)
                    {
                        break;
                    }

                }
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

        //상점기능
        static void Shop(stat myStat, List<itemStat> itemList)
        {
            int cmd;

            while (true)
            {
                Console.WriteLine("상점");
                Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
                Console.WriteLine();
                Console.WriteLine("[보유골드]");
                Console.WriteLine("{0} G", myStat.Gold);
                Console.WriteLine();
                Console.WriteLine("[아이템 목록]");

                for (int i = 0; i < itemList.Count; i++)
                {
                    Console.Write("- ");
                    Console.Write(itemList[i].ItemName);
                    Console.Write(" | ");
                    //실제로 보유한 능력치만 표기
                    if (itemList[i].PlusAtk != 0)
                    {
                        Console.Write("공격력 + {0} | ", itemList[i].PlusAtk);
                    }
                    if (itemList[i].PlusDef != 0)
                    {
                        Console.Write("방어력 + {0} | ", itemList[i].PlusDef);
                    }
                    if (itemList[i].PlusHP != 0)
                    {
                        Console.Write("체력 + {0} | ", itemList[i].PlusHP);
                    }
                    Console.Write(itemList[i].ItemDescription);
                    Console.Write(" | ");
                    if (itemList[i].HasItem == true)
                    {
                        Console.Write("구매완료");
                    }
                    else
                    {
                        Console.Write("{0} G", itemList[i].SellingGold);
                    }
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
                    ItemBuy(myStat, itemList);
                }
                else if (cmd == 2)
                {
                    ItemSell(myStat, itemList);
                }
                else
                {
                    WrongCommand();
                    continue;
                }
            }
            
        }

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
                else if(cmd==1&& myStat.Gold < 500)
                {
                    Console.WriteLine("Gold 가 부족합니다.");
                }
                else if(cmd == 0)
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

        //던전입장전
        static  void EnterDungeon(stat myStat,List<itemStat> itemList)
        {
            int cmd;

            while (true)
            {
                Console.WriteLine("던전입장");
                Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다");
                Console.WriteLine();
                StatusChcek(myStat, itemList);
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
                else if (cmd == 1 && myStat.NowHp>=40)
                {
                    //쉬운던전
                    Dungen(myStat, cmd);
                    //Console.WriteLine("쉬운던전 입장");
                }
                else if(cmd ==2 && myStat.NowHp >= 40)
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
        static void Dungen(stat myStat,int cmd)
        {
            int SuccessProbability = new Random().Next(1, 100);

            if (cmd == 1 && myStat.Def >= 5)
            {
                //던전 클리어
                DungenClear(myStat, cmd);
                //Console.WriteLine("던전클리어");
            }
            else if (cmd == 1 && myStat.Def < 5 && SuccessProbability > 40)
            {
                //던전 클리어
                DungenClear(myStat, cmd);
                //Console.WriteLine("던전클리어");
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
                //Console.WriteLine("던전클리어");
            }
            else if (cmd == 2 && myStat.Def < 11 && SuccessProbability > 40)
            {
                //던전 클리어
                DungenClear(myStat, cmd);
                //Console.WriteLine("던전클리어");
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
                //Console.WriteLine("던전클리어");
            }
            else if (cmd == 3 && myStat.Def < 17 && SuccessProbability > 40)
            {
                //던전 클리어
                DungenClear(myStat, cmd);
                //Console.WriteLine("던전클리어");
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
        static void DungenClear(stat myStat,int cmd)
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
                    //Console.WriteLine(goldluk);
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

        static void DungeonFail(stat myStat)
        {
            int cmd;
            float hptemp = myStat.NowHp;

            while (true)
            {
                myStat.NowHp = myStat.NowHp - (myStat.NowHp / 2);
                Console.WriteLine("던전 실패하셧습니다");
                Console.WriteLine("hp : {0} -> {1}",hptemp,myStat.NowHp);
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

        //경험치상승 및 레벨업
        static void ExpUp(stat myStat)
        {
            myStat.Exp++;
            if (myStat.Exp == myStat.Level)
            {
                myStat.Level++;
                myStat.Exp = 0;
                myStat.Atk++;
                myStat.Def++;
                Console.WriteLine("레벨이 상승했습니다");
                Console.WriteLine("레벨 : {0} -> {1}", myStat.Level - 1, myStat.Level);
                Console.WriteLine("공격력 :  {0} -> {1}", myStat.Atk - 1, myStat.Atk);
                Console.WriteLine("방어력 :  {0} -> {1}", myStat.Def - 1, myStat.Def);
            }
        }

        static void Main(string[] args)
        {
            int command;
            stat myStat;
   
            myStat = new stat() { Level = 1, Name = "kim", JopClass = " 전사", Atk = 10, Def = 5, Hp = 100, Gold = 1500 , ResultAtk = 10, ResultDef = 5, ResultHp = 100, NowHp=100, Exp =0 };
            List<itemStat> itemList = new List<itemStat>();

            ItemSet(itemList);

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
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">>");
                Console.WriteLine();
                command = Command();

                if (command == 1)
                {
                    Console.WriteLine();
                    Console.WriteLine();
                    Status(myStat, itemList);
                    continue;
                }
                if(command == 2)
                {
                    Console.WriteLine();
                    Console.WriteLine();
                    NowInventory(myStat, itemList);
                    continue;
                }

                if(command == 3)
                {
                    Console.WriteLine();
                    Console.WriteLine();
                    Shop(myStat,itemList);
                    continue;
                }

                if (command == 4)
                {
                    Console.WriteLine();
                    Console.WriteLine();
                    EnterDungeon(myStat,itemList);
                    continue;
                }

                if (command == 5)
                {
                    Console.WriteLine();
                    Console.WriteLine();
                    Rest(myStat);
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