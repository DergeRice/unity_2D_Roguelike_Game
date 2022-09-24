using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private Transform charBody;
    private VirtualJoyStick ControlJoystickLever;
    public float speed, PlayerBasicSpeed;
    private bool joystickOn;
    public bool DashBtnOn;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;

    public float DashTimeLimit, DashCount, DashCountLimit, DashSpeed, DashTime, DashCoolTimeNow, DashCoolTimeMax, DashRecovCoolNow, DashRecovCoolMax,DashBump;
    
    public bool DashAble, CallDashRecovAble, PlayerMovAble, FlipBool, PlayerLookLeft, PlayerLookRight,PlayerKnockBacking, PlayerSkilling;

    public Vector2 moveInput, DashVector;
    public bool IsDashing = false;
    Animator animator;
    public GameObject player, camera_Main, MobCanvas;

    // Start is called before the first frame update

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        animator = GetComponent<Animator>();
        CallDashRecovAble = true;
        DashRecovCoolNow = DashRecovCoolMax;
        DashCount = DashCountLimit;
        FlipBool = true;
        PlayerMovAble = true;
        DashCoolTimeNow = 0;
        PlayerKnockBacking = false;

    }
    void Start()
    {
        DashBtnOn = GameObject.Find("dashBtn").GetComponent<DashBtn>().DashBtnOn;
    }
    public void Move(Vector2 inputDirecion)
    {
        moveInput = inputDirecion;
    }
    // Update is called once per frame
    void Update()
    {
        if (PlayerSkilling)
        {
            FlipBool = false;
            animator.SetBool("Running", false);
            animator.SetBool("Damaged", false);
        }
        else FlipBool = true;

        spriteRenderer.sortingOrder = Mathf.RoundToInt(transform.position.y * 10) * -1;
        if (FlipBool == true)
        {
            if (moveInput.x < 0)
            {
                spriteRenderer.flipX = false;
                PlayerLookLeft = true;
                PlayerLookRight = false;
            }
            else if (moveInput.x > 0)
            {
                spriteRenderer.flipX = true;
                PlayerLookRight = true;
                PlayerLookLeft = false;
            }
        }
        if (Mathf.Abs(moveInput.x) > 0 || Mathf.Abs(moveInput.y) > 0)
            animator.SetBool("Running", true);
        else
            animator.SetBool("Running", false);

        if (Input.GetKeyDown(KeyCode.Z)) DashBtnClickedDown();
        if (Input.GetKeyUp(KeyCode.Z)) DashBtnClickedUp();


        if (DashCoolTimeNow > 0) DashCoolTimeNow -= Time.deltaTime;

        if (DashRecovCoolNow > 0 && DashCount!=DashCountLimit) DashRecovCoolNow -= Time.deltaTime;
        if (DashRecovCoolNow <= 0 ) 
        {
            if(CallDashRecovAble == true)
            CallDashRecovAble = false;
            DashRecover();
        }
    }



    public void FixedUpdate()
    {
        if (gameObject.GetComponent<KnightClass>().Skill3Move)
        {
            if (PlayerLookLeft) { rigid.velocity = new Vector2(-gameObject.GetComponent<KnightClass>().PullingSpeed, 0); }
            else { rigid.velocity = new Vector2(gameObject.GetComponent<KnightClass>().PullingSpeed, 0); }
        }

        if (PlayerMovAble == true&& !PlayerKnockBacking&& !gameObject.GetComponent<KnightClass>().Skill3Move) rigid.velocity = new Vector2(moveInput.x * speed, moveInput.y * speed);

        DashTime -= Time.deltaTime;
        if (PlayerKnockBacking) { GetComponent<PlayerDamaged>().KnockBack(); }
        if (DashTime <= 0) DashTime = 0;

        if(!PlayerMovAble&& IsDashing) rigid.velocity = DashVector * (float)Math.Sin(DashTimeLimit) * DashSpeed;

        if (DashTime <= 0 && IsDashing == true)
        {
            DashEnd();
        }
        if (DashCount > 0 && DashCoolTimeNow <= 0 && (Math.Abs(moveInput.x) > 0 || Math.Abs(moveInput.y) > 0)&& IsDashing == false)
        {
            DashAble = true;
        }
        else { DashAble = false; }


    }

    public void OnEndDrag()
    {
        joystickOn = false;
    }
    public void OnDrag()
    {
        joystickOn = true;
    }
    public void DashBtnClickedDown()
    {
        if (DashAble == true)
        {
            
            DashTime = DashTimeLimit + DashBump;
            GetDashVector();//대시 버튼 눌렀을때, 벡터값 가져오기 
            IsDashing = true;
            FlipBool = false;
            PlayerMovAble = false;
            animator.SetBool("Dashing", true);
            //Debug.Log("dashing");
        }

    }
    public void DashBtnClickedUp()
    {

    }
    public void Dashing()
    {
        
    }

    public void DashEnd() 
    {
        speed = PlayerBasicSpeed;
        animator.SetBool("Dashing", false);
        IsDashing = false;
        DashCoolTimeNow = DashCoolTimeMax;
        PlayerMovAble = true;
        FlipBool = true;
        DashTime = DashTimeLimit + DashBump;

    }
    public void DashRecover()
    {
        if (DashCount < DashCountLimit)
        {
            DashCount++;
            DashRecovCoolNow = DashRecovCoolMax;
        }
        CallDashRecovAble = true;
    }

    public void GetDashVector()
    {
        if (IsDashing == false)
        {
            DashVector = new Vector2(moveInput.x, moveInput.y).normalized * speed;
            DashCount--;
        }
    }



}
