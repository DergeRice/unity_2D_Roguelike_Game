using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MobDamageSystem : MonoBehaviour
{
    public float MobMaxHp, MobNowHp;
    Rigidbody2D rigid;
    Transform player;

    public Transform DamagePos;
    public float distanceX, distanceY, MaxSpeed, time, KnockBack, KnockBackSpeed, KnockBackAmount, KnockBackPos;
    private float PlayerDistanceX, PlayerDistanceY,speed;
    public float KnockBackCool, VibrateRate, HpPos, HpBarFadeTime;
    
    public bool DistanceBool, MobAttackBool, KnockBackBool, KnockBackLeft, MobHpBarEnable;
    SpriteRenderer spriteRenderer;

    public GameObject CameraShake, DamageHUD, MobHpBar, canvas,um;
    Animator animator;
    RectTransform hpbar;

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.Find("Main Char").transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    // Update is called once per frame
    private void Start()
    {

        animator = GetComponent<Animator>();
        HpBarFadeTime = 4f;
        MobHpBarEnable = false;
        MobNowHp = MobMaxHp;
        CameraShake = player.GetComponent<PlayerMove>().camera_Main;
        canvas = player.GetComponent<PlayerMove>().MobCanvas;
       
    }
    void Update()
    {
        
        if (MobHpBarEnable)
        {

            hpbar.position = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + HpPos, 0));

            if (KnockBackBool == true)
            {
                hpbar.GetComponent<CanvasGroup>().alpha += 0.1f;
                HpBarFadeTime = 4f;
            }
            HpBarFadeTime -= Time.deltaTime;
            if (HpBarFadeTime <= 0)
            {
                HpBarFadeTime = 0;
                hpbar.GetComponent<CanvasGroup>().alpha -= 0.1f;
            }
            hpbar.GetComponent<MobHpBar>().MobMaxHp = MobMaxHp;
            hpbar.GetComponent<MobHpBar>().MobNowHp = MobNowHp;

        }

        spriteRenderer.sortingOrder = Mathf.RoundToInt(transform.position.y * 10) * -1;
        time += Time.deltaTime;


        PlayerDistanceX = player.position.x - transform.position.x;
        PlayerDistanceY = player.position.y - transform.position.y;

        

        DistanceBool = distanceX > Mathf.Abs(PlayerDistanceX) && distanceY > Mathf.Abs(PlayerDistanceY);


    }

    private void FixedUpdate()
    {
        if (KnockBackBool == true)
        {
            Physics2D.IgnoreLayerCollision(7, 7);
            if ( Mathf.Abs(PlayerDistanceX) < Mathf.Abs(KnockBackPos) + KnockBackAmount)
            {

                rigid.velocity = new Vector2(-KnockBackPos, 0).normalized * KnockBackSpeed * ((Mathf.Abs(KnockBackPos) + KnockBackAmount)-(Mathf.Abs(PlayerDistanceX)));
                Physics2D.IgnoreLayerCollision(7, 7);

            }

        }

    }

    public void KnockBackDisCal(float amount)
    {
        KnockBackAmount = amount;
        KnockBackLeft = player.GetComponent<PlayerMove>().PlayerLookLeft;
        KnockBackPos = PlayerDistanceX; //플레이어가 오른쪽이면 음수 호출할때의 위치를 기억
    }


    public void TakeDamaged(int Damage)
    {
        if (MobNowHp == MobMaxHp)
        {

            um = Instantiate(canvas);
            hpbar = Instantiate(MobHpBar, new Vector3(0, 0, 0), Quaternion.identity, um.transform).GetComponent<RectTransform>();
            Transform sival = hpbar.GetComponent<RectTransform>().GetChild(1).GetChild(0);
            sival.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            sival.GetComponent<RectTransform>().localPosition = new Vector2(0, 0);
        }
        gameObject.GetComponent<MobMove>().AttackDelay = gameObject.GetComponent<MobMove>().MaxAttackDelay;
   
        MobHpBarEnable = true;
        int RealDamage = (int)Random.Range(Damage * 0.9f, Damage * 1.1f);

        MobNowHp -= RealDamage;
        StartCoroutine(Damaged());

        
        StartCoroutine(KnockBacking());
        rigid.position = Vector3.Lerp(new Vector3(rigid.position.x - KnockBack, rigid.position.y, 0), rigid.position, 0.5f);
        GameObject DamageText = Instantiate(DamageHUD);

        DamageText.transform.position = DamagePos.position;
        DamageText.GetComponent<DamageText>().damage = RealDamage;



        // Debug.Log(sival);
        // sival.GetComponent<RectTransform>()=Null;

        //hpbar.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);


    }

    IEnumerator Damaged()
    {
        if (MobNowHp > 0) animator.SetBool("Damaged", true);


        if (MobNowHp <= 0) animator.SetBool("Death", true);

        Time.timeScale = 0.5f;
        yield return new WaitForSeconds(0.2f);
        Time.timeScale = 1f;


    }

    IEnumerator KnockBacking()
    {
        KnockBackBool = true;

        yield return new WaitForSeconds(0.7f);

        if (MobNowHp > 0) animator.SetBool("Damaged", false);
        if (MobNowHp <= 0)
        {
            yield return new WaitForSeconds(0.3f);
            MobHpBarEnable = false;
            hpbar.GetComponent<MobHpBar>().DestroyThis();
            Destroy(gameObject);

        }
        Physics2D.IgnoreLayerCollision(7, 7, false);
        KnockBackBool = false;
        speed = MaxSpeed;

    }


}
