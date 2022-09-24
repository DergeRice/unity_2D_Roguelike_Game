using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobMove : MonoBehaviour
{
    Rigidbody2D rigid;
    Transform player;
    public int MobAd;
    public float distanceX, distanceY,DownYpos,UpYpos,AggroTracking;
    private float PlayerDistanceX, PlayerDistanceY;
    public float speed, AttackDelay,MaxAttackDelay, AttackMove, AttackRangeX, AttackRangeY, MobMoveDelaytime, MobMoveStopDelay;
    public int MobMovingPosX, MobMovingPosY;
    public int MobMoveDelayMax;
    public bool PeaceMode, RePeaceMode, DistanceBool, MobStopBool, MobAttackBool, AggroMarkAble, KnockBackBool, MobAttackMove,MobAttackRangeOut;
    SpriteRenderer spriteRenderer;
    BoxCollider2D col2D;
    RaycastHit2D raycastHitDown, raycastHitUp, raycastHitLeft, raycastHitRight, raycastHitPlayerLeft, raycastHitPlayerRight;
    public GameObject Aggromark;

    Animator animator;

    // Start is called before the first frame update
    void Awake()
    {

        player = GameObject.Find("Main Char").transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        col2D = GetComponent<BoxCollider2D>();
        DistanceBool = false;
    }

    // Update is called once per frame
    private void Start()
    {
        PeaceMode = true;
        AggroTracking = 0;
        AttackMove = 1;
        MobMoveDelayMax = Random.Range(2, 5);
        MobMoveDelaytime = Random.Range(1, 4);
        MobMoveStopDelay = Random.Range(2, 5);
        MobStopBool = true;
        AggroMarkAble = true;
        Aggromark.SetActive(false);
        KnockBackBool = false;
        AttackDelay = 0;
        MobMoveDelaytime = 3f;
    }
    void Update()
    {

        PlayerDistanceX = player.position.x - transform.position.x;
        PlayerDistanceY = player.position.y - transform.position.y;

        spriteRenderer.sortingOrder = Mathf.RoundToInt(transform.position.y * 10) * -1;
       

        if (rigid.velocity != new Vector2(0, 0))
        {
            animator.SetBool("Walk", true);
            MobStopBool = false;
        }
        else animator.SetBool("Walk", false);
        //움직임이 있으면 걷기 애니마이션

        if (!MobAttackRangeOut) { DistanceBool = distanceX+ AggroTracking > Mathf.Abs(PlayerDistanceX) && distanceY+ AggroTracking-2 > Mathf.Abs(PlayerDistanceY); }
        
        //Debug.Log(DistanceBool);
        if (DistanceBool)
        {
            AggroTracking = 5;
            //AggroMark = true;
            //DistanceBool = f;
            PeaceMode = false;
            RePeaceMode = true;

            if (PlayerDistanceX < 0) spriteRenderer.flipX = false;
            else if (PlayerDistanceX > 0) spriteRenderer.flipX = true;
            //플레이어 거리 계산해서 따라갈때 플립

            if (AggroMarkAble == true)
            {
                AggroMarkAble = false;
                Aggromark.SetActive(true);

                Invoke("AggroEnd", 1f);
            }

        }
        else
        {
            MobMoveDelaytime -= Time.deltaTime;
            if (MobMoveDelaytime <= 0)
            {
                MobMoveDelaytime = 0;
            }


            if (MobMovingPosX < 0)
                spriteRenderer.flipX = false;
            else if (MobMovingPosX > 0)
                spriteRenderer.flipX = true;

            if (MobMoveDelaytime <= 0 && PeaceMode)
            {
                // MobMoveDelaytime -= MobMoveDelaytime;
                //몬스터 think 시간벌기
                Think();
                MobMoveDelaytime = 4f;
            }
        }
        //if (ollider2Ds == false){

        //}


        raycastHitDown = Physics2D.BoxCast(col2D.bounds.center, col2D.bounds.size, 0f, Vector2.down, 0.02f, LayerMask.GetMask("Wall"));
        raycastHitUp = Physics2D.BoxCast(col2D.bounds.center, col2D.bounds.size, 0f, Vector2.up, 0.02f, LayerMask.GetMask("Wall"));
        raycastHitLeft = Physics2D.BoxCast(col2D.bounds.center, col2D.bounds.size, 0f, Vector2.left, 0.02f, LayerMask.GetMask("Wall"));
        raycastHitRight = Physics2D.BoxCast(col2D.bounds.center, col2D.bounds.size, 0f, Vector2.right, 0.02f, LayerMask.GetMask("Wall"));
        raycastHitPlayerLeft = Physics2D.BoxCast(col2D.bounds.center, col2D.bounds.size, 0f, Vector2.left, AttackRangeX, LayerMask.GetMask("Player"));
        raycastHitPlayerRight = Physics2D.BoxCast(col2D.bounds.center, col2D.bounds.size, 0f, Vector2.right, AttackRangeX, LayerMask.GetMask("Player"));
        
        if (raycastHitDown.collider != null) MobMovingPosY = Mathf.Abs(MobMovingPosY);
        if (raycastHitUp.collider != null) MobMovingPosY = -Mathf.Abs(MobMovingPosY);
        if (raycastHitLeft.collider != null) MobMovingPosX = Mathf.Abs(MobMovingPosX);
        if (raycastHitRight.collider != null) MobMovingPosX = -Mathf.Abs(MobMovingPosX);

        if (raycastHitPlayerLeft.collider != null || raycastHitPlayerRight.collider != null)
        {
            if (Mathf.Abs(PlayerDistanceY)<AttackRangeY)
            {
                AttackDelay -= Time.deltaTime;
                if (AttackDelay <= 0) AttackDelay = 0;
                if (!MobAttackBool)
                {
                    if (AttackDelay <= 0) { Attack(); }
                }
                MobAttackMove = false;
            }
        }
        else
        {
            AttackMove = 1;
            MobAttackMove = true;
        }

    }

    private void FixedUpdate()
    {
        KnockBackBool = gameObject.GetComponent<MobDamageSystem>().KnockBackBool;
        //notice playerpos event
        if (!KnockBackBool)
        {
            if (DistanceBool)
            {
                if (!MobAttackBool)
                {
                    if (MobAttackMove)
                    {
                        rigid.velocity = new Vector2(PlayerDistanceX, PlayerDistanceY).normalized * speed * AttackMove;
                    }
                    else
                    {
                        rigid.velocity = new Vector2(0, 0);
                    }
                }
                RePeaceMode = true;
                PeaceMode = false;
            }
            else
            {
                rigid.velocity = new Vector2(MobMovingPosX, MobMovingPosY);
                ReturnToPeace();
            }

        }


        if (MobAttackRangeOut)
        {
            //rigid.velocity = new Vector2(PlayerDistanceX, PlayerDistanceY).normalized * speed * AttackMove;
            RePeaceMode = true;
            PeaceMode = false;
            DistanceBool = true;
            Invoke("MobAttackRangeOutEnd", 2f);
        }
    }


    private void Think()
    {
        if (DistanceBool == false)
        {
            ReturnToPeace();

            MobMovingPosX = Random.Range(-1, 2);
            MobMovingPosY = Random.Range(-1, 2);
            MobMoveStopDelay = Random.Range(2, 5);
            Invoke("Stop", MobMoveStopDelay);
        }
    }

    private void Stop()
    {
        if (DistanceBool == false)
        {
            ReturnToPeace();

            MobMovingPosX = 0;
            MobMovingPosY = 0;

            MobMoveDelayMax = Random.Range(2, 5);
            MobMoveDelaytime = MobMoveDelayMax;

            MobStopBool = true;
           // Debug.Log("stop");
        }
    }
    public void Attack()
    {

        AttackMove = 0;
        MobAttackBool = true;
        animator.SetBool("Attack", true);
        Invoke("AttackEnd", 0.1f);
        if (raycastHitPlayerLeft.collider != null || raycastHitPlayerRight.collider != null)
        {
            if (Mathf.Abs(PlayerDistanceY) < AttackRangeY)
            {
                player.GetComponent<PlayerDamaged>().PlayerDamged(MobAd,gameObject.transform);
            }
        }


    }
    private void AttackEnd()
    {
        animator.SetBool("Attack", false);
        MobAttackBool = false;
        AttackDelay = MaxAttackDelay;
    }
    private void AggroEnd()
    {
        Aggromark.SetActive(false);

    }
    private void ReturnToPeace()
    {
        if (RePeaceMode == true)
        {
            
            PeaceMode = true;
            AggroMarkAble = true;
            //StartCoroutine(ReturnToPeaceMove());
            RePeaceMode = false;

        }
    }
    IEnumerator ReturnToPeaceMove()
    {

        yield return new WaitForSeconds(3f);
       // MobPeaceMove = true;
        
    }
    
        

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(gameObject.transform.position,new Vector3((distanceX*2), (distanceY*2), 0));
        //col2D.bounds.size
        //Gizmos.DrawWireCube(gameObject.transform.position, new Vector3(col2D.bounds.size.x, col2D.bounds.size.y, 0));
    }

    public void MobAngry()
    {
        MobAttackRangeOut = true;
        AggroMarkAble = true;
    }
    public void MobAttackRangeOutEnd()
    {
        MobAttackRangeOut = false;
        AggroTracking = 0;
    }
}
