using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightClass : MonoBehaviour
{
    public bool AttackBtnOn, AttackAble, GizmosOn,PlayerSkilling, Skill3Move;
    public int BasicAttackNum;
    public float ResetTime, AttackCoolTime, AttackCoolTimeLim ,um,damage= 0;
    public float Skill_1_Casting, Skill_2_Casting, Skill_3_Casting, Skill_4_Casting,
                     Skill_1_SetupTime, Skill_2_SetupTime, Skill_3_SetupTime, Skill_4_SetupTime;
    public float SkillAniReturnTime, EffectDesTime, VibrateRate, VibrateTime,PullingSpeed, BlockTime;
    Animator animator;
    public Transform pos;
    public Vector2 boxSize,EffectPos;
    public GameObject playerStats,Monster,AttackedEffect_BasicATK1, AttackedEffect_BasicATK2, AttackedEffect_BasicATK3, AttackedEffect_BasicATK4;
    public GameObject Skill4Effect, CameraShake;
    public float skill_3_Damge_Delay = 0;



    // Start is called before the first frame update
    void Start()
    {
        BasicAttackNum = 1;
        
        ResetTime = 0;
        
        AttackAble = true;
        CameraShake = playerStats.GetComponent<PlayerMove>().camera_Main;
        PlayerSkilling = false;
        Skill3Move = false;
        damage = playerStats.GetComponent<PlayerStats>().PlayerAD;
    }


    private void Awake()
    {
        animator = GetComponent<Animator>();
       
    }
    private void OnEnable()
    {
        GizmosOn = true;
        AttackAble = true;
    }
    // Update is called once per frame

    private void OnDisable()
    {
        GizmosOn = false;

    }

    void Update()
    {
        damage = playerStats.GetComponent<PlayerStats>().PlayerAD;
        animator.SetBool("ReturnIdle", true);
        ResetTime = ResetTime - Time.deltaTime;
        AttackCoolTime = AttackCoolTime - Time.deltaTime;
        if (AttackCoolTime <= 0) AttackCoolTime = 0;
        if (ResetTime <= 0) ResetTime = 0;

        if (AttackBtnOn == true)
        {


        }
        if (AttackBtnOn == false)
        {
            animator.SetBool("KnightBasicAttack", false);
        }
        if (ResetTime <= 0)
        {

            StartCoroutine(AttackEndRou());

        }

        if (playerStats.GetComponent<PlayerMove>().PlayerLookLeft == false) { pos.position = new Vector3(playerStats.transform.position.x + 1, playerStats.transform.position.y, 0); }
        if (playerStats.GetComponent<PlayerMove>().PlayerLookLeft) {pos.position = new Vector3(playerStats.transform.position.x - 1, playerStats.transform.position.y, 0);}
        //플레이어 좌우 추적
        if (PlayerSkilling)
        {
            gameObject.GetComponent<PlayerMove>().PlayerSkilling = true;
            gameObject.GetComponent<PlayerDamaged>().NotKnockBackBool = true;

        }
        else
        {
            gameObject.GetComponent<PlayerMove>().PlayerSkilling = false;
            gameObject.GetComponent<PlayerDamaged>().NotKnockBackBool = false;
        }

        try 
        {
            
        }
        catch
        {

        }
    }
    private void FixedUpdate()
    {

        if ((animator.GetCurrentAnimatorStateInfo(1).IsName("BasicAttack1") ||
            animator.GetCurrentAnimatorStateInfo(1).IsName("BasicAttack2") ||
            animator.GetCurrentAnimatorStateInfo(1).IsName("BasicAttack3") ||
            animator.GetCurrentAnimatorStateInfo(1).IsName("BasicAttack4") ||
            animator.GetCurrentAnimatorStateInfo(1).IsName("BasicAttack")) &&
            animator.GetCurrentAnimatorStateInfo(1).normalizedTime >= 0.9f)
        {
            AttackAble = true;
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Running") &&
            animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.4f)
        {
            AttackAble = true;
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") &&
            animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.7f)
        {
            AttackAble = true;
        }
    }
    public void AttackBtnClickedDown()
    {

        if (AttackAble == true)
        {
            if (AttackCoolTime <= 0)
            {
                BasicAttackNum++;
                AttackCoolTime = AttackCoolTimeLim;
                AttackAble = false;
                AttackBtnOn = true;
                StartCoroutine(AttackRou());

                GiveMonsterDamaged(1f,boxSize,0,0.3f);
            }
        }




    }
    IEnumerator EffectPosCal(Collider2D collider)
    {
        if (BasicAttackNum == 1) GameObject.Instantiate(AttackedEffect_BasicATK1, EffectPos, Quaternion.identity).transform.parent = Monster.transform;
        if (BasicAttackNum == 2) GameObject.Instantiate(AttackedEffect_BasicATK2, EffectPos, Quaternion.identity).transform.parent = Monster.transform;
        if (BasicAttackNum == 3) GameObject.Instantiate(AttackedEffect_BasicATK3, EffectPos, Quaternion.identity).transform.parent = Monster.transform;
        if (BasicAttackNum == 4) GameObject.Instantiate(AttackedEffect_BasicATK4, EffectPos, Quaternion.identity).transform.parent = Monster.transform;
        AttackedEffect_BasicATK1.SetActive(true);
        AttackedEffect_BasicATK2.SetActive(true);
        AttackedEffect_BasicATK3.SetActive(true);
        AttackedEffect_BasicATK4.SetActive(true);

        damage = playerStats.GetComponent<PlayerStats>().PlayerAD;
       


        yield return new WaitForSeconds(0.1f);
    }


    public void AttackBtnClickedUp()
    {
        ResetTime = 2f;


    }

    IEnumerator AttackRou()
    {

        if (BasicAttackNum > 4) BasicAttackNum = 1;

        animator.SetBool("KnightBasicAttack", true);
        animator.SetInteger("KnightBasicAttackNum", BasicAttackNum);
        yield return new WaitForSeconds(0.1f);
        
        
        animator.SetBool("KnightBasicAttack", false);
        playerStats.GetComponent<PlayerMove>().speed = 0;
        yield return new WaitForSeconds(0.3f);
        playerStats.GetComponent<PlayerMove>().speed = playerStats.GetComponent<PlayerMove>().PlayerBasicSpeed;

    }

    IEnumerator AttackEndRou()
    {
        ResetTime = 0;
        BasicAttackNum = 1;
        yield return new WaitForSeconds(0.1f);


    }
    public void Attackfocus()
    {
    }


    private void OnDrawGizmos()
    {
        if (GizmosOn)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(pos.position, boxSize);
        }
    }
    public void Skill_1_ClickedDown()
    {
        if (!PlayerSkilling)
        {
            PlayerSkilling = true;
            animator.SetInteger("Skill_Num_Start", 1);
            StartCoroutine(Skill1EffectActive());
            playerStats.GetComponent<PlayerMove>().PlayerMovAble = false;
        }
    }
    IEnumerator Skill1EffectActive()
    {
        //GameObject um;
        yield return new WaitForSeconds(SkillAniReturnTime);
        animator.SetInteger("Skill_Num_Start", 0);
        yield return new WaitForSeconds(Skill_1_SetupTime);
        boxSize = new Vector2(1.5f, 1.44f);
        GiveMonsterDamaged(2.5f,boxSize,2,2);

        // um = GameObject.Instantiate(Skill4Effect, playerStats.transform.position, Quaternion.identity);
        CameraShake.GetComponent<MoveCamera>().VibrateForTime(20, 32);
        yield return new WaitForSeconds(Skill_1_Casting);

        
        

        //Destroy(um);
        playerStats.GetComponent<PlayerMove>().PlayerMovAble = true;
        PlayerSkilling = false;
        playerStats.GetComponent<PlayerAttack>().AttackAble = true;
    }


    public void Skill_1_ClickedUp()
    {

    }

    public void Skill_2_ClickedDown()
    {
        if (!PlayerSkilling)
        {
            PlayerSkilling = true;
            animator.SetInteger("Skill_Num_Start", 2);
            //animator.SetBool("Running", false);
            StartCoroutine(Skill2EffectActive());
            playerStats.GetComponent<PlayerMove>().PlayerMovAble = false;
            playerStats.GetComponent<PlayerDamaged>().Blocked = true;
        }
    }
    IEnumerator Skill2EffectActive()
    {
        yield return new WaitForSeconds(SkillAniReturnTime);
        animator.SetInteger("Skill_Num_Start", 0);
        
        yield return new WaitForSeconds(BlockTime);
        playerStats.GetComponent<PlayerDamaged>().Blocked = false;

        boxSize = new Vector2(1.5f, 1.44f);
        GiveMonsterDamaged(2f,boxSize,2,2);
        yield return new WaitForSeconds(Skill_2_Casting);
        
        playerStats.GetComponent<PlayerMove>().PlayerMovAble = true;
        PlayerSkilling = false;
        playerStats.GetComponent<PlayerAttack>().AttackAble = true;


    }
    public void Skill_2_ClickedUp()
    {

    }

    public void Skill_3_ClickedDown()
    {
        if (!PlayerSkilling)
        {
            PlayerSkilling = true;
            animator.SetInteger("Skill_Num_Start", 3);
            StartCoroutine(Skill3EffectActive());
            playerStats.GetComponent<PlayerMove>().PlayerMovAble = false;
        }
    }
    IEnumerator Skill3EffectActive()
    {

        yield return new WaitForSeconds(SkillAniReturnTime);
        animator.SetInteger("Skill_Num_Start", 0);
        yield return new WaitForSeconds(Skill_3_SetupTime);
        Skill3Move = true;
        boxSize = new Vector2(1.5f, 1.44f);

        GiveMonsterDamaged(0.26f,boxSize,3,2);
        yield return new WaitForSeconds(0.1f);
        GiveMonsterDamaged(0.26f, boxSize, 3,2);
        yield return new WaitForSeconds(0.1f);
        GiveMonsterDamaged(0.26f, boxSize, 3, 2);
        yield return new WaitForSeconds(0.1f);
        GiveMonsterDamaged(0.26f, boxSize, 3, 2);

        yield return new WaitForSeconds(Skill_3_Casting-0.6f);
        GiveMonsterDamaged(0.26f, boxSize, 3, 13);
        playerStats.GetComponent<PlayerMove>().PlayerMovAble = true;
        PlayerSkilling = false;
        playerStats.GetComponent<PlayerAttack>().AttackAble = true;
        Skill3Move = false;
    }

    public void Skill_3_ClickedUp()
    {

    }

    public void Skill_4_ClickedDown()
    {
        if (!PlayerSkilling)
        {
            PlayerSkilling = true;
            animator.SetInteger("Skill_Num_Start", 4);
            StartCoroutine(Skill4EffectActive());
            playerStats.GetComponent<PlayerMove>().PlayerMovAble = false;
        }
    }

    IEnumerator Skill4EffectActive()
    {
        GameObject um;
        yield return new WaitForSeconds(SkillAniReturnTime);
        animator.SetInteger("Skill_Num_Start", 0);
        yield return new WaitForSeconds(Skill_4_SetupTime);
        um =GameObject.Instantiate(Skill4Effect, playerStats.transform.position, Quaternion.identity);
        CameraShake.GetComponent<MoveCamera>().VibrateForTime(VibrateTime, VibrateRate+30);
        boxSize = new Vector2(7, 4);
        GiveMonsterDamaged(4.5f, boxSize,4,5);
        yield return new WaitForSeconds(Skill_4_Casting);
        Destroy(um);
        playerStats.GetComponent<PlayerMove>().PlayerMovAble = true;
        PlayerSkilling = false;
        playerStats.GetComponent<PlayerAttack>().AttackAble = true;
    }

    public void Skill_4_ClickedUp()
    {

    }

    public void GiveMonsterDamaged(float value,Vector2 Range,int SkillNum,float KnockBackAmount)//평타는 0번 스킬
    {
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, Range, 0);
        foreach (Collider2D collider in collider2Ds)
        {
            bool DamagedBool = true;

            if (DamagedBool == true && collider.tag == "Monter" && (collider.isTrigger))
            {
                Monster = collider.gameObject;
                EffectPos = new Vector2(Monster.transform.position.x, Monster.transform.position.y);

                if (animator.GetInteger("Skill_Num_Start") == 3)
                {
                    GiveMonsterDamaged(0.26f, boxSize, 3,5);
                    skill_3_Damge_Delay += Time.deltaTime;

                    if (skill_3_Damge_Delay > 0.1)
                    {

                        skill_3_Damge_Delay = 0;
                        Monster.GetComponent<MobDamageSystem>().KnockBackDisCal(KnockBackAmount);
                        Monster.GetComponent<MobDamageSystem>().TakeDamaged(Mathf.RoundToInt(damage * value));
                    }

                }
                else
                {
                    Monster.GetComponent<MobDamageSystem>().KnockBackDisCal(KnockBackAmount);
                    Monster.GetComponent<MobDamageSystem>().TakeDamaged(Mathf.RoundToInt(damage * value));
                }

                    StartCoroutine(EffectPosCal(collider));
               
                CameraShake.GetComponent<MoveCamera>().VibrateForTime(2, VibrateRate);


            }

        }
    }
}
