using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    public GameObject player, Skill_1, Skill_2, Skill_3, Skill_4;
    public float NowMP, Skill_1_Cost, Skill_2_Cost, Skill_3_Cost, Skill_4_Cost,
        Skill_1_Cool, Skill_2_Cool, Skill_3_Cool, Skill_4_Cool,
        Skill_1_CoolNow, Skill_2_CoolNow, Skill_3_CoolNow, Skill_4_CoolNow;
    public bool AttackAble;


    void Start()
    {
        AttackAble = true;
        NowMP = player.GetComponent<PlayerStats>().PlayerNowMp;
        Skill_1_Cost = Skill_1.GetComponent<Skill_1_Script>().Skill_1_Cost;
        Skill_2_Cost = Skill_2.GetComponent<Skill_2_Script>().Skill_2_Cost;
        Skill_3_Cost = Skill_3.GetComponent<Skill_3_Script>().Skill_3_Cost;
        Skill_4_Cost = Skill_4.GetComponent<Skill_4_Script>().Skill_4_Cost;

        Skill_1_Cool = Skill_1.GetComponent<Skill_1_Script>().Skill_1_CoolTime;
        Skill_2_Cool = Skill_2.GetComponent<Skill_2_Script>().Skill_2_CoolTime;
        Skill_3_Cool = Skill_3.GetComponent<Skill_3_Script>().Skill_3_CoolTime;
        Skill_4_Cool = Skill_4.GetComponent<Skill_4_Script>().Skill_4_CoolTime;
    }


    private void Awake()
    {

    }
    // Update is called once per frame
    void Update()
    {
        
        Skill_1_CoolNow -= Time.deltaTime;
        Skill_2_CoolNow -= Time.deltaTime;
        Skill_3_CoolNow -= Time.deltaTime;
        Skill_4_CoolNow -= Time.deltaTime;

        NowMP = player.GetComponent<PlayerStats>().PlayerNowMp;
        Skill_1_Cost = Skill_1.GetComponent<Skill_1_Script>().Skill_1_Cost;
        Skill_2_Cost = Skill_2.GetComponent<Skill_2_Script>().Skill_2_Cost;
        Skill_3_Cost = Skill_3.GetComponent<Skill_3_Script>().Skill_3_Cost;
        Skill_4_Cost = Skill_4.GetComponent<Skill_4_Script>().Skill_4_Cost;

        Skill_1_Cool = Skill_1.GetComponent<Skill_1_Script>().Skill_1_CoolTime;
        Skill_2_Cool = Skill_2.GetComponent<Skill_2_Script>().Skill_2_CoolTime;
        Skill_3_Cool = Skill_3.GetComponent<Skill_3_Script>().Skill_3_CoolTime;
        Skill_4_Cool = Skill_4.GetComponent<Skill_4_Script>().Skill_4_CoolTime;

         Skill_1.GetComponent<Skill_1_Script>().Skill_1_CoolTimeNow=Skill_1_CoolNow ;
         Skill_2.GetComponent<Skill_2_Script>().Skill_2_CoolTimeNow= Skill_2_CoolNow;
         Skill_3.GetComponent<Skill_3_Script>().Skill_3_CoolTimeNow= Skill_3_CoolNow;
         Skill_4.GetComponent<Skill_4_Script>().Skill_4_CoolTimeNow= Skill_4_CoolNow ;

        if (Skill_1_CoolNow < 0) { Skill_1_CoolNow =0; }
        if (Skill_2_CoolNow < 0) { Skill_2_CoolNow = 0; }
        if (Skill_3_CoolNow < 0) { Skill_3_CoolNow = 0; }
        if (Skill_4_CoolNow < 0) { Skill_4_CoolNow = 0; }

    }
    private void FixedUpdate()
    {

    }
    public void AttackBtnClickedDown()
    {
        if (AttackAble == true) { 
        if (player.GetComponent<PlayerClass>().Class == "Knight") player.GetComponent<KnightClass>().AttackBtnClickedDown();
        if (player.GetComponent<PlayerClass>().Class == "MagicGun") player.GetComponent<MagicGunClass>().AttackBtnClickedDown();
        if (player.GetComponent<PlayerClass>().Class == "Magician") player.GetComponent<MagicianClass>().AttackBtnClickedDown();
        if (player.GetComponent<PlayerClass>().Class == "Mechanic") player.GetComponent<MechanicClass>().AttackBtnClickedDown();
        if (player.GetComponent<PlayerClass>().Class == "Slayer") player.GetComponent<SlayerClass>().AttackBtnClickedDown();
        if (player.GetComponent<PlayerClass>().Class == "Sniper") player.GetComponent<SniperClass>().AttackBtnClickedDown();
        if (player.GetComponent<PlayerClass>().Class == "MagicSword") player.GetComponent<MagicSwordClass>().AttackBtnClickedDown();
        }
        
         //DunGeonMake.Making();

    }
    public void AttackBtnClickedUp()
    {
        if (player.GetComponent<PlayerClass>().Class == "Knight") player.GetComponent<KnightClass>().AttackBtnClickedUp();
        if (player.GetComponent<PlayerClass>().Class == "MagicGun") player.GetComponent<MagicGunClass>().AttackBtnClickedUp();
        if (player.GetComponent<PlayerClass>().Class == "Magician") player.GetComponent<MagicianClass>().AttackBtnClickedUp();
        if (player.GetComponent<PlayerClass>().Class == "Mechanic") player.GetComponent<MechanicClass>().AttackBtnClickedUp();
        if (player.GetComponent<PlayerClass>().Class == "Slayer") player.GetComponent<SlayerClass>().AttackBtnClickedUp();
        if (player.GetComponent<PlayerClass>().Class == "Sniper") player.GetComponent<SniperClass>().AttackBtnClickedUp();
        if (player.GetComponent<PlayerClass>().Class == "MagicSword") player.GetComponent<MagicSwordClass>().AttackBtnClickedUp();

    }
    public void Skill_1_ClickedUp()
    {
        if (player.GetComponent<PlayerClass>().Class == "Knight") player.GetComponent<KnightClass>().Skill_1_ClickedUp();
        if (player.GetComponent<PlayerClass>().Class == "MagicGun") player.GetComponent<MagicGunClass>().Skill_1_ClickedUp();
        if (player.GetComponent<PlayerClass>().Class == "Magician") player.GetComponent<MagicianClass>().Skill_1_ClickedUp();
        if (player.GetComponent<PlayerClass>().Class == "Mechanic") player.GetComponent<MechanicClass>().Skill_1_ClickedUp();
        if (player.GetComponent<PlayerClass>().Class == "Slayer") player.GetComponent<SlayerClass>().Skill_1_ClickedUp();
        if (player.GetComponent<PlayerClass>().Class == "Sniper") player.GetComponent<SniperClass>().Skill_1_ClickedUp();
        if (player.GetComponent<PlayerClass>().Class == "MagicSword") player.GetComponent<MagicSwordClass>().Skill_1_ClickedUp();

    }
    public void Skill_1_ClickedDown()
    {
        if (player.GetComponent<PlayerStats>().PlayerNowMp > Skill_1_Cost&&AttackAble == true&& Skill_1.GetComponent<Skill_1_Script>().Skill_1_Able)
        {
            Skill_1.GetComponent<Skill_1_Script>().Skill_1_Able = false;
            player.GetComponent<PlayerStats>().PlayerNowMp -= Skill_1_Cost;
            AttackAble = false;
            Skill_1_CoolNow = Skill_1_Cool;
            
            if (player.GetComponent<PlayerClass>().Class == "Knight")
            {
                player.GetComponent<KnightClass>().Skill_1_ClickedDown();
            }
            if (player.GetComponent<PlayerClass>().Class == "MagicGun")
            {
                player.GetComponent<MagicGunClass>().Skill_1_ClickedDown();
            }
            if (player.GetComponent<PlayerClass>().Class == "Magician")
            {
                player.GetComponent<MagicianClass>().Skill_1_ClickedDown();
            }
            if (player.GetComponent<PlayerClass>().Class == "Mechanic")
            {
                player.GetComponent<MechanicClass>().Skill_1_ClickedDown();
            }
            if (player.GetComponent<PlayerClass>().Class == "Slayer")
            {
                player.GetComponent<SlayerClass>().Skill_1_ClickedDown();
            }
            if (player.GetComponent<PlayerClass>().Class == "Sniper")
            {
                player.GetComponent<SniperClass>().Skill_1_ClickedDown();
            }
            if (player.GetComponent<PlayerClass>().Class == "MagicSword")
            {
                player.GetComponent<MagicSwordClass>().Skill_1_ClickedDown();
            }
            
        }
    }
    public void Skill_2_ClickedUp()
    {
        if (player.GetComponent<PlayerClass>().Class == "Knight") player.GetComponent<KnightClass>().Skill_2_ClickedUp();
        if (player.GetComponent<PlayerClass>().Class == "MagicGun") player.GetComponent<MagicGunClass>().Skill_2_ClickedUp();
        if (player.GetComponent<PlayerClass>().Class == "Magician") player.GetComponent<MagicianClass>().Skill_2_ClickedUp();
        if (player.GetComponent<PlayerClass>().Class == "Mechanic") player.GetComponent<MechanicClass>().Skill_2_ClickedUp();
        if (player.GetComponent<PlayerClass>().Class == "Slayer") player.GetComponent<SlayerClass>().Skill_2_ClickedUp();
        if (player.GetComponent<PlayerClass>().Class == "Sniper") player.GetComponent<SniperClass>().Skill_2_ClickedUp();
        if (player.GetComponent<PlayerClass>().Class == "MagicSword") player.GetComponent<MagicSwordClass>().Skill_2_ClickedUp();

    }
    public void Skill_2_ClickedDown()
    {
        if (player.GetComponent<PlayerStats>().PlayerNowMp > Skill_2_Cost && AttackAble == true &&Skill_2.GetComponent<Skill_2_Script>().Skill_2_Able)
        {
            Skill_2.GetComponent<Skill_2_Script>().Skill_2_Able = false;
            player.GetComponent<PlayerStats>().PlayerNowMp -= Skill_2_Cost;
            AttackAble = false;
            Skill_2_CoolNow = Skill_2_Cool;
            
            if (player.GetComponent<PlayerClass>().Class == "Knight") player.GetComponent<KnightClass>().Skill_2_ClickedDown();
            if (player.GetComponent<PlayerClass>().Class == "MagicGun") player.GetComponent<MagicGunClass>().Skill_2_ClickedDown();
            if (player.GetComponent<PlayerClass>().Class == "Magician") player.GetComponent<MagicianClass>().Skill_2_ClickedDown();
            if (player.GetComponent<PlayerClass>().Class == "Mechanic") player.GetComponent<MechanicClass>().Skill_2_ClickedDown();
            if (player.GetComponent<PlayerClass>().Class == "Slayer") player.GetComponent<SlayerClass>().Skill_2_ClickedDown();
            if (player.GetComponent<PlayerClass>().Class == "Sniper") player.GetComponent<SniperClass>().Skill_2_ClickedDown();
            if (player.GetComponent<PlayerClass>().Class == "MagicSword") player.GetComponent<MagicSwordClass>().Skill_2_ClickedDown();
        }
    }
    public void Skill_3_ClickedUp()
    {
        if (player.GetComponent<PlayerClass>().Class == "Knight") player.GetComponent<KnightClass>().Skill_3_ClickedUp();
        if (player.GetComponent<PlayerClass>().Class == "MagicGun") player.GetComponent<MagicGunClass>().Skill_3_ClickedUp();
        if (player.GetComponent<PlayerClass>().Class == "Magician") player.GetComponent<MagicianClass>().Skill_3_ClickedUp();
        if (player.GetComponent<PlayerClass>().Class == "Mechanic") player.GetComponent<MechanicClass>().Skill_3_ClickedUp();
        if (player.GetComponent<PlayerClass>().Class == "Slayer") player.GetComponent<SlayerClass>().Skill_3_ClickedUp();
        if (player.GetComponent<PlayerClass>().Class == "Sniper") player.GetComponent<SniperClass>().Skill_3_ClickedUp();
        if (player.GetComponent<PlayerClass>().Class == "MagicSword") player.GetComponent<MagicSwordClass>().Skill_3_ClickedUp();

    }
    public void Skill_3_ClickedDown()
    {
        if (player.GetComponent<PlayerStats>().PlayerNowMp > Skill_3_Cost && AttackAble == true && Skill_3.GetComponent<Skill_3_Script>().Skill_3_Able)
        {
            Skill_3.GetComponent<Skill_3_Script>().Skill_3_Able = false;
            Skill_3_CoolNow = Skill_3_Cool;
            AttackAble = false;
            
            player.GetComponent<PlayerStats>().PlayerNowMp -= Skill_3_Cost;
            if (player.GetComponent<PlayerClass>().Class == "Knight") player.GetComponent<KnightClass>().Skill_3_ClickedDown();
            if (player.GetComponent<PlayerClass>().Class == "MagicGun") player.GetComponent<MagicGunClass>().Skill_3_ClickedDown();
            if (player.GetComponent<PlayerClass>().Class == "Magician") player.GetComponent<MagicianClass>().Skill_3_ClickedDown();
            if (player.GetComponent<PlayerClass>().Class == "Mechanic") player.GetComponent<MechanicClass>().Skill_3_ClickedDown();
            if (player.GetComponent<PlayerClass>().Class == "Slayer") player.GetComponent<SlayerClass>().Skill_3_ClickedDown();
            if (player.GetComponent<PlayerClass>().Class == "Sniper") player.GetComponent<SniperClass>().Skill_3_ClickedDown();
            if (player.GetComponent<PlayerClass>().Class == "MagicSword") player.GetComponent<MagicSwordClass>().Skill_3_ClickedDown();
        }
    }
    public void Skill_4_ClickedUp()
    {
        if (player.GetComponent<PlayerClass>().Class == "Knight") player.GetComponent<KnightClass>().Skill_4_ClickedUp();
        if (player.GetComponent<PlayerClass>().Class == "MagicGun") player.GetComponent<MagicGunClass>().Skill_4_ClickedUp();
        if (player.GetComponent<PlayerClass>().Class == "Magician") player.GetComponent<MagicianClass>().Skill_4_ClickedUp();
        if (player.GetComponent<PlayerClass>().Class == "Mechanic") player.GetComponent<MechanicClass>().Skill_4_ClickedUp();
        if (player.GetComponent<PlayerClass>().Class == "Slayer") player.GetComponent<SlayerClass>().Skill_4_ClickedUp();
        if (player.GetComponent<PlayerClass>().Class == "Sniper") player.GetComponent<SniperClass>().Skill_4_ClickedUp();
        if (player.GetComponent<PlayerClass>().Class == "MagicSword") player.GetComponent<MagicSwordClass>().Skill_4_ClickedUp();

    }
    public void Skill_4_ClickedDown()
    {
        if (player.GetComponent<PlayerStats>().PlayerNowMp > Skill_4_Cost && AttackAble == true && Skill_4.GetComponent<Skill_4_Script>().Skill_4_Able)
        {
            Skill_4.GetComponent<Skill_4_Script>().Skill_4_Able = false;
            Skill_4_CoolNow = Skill_4_Cool;
            AttackAble = false;
            
            player.GetComponent<PlayerStats>().PlayerNowMp -= Skill_4_Cost;
            if (player.GetComponent<PlayerClass>().Class == "Knight") player.GetComponent<KnightClass>().Skill_4_ClickedDown();
            if (player.GetComponent<PlayerClass>().Class == "MagicGun") player.GetComponent<MagicGunClass>().Skill_4_ClickedDown();
            if (player.GetComponent<PlayerClass>().Class == "Magician") player.GetComponent<MagicianClass>().Skill_4_ClickedDown();
            if (player.GetComponent<PlayerClass>().Class == "Mechanic") player.GetComponent<MechanicClass>().Skill_4_ClickedDown();
            if (player.GetComponent<PlayerClass>().Class == "Slayer") player.GetComponent<SlayerClass>().Skill_4_ClickedDown();
            if (player.GetComponent<PlayerClass>().Class == "Sniper") player.GetComponent<SniperClass>().Skill_4_ClickedDown();
            if (player.GetComponent<PlayerClass>().Class == "MagicSword") player.GetComponent<MagicSwordClass>().Skill_4_ClickedDown();
        }
    }
    

}
