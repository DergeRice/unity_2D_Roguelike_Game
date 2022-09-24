using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DunGeonMake : MonoBehaviour
{
    public bool MakingEnd,MakingON, MakingOFF,sival;
    
    public int RoomCntMax, RoomCnt,NextRoomCnt;
    public int i,j,k, BossRoomListCnt, NPCRoomListCnt,MiddleBossCnt, MiddleBossCntMax;
    public string UiName;
    public GameObject UiObject,MapCreator;
    public int DugeonCheckCnt, BossRoomCnt=0;
    public Sprite BossRoom, PlayerRoom,NPCRoom, MiddleBossRoom, NormalRoom;
    public bool RoomIsMax,DungeonSpChk;
    bool MiddleBossChk = true;
    int TempX, TempY;
    public bool temp = true;
    //[Serializable]
    public bool[,] Dungeon = new bool[9, 9];
    public int[] BossRoomListX = new int[10];
    public int[] BossRoomListY = new int[10];
    public int[] NPCRoomListX = new int[10];
    public int[] NPCRoomListY = new int[10];
    public String[,] DungeonSpecial = new String[9, 9];

    // Start is called before the first frame update
    void Start()
    {
        RoomCntMax = 9;
        RoomCnt = 1;
        MakingEnd = false;
        UiObject = null;
        MakingON = true;
        NextRoomCnt = 0;
        RoomIsMax = false;
        DungeonSpChk = false;
        BossRoomListCnt = 7;
        NPCRoomListCnt = 7;
        MiddleBossCnt = 0;


       
        Dungeon[4, 4] = true;
        GameObject.Find("Image 44").GetComponent<Image>().enabled = true;
        GameObject.Find("Image 44").GetComponent<Image>().sprite = PlayerRoom;
        DungeonSpecial[4, 4] = "Player";
        MapClear();
        

    }

    // Update is called once per frame
    void Update()
    {
        if (sival)
        {
            MapCheck();
            sival = false;
        }
        if (MakingON)
        {
            Making();
            MakingEnd = false;
        }
        else if (MakingOFF)
        {
            MakingEnd = false;
            //BossRoomCnt = 0;
            RoomCnt = 0;
            NextRoomCnt = 0;


            BossRoomCnt = 0;
            //RoomCnt = 0;
            MakingOFF = false;
            MiddleBossCnt = 0;
            MapClear();

        }

        if ((RoomCnt + 1 >= RoomCntMax) && !MakingEnd)
        {
            MakingON = false;
            MakingEnd = true;
            MakingAfter();
            StartCoroutine((LastCheck()));
        }
        /*BossRoom Indicator 보스룸 디버그할때 씨발 힘들다. 고생했다 완성.
        for (i = 1; i < 8; i++)
        {

            
            for (j = 1; j < 8; j++)
            {
                string um = "um " + i + j;
                UiObject = GameObject.Find(um);
               /* if (DungeonSpecial[i, j] == "Boss")
                {
                    
                    
                    UiObject.GetComponent<Image>().enabled = true;
                }
                else { UiObject.GetComponent<Image>().enabled = false; }

                if (DungeonSpecial[i, j] == "MiddleBoss")
                {


                    UiObject.GetComponent<Image>().enabled = true;
                }
                else { UiObject.GetComponent<Image>().enabled = false; }
                
            }
        }*/
    }

    public void Making()
    {
        
        for(i=1;i<8 ; i++)
        {
            
            
            for (j = 1;j<8 ; j++)
            {
                //Debug.Log(Dungeon[i, j]);
                if (Dungeon[i, j])
                {
                    
                    if (Dungeon[i + 1, j])//인접한 방이 없을 때
                    {
                        NextRoomCnt++;
                    }

                    if (Dungeon[i - 1, j])
                    {
                        NextRoomCnt++;
                    }
                    if (Dungeon[i , j+1])
                    {
                        NextRoomCnt++;
                    }
                    if (Dungeon[i, j - 1])
                    {
                        NextRoomCnt++;
                    }
                    //k++;
                    if (NextRoomCnt <= 2)//인접한 방개수가 2 개이하
                    {
                        if (!(RoomCnt + 1 >= RoomCntMax))//방 개수가 Max가 아니라면
                        {
                            if (PercentCal.GetPercent.GetThisChanceResult_Percentage(50))//50%확률로 방생성
                            {
                                int RanRoom = UnityEngine.Random.Range(1, 5);
                                if (RanRoom == 1)
                                {
                                    if (i + 1 < 8)
                                    {
                                        if (!Dungeon[i + 1, j])
                                        {
                                            CreateRoom(i + 1, j);
                                        }
                                    }


                                }
                                else
                                if (RanRoom == 2)
                                {
                                    if (i - 1 > 0)
                                    {
                                        if (!Dungeon[i - 1, j])
                                        {
                                            CreateRoom(i - 1, j);
                                        }
                                    }

                                }
                                else
                                if (RanRoom == 3)
                                {
                                    if (j + 1 < 8)
                                    {
                                        if (!Dungeon[i, j + 1])
                                        {
                                            CreateRoom(i, j + 1);
                                        }
                                    }

                                }
                                else
                                if (RanRoom == 4)
                                {
                                    if (j - 1 > 0)
                                    {
                                        if (!Dungeon[i, j - 1])
                                        {
                                            CreateRoom(i, j - 1);
                                        }
                                    }
                                }
                            }
                        }                  
                    }
                    NextRoomCnt = 0;
                }          
            }

        }
        
    }

    void MapClear()
    {
        BossRoomListCnt = 0;
        NPCRoomListCnt = 0;
        MiddleBossChk = true;
        //
        for (i = 1; i < 8; i++)
        {
            for (j = 1; j < 8; j++)
            {
                if (!(i == 4 && j == 4))
                {
                    Dungeon[i, j] = false;
                    
                    UiName = "Image " + i + j;
                    UiObject = GameObject.Find(UiName);
                    UiObject.GetComponent<Image>().enabled = false;
                    DungeonSpChk = false;
                    DungeonSpecial[i, j] = "";
                }
                
            }
            
        }
        for (i = 0; i < 7; i++)
        {
           
                BossRoomListX[i] = 0;
                BossRoomListY[i] = 0;
        
        }
                
        //BossRoomListX
       
    }
    void CreateRoom(int t,int q)
    {
        if (!Dungeon[t, q])
        {
            Dungeon[t, q] = true;
            UiName = "Image " + t + q;
            UiObject = GameObject.Find(UiName);
            UiObject.GetComponent<Image>().enabled = true;
            RoomCnt++;
            GameObject.Find(UiName).GetComponent<Image>().sprite = NormalRoom;
            DungeonSpecial[t, q] = "";
        }

    }
    void MapCheck()
    {
        //bool temp = true;
        for (i = 1; i < 8; i++)//보스룸 체크
        {
            for (j = 1; j < 8; j++)
            {
                if (Dungeon[i, j])
                {
                    DugeonCheckCnt++;
                }

                if (DungeonSpecial[i, j] == "Boss")
                {
                  
                    DungeonSpChk = true;
                }
            }
        }

        if (!DungeonSpChk||temp) //보스룸이 생성되지 않으면 
        {
            temp = false;
            RoomCnt = 0;
            NextRoomCnt = 0;

            RoomIsMax = false;

            BossRoomCnt = 0;
            //RoomCnt = 0;
            MakingON = true;

            MapClear();
            Making();
        }

        if (BossRoomListCnt > 1)//보스 룸이 1개 이상
        {
            int RealBossRoomX, RealBossRoomY, BossRoomRanDdom;
            BossRoomRanDdom = UnityEngine.Random.Range(1, BossRoomListCnt);

            RealBossRoomX = BossRoomListX[BossRoomRanDdom];
            RealBossRoomY = BossRoomListY[BossRoomRanDdom];

            for (i = 1; i < 8; i++)//보스룸 체크
            {
                for (j = 1; j < 8; j++)
                {
                    if (DungeonSpecial[i, j] == "Boss")
                    {
                        if (!(i == RealBossRoomX && j == RealBossRoomY))
                        {
                            string UiName = "Image " + i + j;
                            GameObject.Find(UiName).GetComponent<Image>().sprite = NormalRoom;
                            DungeonSpecial[i, j] = "";
                            BossRoomListX[i] = 0;
                            BossRoomListY[i] = 0;
                        }
                    }
                }
            }
        }
        for (i = 3; i < 6; i++)//보스룸이 플레이어 1칸 곁에 있는지 체크
        {
            for (j = 3; j < 6; j++)
            {
                if (DungeonSpecial[i, j] == "Boss")
                {  //temp = false;

                    //  temp = false; 
                    RoomCnt = 0;
                    NextRoomCnt = 0;

                    RoomIsMax = false;

                    BossRoomCnt = 0;
                    //RoomCnt = 0;
                    MakingON = true;

                    MapClear();
                    Making();  //다시 만들기
                }
            }
        }

        if (NPCRoomListCnt > 1)//NPC 룸이 1개 이상
        {
            int RealNPCRoomX, RealNPCRoomY, NPCRoomRanDdom;
            int RealMiddleBossRoomX, RealMiddleBossRoomY, MiddleBossRoomRanDdom;

            NPCRoomRanDdom = UnityEngine.Random.Range(1, NPCRoomListCnt);
            MiddleBossRoomRanDdom = UnityEngine.Random.Range(1, NPCRoomListCnt);

            if (NPCRoomRanDdom == MiddleBossRoomRanDdom) 
                MiddleBossRoomRanDdom --;

            RealNPCRoomX = NPCRoomListX[NPCRoomRanDdom];
            RealNPCRoomY = NPCRoomListY[NPCRoomRanDdom];

            RealMiddleBossRoomX = NPCRoomListX[MiddleBossRoomRanDdom];
            RealMiddleBossRoomY = NPCRoomListY[MiddleBossRoomRanDdom];

            for (i = 1; i < 8; i++)//NPC룸 체크
            {
                for (j = 1; j < 8; j++)
                {
                    if (DungeonSpecial[i, j] == "NPC")
                    {
                        if ((!(i == RealNPCRoomX && j == RealNPCRoomY)))
                        {
                            //Debug.Log(NPCRoomRanDdom);
                            string UiName = "Image " + i + j;
                            GameObject.Find(UiName).GetComponent<Image>().sprite = NormalRoom;
                            DungeonSpecial[i, j] = "";
                            NPCRoomListX[i] = 0;
                            NPCRoomListY[i] = 0;
                            NPCRoomListCnt--;
                        }
                    }
                    if (i == RealMiddleBossRoomX && j == RealMiddleBossRoomY)
                    {

                        string UiName = "Image " + i + j;
                        GameObject.Find(UiName).GetComponent<Image>().sprite = MiddleBossRoom;
                        
                        DungeonSpecial[i, j] = "MiddleBoss";
                    }
                }
            }

        }

        for (i = 1; i < 8; i++)//중간보스룸 체크
        {
            for (j = 1; j < 8; j++)
            {
                if (Dungeon[i, j])
                {
                    // DugeonCheckCnt++;
                    if (DungeonSpecial[i, j] == "MiddleBoss")
                    {

                        MiddleBossCnt++;
                    }
                }

                
            }
        }


        
        if ( MiddleBossCnt == 0)
        {
            RoomCnt = 0;
            NextRoomCnt = 0;

            RoomIsMax = false;

            BossRoomCnt = 0;
            //RoomCnt = 0;
            MakingON = true;
            MapClear();
            Making();
            
        }
        if (NPCRoomListCnt > 1)
        {
            RoomCnt = 0;
            NextRoomCnt = 0;

            RoomIsMax = false;

            BossRoomCnt = 0;
            //RoomCnt = 0;
            MakingON = true;
            MapClear();
            Making();

        }



    }

    void MakeMiddleBoss()
    {
        int loopNum = 0;


        for (MiddleBossCnt = 0; MiddleBossChk; MiddleBossCnt++)
        {
            if (loopNum++ > 10000)
                throw new Exception("Infinite Loop");

            TempX = UnityEngine.Random.Range(1, 8);
            TempY = UnityEngine.Random.Range(1, 8);
            if ((TempX == 4 && TempY == 4))
            {
                break;

            }
            else if (Dungeon[TempX, TempY])
            {
                if ((!(TempX == 3 && TempY == 4)) && (!(TempX == 4 && TempY == 3)) && (!(TempX == 5 && TempY == 4)) && (!(TempX == 4 && TempY == 5)))//인접한 십자가 아니고
                {

                    //&& (!(TempX == 4 && TempY == 4))
                    //&& (!(DungeonSpecial[TempX, TempY] == "Player")
                    
                    if ((!(DungeonSpecial[TempX, TempY] == "Boss")) && (!(DungeonSpecial[TempX, TempY] == "NPC")))//보스방이 아니고 4,4가 아니고 NPC가 아니면
                    {
                        DungeonSpecial[TempX, TempY] = "MiddleBoss";
                        string UiName = "Image " + TempX + TempY;
                        GameObject.Find(UiName).GetComponent<Image>().sprite = MiddleBossRoom;
                        MiddleBossChk = false;
                        MiddleBossCnt++;
                        break;

                    }
                }
            }
        }
        
    }
    void MapSpecial()
    {
        int BossOptChkCnt=0;

        for (i = 1; i < 8; i++)
        {
            for (j = 1; j < 8; j++)
            {

                if (Dungeon[i + 1, j])//인접한 방이 없을 때
                {
                    BossOptChkCnt++;
                }

                if (Dungeon[i - 1, j])
                {
                    BossOptChkCnt++;
                }
                if (Dungeon[i, j + 1])
                {
                    BossOptChkCnt++;
                }
                if (Dungeon[i, j - 1])
                {
                    BossOptChkCnt++;
                }
                if (BossOptChkCnt == 1)
                {
                    if (Dungeon[i, j] == true)
                    {
                        if (!(i == 4 && j == 4))
                        {

                            UiName = "Image " + i + j;
                            UiObject = GameObject.Find(UiName);
                            DungeonSpecial[i, j] = "Boss";

                            GameObject.Find(UiName).GetComponent<Image>().sprite = BossRoom;


                        }
                    }
                }
                BossOptChkCnt = 0;
                if (DungeonSpecial[i, j] == "Boss")
                {
                    BossRoomListCnt++;
                    BossRoomListX[BossRoomListCnt] = i;
                    BossRoomListY[BossRoomListCnt] = j;
                }
                //Debug.Log("dd");
                if ((!(DungeonSpecial[i, j] == "Boss")) && (!(i == 4 && j == 4)))//보스방이 아니고 4,4가 아니면  //방이 있고
                {
                    if (Dungeon[i, j])
                    {

                        ///NPC방 생성 조건:인접한 십자 제외 모든곳
                        if ((!(i == 3 && j == 4)) && (!(i == 4 && j == 3)) && (!(i == 5 && j == 4) && (!(i == 4 && j == 5))))//인접한 십자가 아니고
                        {

                            
                                
                                DungeonSpecial[i, j] = "NPC";
                                string UiName = "Image " + i + j;
                                GameObject.Find(UiName).GetComponent<Image>().sprite = NPCRoom;

                            
                        }
                    }
                    if (DungeonSpecial[i, j] == "NPC")
                    {
                        NPCRoomListCnt++;
                        NPCRoomListX[NPCRoomListCnt] = i;
                        NPCRoomListY[NPCRoomListCnt] = j;
                    }
                }
                //중간 보스 만들기 조건은 똑같고 랜덤으로 하나 넣을예정
                //MapCheck();
                
                
            }

        }

    }
    void  MakingAfter()
    {
        //yield return new WaitForSeconds(0.1f);
        
        
        MapSpecial();

        MapCheck();
       

        //BossRoomCnt = 0;

    }

    IEnumerator LastCheck()
    {
        yield return new WaitForSeconds(1f);
        MapCreator.GetComponent<MapCreater>().MapCreate(Dungeon,DungeonSpecial);

    }


}
