using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerStats : MonoBehaviour
{
    public float PlayerMaxHp, PlayerNowHp,PlayerAutoHP;
    public float PlayerMaxMp, PlayerNowMp, PlayerAutoMP;
    public float  PlayerCriticalPer, PlayerCriticalDmg;
    public int PlayerAD;
    public float PlayerDodgePer;
    [SerializeField]
    private bool CallAutoMpBool=true;
    public float time ,intTime;

    // Start is called before the first frame update
    void Awake()
    {
        //PlayerMaxHp = 100;
       // PlayerNowHp = 100;
       // PlayerAutoHP = 0;

        //PlayerMaxMp = 100;
       // PlayerNowMp = 100;
        //PlayerAutoMP = 3;

       
       // PlayerCriticalPer = 0;
       // PlayerCriticalDmg = 50;

        //PlayerDodgePer = 0;

    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        intTime = Mathf.Floor(time);
        //time = 
        if (intTime % 4 == 0) { 
            CallAutoMp();
            CallAutoMpBool = false;
        }
        else
            CallAutoMpBool = true;
        //CallAutoMpBool = false;

        if (Input.GetKeyDown(KeyCode.X))
        {
            PlayerNowMp -= 5;
        }

        if (PlayerMaxHp<PlayerNowHp)
        {
            PlayerNowHp = PlayerMaxHp;
        }

        if (PlayerMaxMp < PlayerNowMp)
        {
            PlayerNowMp = PlayerMaxMp;
        }
        if (PlayerNowMp<0)
        {
            PlayerNowMp = 0;
        }
        DamgeCheck();
    }

    void CallAutoMp()
    {
        if (CallAutoMpBool ==true)
            PlayerNowMp += PlayerAutoMP;
    }
    public void DamgeCheck()
    {
        //bool sibal =PlayerDamaged.Dods_ChanceMaker.GetThisChanceResult_Percentage(30);
    }
}

