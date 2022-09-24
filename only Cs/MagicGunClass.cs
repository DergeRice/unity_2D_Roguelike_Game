using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicGunClass : MonoBehaviour
{
    public bool AttackBtnOn, AttackAble, GunReady, GizmosOn,Detected;
    public float ResetTime, AttackCoolTime, AttackCoolTimeLim,um = 0;
    int mobcount;
    public float GunDownTime, GunDownTimeLim,GunX=0;
    Animator animator;
    public Transform pos;
    public Vector2 boxSize,LeftBoxSize, RightBoxSize, BulletPos;
    public GameObject playerStats,BasicBullet,RealBullet;
    public Collider2D NearestMob, MobCollider;
    Collider2D Max, common;
    //Collider2D collider;


    // Start is called before the first frame update
    void Start()
    { 
        ResetTime = 0;
        NearestMob = null; 
        AttackAble = true;
        GunDownTime = GunDownTimeLim;
      
    }

    private void OnEnable()
    {
        GizmosOn = true;
        AttackAble = true;
    }
    private void OnDisable()
    {
        GizmosOn = false;

    }
    private void Awake()
    {
        animator = GetComponent<Animator>();
        

    }

    // Update is called once per frame
    void Update()
    {
        if (playerStats.GetComponent<PlayerMove>().PlayerLookLeft)
        {
            GunX = 1;
        }
        else GunX = -1;
        animator.SetBool("ReturnIdle", true);
        AttackCoolTime = AttackCoolTime - Time.deltaTime;

        if (AttackCoolTime <= 0) AttackCoolTime = 0;
        if (GunReady)
        {
            GunDownTime = GunDownTime - Time.deltaTime;
            if (GunDownTime <= 0) GunDownTime = 0;
        }
        if (GunDownTime <= 0)
        {
            StartCoroutine(GunDown());


        }
        if (ResetTime <= 0) ResetTime = 0;
        //if (AttackBtnOn == true)
        {
            //  animator.SetBool("MagicGunBasicAttack_GunReady", true);

        }
        if (AttackBtnOn == false)
        {
            animator.SetBool("MagicGunAttack", false);
        }
        if (ResetTime <= 0)
        {

            StartCoroutine(AttackEndRou());

        }
        if (playerStats.GetComponent<PlayerMove>().PlayerLookLeft == false) 
        { 
            pos.position = new Vector3(playerStats.transform.position.x + boxSize.x/2, playerStats.transform.position.y, 0);
            BulletPos = new Vector2(pos.position.x- 3.5f, playerStats.transform.position.y);
        }
        if (playerStats.GetComponent<PlayerMove>().PlayerLookLeft) 
        {
            pos.position = new Vector3(playerStats.transform.position.x - boxSize.x / 2, playerStats.transform.position.y, 0);
            BulletPos = new Vector2(pos.position.x +3.5f, playerStats.transform.position.y);
        }
        //플레이어 히트 범위의 좌우 추적

        if (animator.GetCurrentAnimatorStateInfo(2).IsName("MagicGun_BasicATK_GunDown") ||
            animator.GetCurrentAnimatorStateInfo(2).IsName("MagicGun_BasicATK_GunShoot") ||
            animator.GetCurrentAnimatorStateInfo(2).IsName("MagicGun_BasicATK_GunDown") )
        {
            playerStats.GetComponent<PlayerMove>().speed = 0;
        }
        else playerStats.GetComponent<PlayerMove>().speed = playerStats.GetComponent<PlayerMove>().PlayerBasicSpeed; 

    }
    private void FixedUpdate()
    { 
        if ((animator.GetCurrentAnimatorStateInfo(2).IsName("MagicGun_BasicATK")) &&
            animator.GetCurrentAnimatorStateInfo(2).normalizedTime >= 0.9f)
        {
            AttackAble = true;
        }
        if (animator.GetCurrentAnimatorStateInfo(2).IsName("Running") &&
            animator.GetCurrentAnimatorStateInfo(2).normalizedTime >= 0.4f)
        {
            AttackAble = true;
        }
        if (animator.GetCurrentAnimatorStateInfo(2).IsName("Idle") &&
            animator.GetCurrentAnimatorStateInfo(2).normalizedTime >= 0.8f)
        {
            AttackAble = true;
        }
        //Debug.Log(GunX);


    }
    public void AttackBtnClickedDown()
    {
        
        if (!animator.GetBool("MagicGunBasicAttack_GunReady"))
        {
            GunDownTime = GunDownTimeLim;
            GunReady = true;
            animator.SetBool("MagicGunBasicAttack_GunReady", true);
            animator.SetBool("MagicGunBasicAttack_Shoot", true);
            AttackCoolTime = AttackCoolTimeLim;
            playerStats.GetComponent<PlayerMove>().speed = 0;
        } 
        else
        {
            
            GunDownTime = GunDownTimeLim;
            animator.Play("MagicGun_BasicATK_GunShoot", 2,0f);
        }

        if (AttackAble == true)
        {
            {
                AttackAble = false;
                AttackCoolTime = AttackCoolTimeLim;
                AttackBtnOn = true;


                StartCoroutine(AttackRou());
                Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);

                Max = collider2Ds[0];

                foreach (Collider2D collider in collider2Ds)
                {
                    if (collider.tag == "Monter" && (collider.isTrigger))
                    { 
                        MobCollider = collider;

                        if (Mathf.Abs(gameObject.transform.position.x - Max.transform.position.x) >
                       Mathf.Abs(gameObject.transform.position.x - collider2Ds[mobcount].transform.position.x))
                        {
                            Max = collider2Ds[mobcount];
                            MobCollider = Max;
                        }
                        mobcount++;

                    } 
                }
            }

        }
        if (mobcount > 0)
        {
            NearestMob = MobCollider;
        }
        else NearestMob = null;
        mobcount =0;

        
        GameObject RealBullet = Instantiate(BasicBullet);

        RealBullet.transform.position = BulletPos;
      
        AttackAble = true;
        

    }
    public void AttackBtnClickedUp()
    {
        ResetTime = 1f;
    }

    IEnumerator AttackRou()
    {
        animator.SetBool("MagicGunAttack", true);
        yield return new WaitForSeconds(0.1f);
        animator.SetBool("MagicGunAttack", false);  
    }

    IEnumerator AttackEndRou()
    {
        ResetTime = 0;

        yield return new WaitForSeconds(0.1f);


    }
    IEnumerator GunDown()
    {
        GunReady = false;
        animator.SetBool("MagicGunBasicAttack_GunDown", true);
        animator.SetBool("MagicGunBasicAttack_Shoot", false);
        animator.SetBool("MagicGunBasicAttack_GunReady", false);
        yield return new WaitForSeconds(0.1f);
        animator.SetBool("MagicGunBasicAttack_GunDown", false);
        //playerStats.GetComponent<PlayerMove>().speed = playerStats.GetComponent<PlayerMove>().PlayerBasicSpeed;


    }

    private void OnDrawGizmos()
    {
        if (GizmosOn) { 
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(pos.position, boxSize);
        
 
        }
    }
    public void Skill_1_ClickedDown()
    {

    }
    public void Skill_1_ClickedUp()
    {

    }

    public void Skill_2_ClickedDown()
    {

    }
    public void Skill_2_ClickedUp()
    {

    }

    public void Skill_3_ClickedDown()
    {

    }
    public void Skill_3_ClickedUp()
    {

    }

    public void Skill_4_ClickedDown()
    {

    }
    public void Skill_4_ClickedUp()
    {

    }

}
