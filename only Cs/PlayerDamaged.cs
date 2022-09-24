using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerDamaged : MonoBehaviour
{
    public float PlayerMaxHp, PlayerNowHp, PlayerAutoHP;
    public float PlayerDodgePer, DamageDelay, KnockBackAmount,DeathTimeSlow;
    Animator animator;
    public bool Dodged,CanBeDamaged, Blocked,NotKnockBackBool;
    public GameObject DodgedHUD;
    public Transform DodgedPos;
    Rigidbody2D rigid;
    SpriteRenderer sr;
    Color halfAlpha = new Color(1, 1, 1, 0.5f);
    Color fullAlpha = new Color(1, 1, 1, 1);
    Transform KnockBackMob;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        PlayerMaxHp = gameObject.GetComponent<PlayerStats>().PlayerMaxHp;
        PlayerNowHp = gameObject.GetComponent<PlayerStats>().PlayerNowHp;
        PlayerDodgePer = gameObject.GetComponent<PlayerStats>().PlayerDodgePer;
        sr = GetComponent<SpriteRenderer>();
        CanBeDamaged = true;
        Dodged = false;
        Blocked = false;

    }

    // Update is called once per frame
    void Update()
    {
        PlayerMaxHp = gameObject.GetComponent<PlayerStats>().PlayerMaxHp;
        PlayerNowHp = gameObject.GetComponent<PlayerStats>().PlayerNowHp;
        PlayerDodgePer = gameObject.GetComponent<PlayerStats>().PlayerDodgePer;


        if ((animator.GetCurrentAnimatorStateInfo(0).IsName("Death")))
        {
            StartCoroutine(TimeStop());
        }

    }
    IEnumerator TimeStop()
    {
        yield return new WaitForSeconds(10f);
        Time.timeScale = 0;
    }
    public void PlayerDamged(int damage,Transform MobPos)
    {
        
        if (CanBeDamaged)
        {
            Dodged = PercentCal.GetPercent.GetThisChanceResult_Percentage(gameObject.GetComponent<PlayerStats>().PlayerDodgePer);
            if (!gameObject.GetComponent<PlayerMove>().IsDashing)//대시중 이거나 회피 한다면
            {
                
                if (Dodged|| Blocked)
                {
                    if (Dodged)
                    {
                        Dodged = false;
                        CanBeDamaged = false;
                        GameObject DodgedText = Instantiate(DodgedHUD);
                        DodgedText.transform.position = DodgedPos.position;
                        DodgedText.GetComponent<DodgeText>().status = "Dodged";
                        StartCoroutine(HurtRoutine());
                    }
                    if (Blocked)
                    {
                        
                        CanBeDamaged = false;
                        GameObject DodgedText = Instantiate(DodgedHUD);
                        DodgedText.transform.position = DodgedPos.position;
                        DodgedText.GetComponent<DodgeText>().status = "Blocked";
                        StartCoroutine(HurtRoutine());

                    }
                }
                else
                {
                    CanBeDamaged = false;
                    gameObject.GetComponent<PlayerStats>().PlayerNowHp -= damage;
                    if (gameObject.GetComponent<PlayerStats>().PlayerNowHp <= 0)//death
                    {
                        GetComponent<PlayerClass>().PlayerCommonAni = true;
                        animator.SetBool("Death", true);

                        CanBeDamaged = false;
                    }
                    else//damaged
                    {
                        GetComponent<PlayerClass>().PlayerCommonAni = true;
                        if (!NotKnockBackBool) {
                        animator.SetBool("Damaged", true);
                        }
                        StartCoroutine(Knockback(MobPos));
                        StartCoroutine(HurtRoutine());
                        StartCoroutine(alphablink());

                    }
                }
                
            }
            
        }
    }
    IEnumerator Knockback(Transform MobPos)
    {
        KnockBackMob = MobPos;
        GetComponent<PlayerMove>().PlayerKnockBacking = true;
        yield return new WaitForSeconds(0.2f);
        Dodged = false;
        GetComponent<PlayerMove>().PlayerKnockBacking = false;
        GetComponent<PlayerMove>().PlayerMovAble = true;
        GetComponent<PlayerClass>().PlayerCommonAni = false;
        animator.SetBool("Damaged", false);

    }
    IEnumerator alphablink()
    {
        while (!CanBeDamaged) 
        {
            
                yield return new WaitForSeconds(0.1f);
                sr.color = halfAlpha;
                yield return new WaitForSeconds(0.1f);
                sr.color = fullAlpha;
            
        }
        
    }
    IEnumerator HurtRoutine()
    {
        Physics2D.IgnoreLayerCollision(6, 7);
        yield return new WaitForSeconds(DamageDelay);
        Dodged = false;
        CanBeDamaged = true;
        
        Physics2D.IgnoreLayerCollision(7, 7, false);
    }
    public void KnockBack()
    {
        if (NotKnockBackBool) { 
        
        }else rigid.velocity = new Vector2(rigid.transform.position.x - KnockBackMob.position.x, 0).normalized * KnockBackAmount;

    }
}
