using Cinemachine;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region State Variables
    public PlayerStateMachine StateMachine { get; private set; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerWallSlideState WallSlideState { get; private set; }
    public PlayerInAirState InAirState { get; private set; }
    [SerializeField]
    public PlayerData playerData;
    #endregion

    #region Components
    public Animator Anim { get; private set; }
    public PlayerInputHandler inputHandler { get; private set; }
    public Rigidbody2D RB { get; private set; }
    #endregion

    #region Check Transforms

    [SerializeField]
    private Transform groundCheck;

    [SerializeField]
    private Transform wallCheck;

    #endregion
    #region Other Variables
    public Vector2 CurrentVelocity { get; private set; }
    public int FacingDirection;
    public int gravityDirection;
    private Vector2 workspace;  // Temporary variable to store vector data
    public TextMeshProUGUI stateName;
    public ParticleSystem dust;

    public GameObject backGroundObject;
    public CinemachineVirtualCamera sceneCamera;
    public Color gravityBackgroundColor;
    private Color initialBackgroundColor;
    public GameObject running;
    public GameObject jump;
    public GameObject gravity;
    public GameObject landing;

    public GameObject gravityEffect;
    private Color gravityEffectColor;
    private float newAlpha;
    #endregion

    #region Timers  
    // Need to find a better alternative to these ??
    public float gravityTimer = 0.0f;
    public bool gravityTimerStart = false;

    public float gravityCoolDownTimer = 0.0f;
    public bool gravityCoolDownTimerStart = false;
    #endregion

    #region Unity callback Functions
    private void Awake()
    {
        // Creating all player states
        StateMachine = new PlayerStateMachine();
        IdleState = new PlayerIdleState(this, StateMachine, playerData, "idle");
        MoveState = new PlayerMoveState(this, StateMachine, playerData, "move");
        InAirState = new PlayerInAirState(this, StateMachine, playerData, "inAir");
        WallSlideState = new PlayerWallSlideState(this, StateMachine, playerData, "wallSlide");
    }

    private void Start()
    {
        Anim = GetComponent<Animator>();
        inputHandler = GetComponent<PlayerInputHandler>();
        RB = GetComponent<Rigidbody2D>();
        FacingDirection = 1;
        gravityDirection = 1;
        StateMachine.Initialize(IdleState);

        SpriteRenderer m_SpriteRenderer;
        m_SpriteRenderer = backGroundObject.GetComponent<SpriteRenderer>();
        initialBackgroundColor = m_SpriteRenderer.color;
        gravityEffectColor = gravityEffect.GetComponent<SpriteRenderer>().color;
        gravityEffect.SetActive(false);
       
    }

    private void Update()
    { 
        CurrentVelocity = RB.velocity;
        StateMachine.CurrentState.LogicUpdate();
        stateName.text = StateMachine.CurrentState.ToString();
        UpdateGravity();
        GravityTimer();
        GravityCoolDownTimer();
        CancelGravityInput();

        if (gravityTimerStart)
        {
            StartGravityEffect();
            //ChangeBackGroundColor(gravityBackgroundColor);
            //sceneCamera.m_Lens.OrthographicSize = Mathf.Lerp(sceneCamera.m_Lens.OrthographicSize, 9f, Time.deltaTime / 1f);
        }
        else
        {
            EndGravityEffect();
            //ChangeBackGroundColor(initialBackgroundColor);
            //sceneCamera.m_Lens.OrthographicSize = Mathf.Lerp(sceneCamera.m_Lens.OrthographicSize, 10, Time.deltaTime / 1f);
        }
    }

    void StartGravityEffect()
    {
        gravityEffect.SetActive(true);
        newAlpha = Mathf.Clamp(newAlpha+Time.deltaTime,0,gravityEffectColor.a);
        Color newColor = new Color(gravityEffectColor.r, gravityEffectColor.g, gravityEffectColor.b, newAlpha);
        gravityEffect.GetComponent<SpriteRenderer>().color = newColor;
    }

    void EndGravityEffect()
    {
        newAlpha = Mathf.Clamp(newAlpha - Time.deltaTime, 0, gravityEffectColor.a);
        Color newColor = new Color(gravityEffectColor.r, gravityEffectColor.g, gravityEffectColor.b, newAlpha);
        gravityEffect.GetComponent<SpriteRenderer>().color = newColor;
        if(newAlpha < 0.1)
        {
            gravityEffect.SetActive(false);
        }
    }
    private void ChangeBackGroundColor(Color targetColor)
    {
        SpriteRenderer m_SpriteRenderer;
        m_SpriteRenderer = backGroundObject.GetComponent<SpriteRenderer>();
        m_SpriteRenderer.color = Color.Lerp(m_SpriteRenderer.color, targetColor, Time.deltaTime / 1f);
    }

    private void CancelGravityInput()
    {
        if (!inputHandler.GravityInput && gravityDirection == -1)
        {
            gravityTimer = playerData.flipGravityTime;
            gravityTimerStart = false;
            gravityCoolDownTimerStart = true;
            ResetGravity();
        }

        if(CheckIfGrounded() && gravityDirection == 1)
        {
            gravityTimer = 0;
        }
    }

    private void GravityCoolDownTimer()
    {
        if (gravityCoolDownTimerStart)
        {
            gravityCoolDownTimer += Time.deltaTime;
            if (gravityCoolDownTimer >= playerData.gravityCoolDown || CheckIfGrounded())
            {
                gravityCoolDownTimerStart = false;
                gravityCoolDownTimer = 0.0f;
            }
        }
    }

    private void GravityTimer()
    {
        if (gravityTimerStart)
        {
            gravity.SetActive(true);
            gravityTimer += Time.deltaTime;
            if (gravityTimer >= playerData.flipGravityTime)
            {
                gravityTimer = 0;
                gravityTimerStart = false;
                gravityCoolDownTimerStart = true;
                ResetGravity();
            }
        }
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    #endregion

    #region Set Functions

    public void SetVelocity(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();
        workspace.Set(angle.x * velocity * direction, angle.y * velocity);
        RB.velocity = workspace;
        CurrentVelocity = workspace;
    }
    public void SetVelocityX(float velocity)
    {
        workspace.Set(velocity, CurrentVelocity.y);
        RB.velocity = workspace;
        CurrentVelocity = RB.velocity;
    }

    public void SetVelocityY(float velocity)
    {
        workspace.Set(CurrentVelocity.x, velocity);
        RB.velocity = workspace;
        CurrentVelocity = workspace;
    }

    #endregion

    #region Check Functions

    public bool CheckIfGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, playerData.groundCheckRadius, playerData.whatIsGround);
    }
    public void CheckIfShouldFlip(int xInput)
    {
        if (xInput != 0 && xInput != FacingDirection)
        {
            Flip();
        }
    }

    public bool CheckIfTouchingWall()
    {
        Debug.DrawRay(wallCheck.position, Vector2.right * FacingDirection * playerData.wallCheckDistance, Color.red);
        return Physics2D.Raycast(wallCheck.position, Vector2.right * FacingDirection, playerData.wallCheckDistance, playerData.whatIsGround);
    }

    public bool CheckIfTouchingWallBack()
    {
        Debug.DrawRay(wallCheck.position, -Vector2.right * FacingDirection * playerData.wallCheckDistance, Color.blue);
        return Physics2D.Raycast(wallCheck.position, -Vector2.right * FacingDirection, playerData.wallCheckDistance, playerData.whatIsGround);
    }

    #endregion

    #region Other Functions
    private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();

    private void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();
    
    public void Flip()
    {
        FacingDirection *= -1;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    public void ReverseGravity()
    {
        if(!gravityCoolDownTimerStart)
        {
            gravityDirection = -1;
            SetVelocityY(playerData.gravityVelocity);
            if (Mathf.Abs(transform.rotation.eulerAngles.z) < 0.01f)
            {
                transform.Rotate(180f, 0.0f, 0.0f);
            }
            gravityTimerStart = true;
        }
    }

    public void ResetGravity()
    {
        gravity.SetActive(false);
        gravityDirection = 1;
        
        if (Mathf.Abs(transform.rotation.eulerAngles.z - 180f) < 0.01f)
        {
            transform.Rotate(-180f, 0.0f, 0.0f);
        }
        SetVelocityY(-playerData.gravityVelocity);
    }

    private void UpdateGravity()
    {
        if(CurrentVelocity.y * gravityDirection < 0)
        {
            RB.gravityScale = playerData.gravityScale * playerData.gravityMultiplier * gravityDirection;
        }

        else
        {
            RB.gravityScale = playerData.gravityScale * gravityDirection;
        }
    }

    public void CreateDust()
    {
        dust.Play();
    }

    #endregion
}
