using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MapCreater : MonoBehaviour
{
    public GameObject[] Room = new GameObject[4];
    public GameObject Minimap, MapParent,um;
    public float mapPosX, mapPosY;
    public int RoomCnt,i,j;
    public bool[,] Dungeon = new bool[9, 9];
    public String[,] DungeonSpecial = new String[9, 9];
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void MapCreate(bool[,] a, String[,] b)
    {
        RoomCnt = Minimap.GetComponent<DunGeonMake>().RoomCntMax;
        //mapPosX = transform.position.x -;
        for (i = 1; i < 8; i++)
        {

            for (j = 1; j < 8; j++)
            {
                if (a[i, j] == true)
                {

                    mapPosX = -24+(j - 4) * 55;
                    mapPosY = 11-(i - 4) * 30;


                    um = GameObject.Instantiate(Room[0], new Vector3(mapPosX, mapPosY), Quaternion.identity);
                    um.transform.parent = MapParent.transform;

                    if (a[i - 1, j] == true)
                    {
                        //um.GetComponent<Portalmanager>().Left_Exist = true;
                        um.GetComponent<Portalmanager>().Up_Exist = true;
                        if (b[i - 1, j] != "")
                        {
                            //um.GetComponent<Portalmanager>().Left_Spec = true;
                            um.GetComponent<Portalmanager>().Up_Spec = true;
                        }
                        if (b[i - 1, j] == "Boss")
                        {
                            //um.GetComponent<Portalmanager>().Left_Boss = true;
                            um.GetComponent<Portalmanager>().Up_Boss = true;
                        }
                    }

                    if (a[i + 1, j] == true)
                    {
                        //um.GetComponent<Portalmanager>().Right_Exist = true;
                        um.GetComponent<Portalmanager>().Down_Exist = true;

                        if (b[i + 1, j] != "")
                        {
                           // um.GetComponent<Portalmanager>().Right_Spec = true;
                            um.GetComponent<Portalmanager>().Down_Spec = true;
                        }
                        if (b[i + 1, j] == "Boss")
                        {
                           // um.GetComponent<Portalmanager>().Right_Boss = true;
                            um.GetComponent<Portalmanager>().Down_Boss = true;
                        }
                    }

                    if (a[i, j + 1] == true)
                    {
                        um.GetComponent<Portalmanager>().Right_Exist = true;
                        //um.GetComponent<Portalmanager>().Down_Exist = true;

                        if (b[i, j + 1] != "")
                        {
                            //um.GetComponent<Portalmanager>().Down_Spec = true;
                            um.GetComponent<Portalmanager>().Right_Spec = true;
                        }
                        if (b[i, j + 1] == "Boss")
                        {
                            //um.GetComponent<Portalmanager>().Down_Boss = true;
                            um.GetComponent<Portalmanager>().Right_Spec = true;
                        }
                    }

                    if (a[i, j - 1] == true)
                    {
                        //um.GetComponent<Portalmanager>().Up_Exist = true;
                        um.GetComponent<Portalmanager>().Left_Exist = true;

                        if (b[i, j - 1] != "")
                        {
                            //um.GetComponent<Portalmanager>().Up_Spec = true;
                            um.GetComponent<Portalmanager>().Left_Spec = true;
                        }
                        if (b[i, j - 1] == "Boss")
                        {
                            //um.GetComponent<Portalmanager>().Up_Boss = true;
                            um.GetComponent<Portalmanager>().Left_Boss = true;
                        }
                    }

                }

            }
        }
        for (i = 1; i < 8; i++)
        {

            for (j = 1; j < 8; j++)
            {
                a[i, j] = false;
            }
        }
    }
                

}

