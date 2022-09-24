using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClass : MonoBehaviour
{
    
    public string Class;
    public GameObject OB;
    public bool KnightScript, MagicGunScript, MagicSwordScript, MagicianScript, MechanicScript, SlayerScript, SniperScript,PlayerCommonAni;
    public bool[] ClassScript;
    public string[] ClassString;
    Animator animator;


    // Start is called before the first frame update
    void Start()
    {

        KnightScript = OB.GetComponent<KnightClass>().enabled;
        MagicGunScript = OB.GetComponent<MagicGunClass>().enabled;
        MagicSwordScript = OB.GetComponent<MagicSwordClass>().enabled;
        MagicianScript = OB.GetComponent<MagicianClass>().enabled;
        MechanicScript = OB.GetComponent<MechanicClass>().enabled;
        SlayerScript = OB.GetComponent<SlayerClass>().enabled;
        SniperScript = OB.GetComponent<SniperClass>().enabled;
        ClassString = new string[] { "Knight", "MagicGun", "MagicSword", "Magician", "Mechanic", "Slayer", "Sniper" };
        ClassScript = new bool[] { KnightScript, MagicGunScript, MagicSwordScript, MagicianScript, MechanicScript, SlayerScript, SniperScript };

        animator = OB.GetComponent<Animator>();
        PlayerCommonAni = false;

        Class = "Knight";
    }

    void ClassSelect()
    {
        /* if (Class == "Knight") { thislmg.sprite = Knight01; }
         if (Class == "MagicGun") { thislmg.sprite = MagicGun01; }
         if (Class == "MagicSword") { thislmg.sprite = MagicSword01; }
         if (Class == "Magician") { thislmg.sprite = Magician01; }
         if (Class == "Mechanic") { thislmg.sprite = Mechanic01; }
         if (Class == "Slayer") { thislmg.sprite = Slayer01; }
         if (Class == "Sniper") { thislmg.sprite = Sniper01; }*/

        
    }

    // Update is called once per frame
    void Update()
    {
        KnightScript = ClassScript[0];
        MagicGunScript = ClassScript[1];
        MagicSwordScript = ClassScript[2];
        MagicianScript = ClassScript[3];
        MechanicScript = ClassScript[4];
        SlayerScript = ClassScript[5];
        SniperScript = ClassScript[6];

        OB.GetComponent<KnightClass>().enabled= KnightScript;
        OB.GetComponent<MagicGunClass>().enabled= MagicGunScript;
        OB.GetComponent<MagicSwordClass>().enabled=MagicSwordScript;
        OB.GetComponent<MagicianClass>().enabled= MagicianScript;
        OB.GetComponent<MechanicClass>().enabled= MechanicScript;
        OB.GetComponent<SlayerClass>().enabled= SlayerScript;
        OB.GetComponent<SniperClass>().enabled= SniperScript;

        if (Input.GetKeyDown(KeyCode.Alpha1)) Class = "Knight";
        if (Input.GetKeyDown(KeyCode.Alpha2)) Class = "MagicGun";
        if (Input.GetKeyDown(KeyCode.Alpha3)) Class = "MagicSword";
        if (Input.GetKeyDown(KeyCode.Alpha4)) Class = "Magician";
        if (Input.GetKeyDown(KeyCode.Alpha5)) Class = "Mechanic";
        if (Input.GetKeyDown(KeyCode.Alpha6)) Class = "Slayer";
        if (Input.GetKeyDown(KeyCode.Alpha7)) Class = "Sniper";

        Identifier();


    }
    public void Identifier()
    {
        if (!PlayerCommonAni)
        {
            for (int i = 0; i < 7; i++)
            {
                if (Class == ClassString[i])
                {
                    ClassScript[i] = true;
                    animator.SetLayerWeight(i + 1, 1f);
                    for (int j = 0; j < i; j++)
                    {
                        ClassScript[j] = false;
                        animator.SetLayerWeight(j + 1, 0);

                    }
                    for (int j = i + 1; j < 7; j++)
                    {
                        ClassScript[j] = false;
                        animator.SetLayerWeight(j + 1, 0);
                    }
                }

            }
        }
        else
        {
            for (int i = 1; i < 7; i++)
            {
                animator.SetLayerWeight(i, 0);
            }
            animator.SetLayerWeight(0, 1);

        }
    }
    public void Roll()
    {
        
    }

}
