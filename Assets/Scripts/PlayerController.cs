using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float longIdleTimer = 5f;
    public float speed = 2.5f;
    public float jumpForce = 2.5f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float groundCheckRadius;

    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private Vector2 _movement;
    private bool _facingRight = true;
    private bool _isGrounded;
    private bool _isAttacking;
    private float _longIdleTimer;

    private void Awake()
    {
        this._rigidbody2D = this.GetComponent<Rigidbody2D>();
        this._animator = this.GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this._isAttacking ==  false)
        {
            //Movement
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            this._movement = new Vector2(horizontalInput, 0f);
            //Flip character
            if (horizontalInput < 0f && this._facingRight == true)
            {
                this.Flip();
            }
            else if (horizontalInput > 0f && _facingRight == false)
            {
                this.Flip();
            }
        }
        //Is Grounded?
        this._isGrounded = Physics2D.OverlapCircle(this.groundCheck.position, this.groundCheckRadius, this.groundLayer);
        //Is Jumping?
        if (Input.GetButtonDown("Jump") && this._isGrounded == true && this._isAttacking == false)
        {
            this._rigidbody2D.AddForce(Vector2.up * this.jumpForce, ForceMode2D.Impulse);
        }
        //Wanna Attack?
        if (Input.GetButtonDown("Fire1") && this._isGrounded == true && this._isAttacking == false)
        {
            this._movement = Vector2.zero;
            this._rigidbody2D.velocity = Vector2.zero;
            this._animator.SetTrigger("Attack");
        }
    }
    private void FixedUpdate()
    {
        if (this._isAttacking == false)
        {
            float horizontalVelocity = this._movement.normalized.x * this.speed;
            this._rigidbody2D.velocity = new Vector2(horizontalVelocity, this._rigidbody2D.velocity.y);
        }
    }

    private void LateUpdate()
    {
        this._animator.SetBool("Idle",this._movement == Vector2.zero);
        this._animator.SetBool("IsGrounded",this._isGrounded);
        this._animator.SetFloat("VerticalVelocity",this._rigidbody2D.velocity.y);
        //Animator
        if (this._animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            this._isAttacking = true;
        }
        else 
        {
            this._isAttacking = false;
        }
        //Long Idle
        if (this._animator.GetCurrentAnimatorStateInfo(0).IsTag("Idle"))
        {
            this._longIdleTimer += Time.deltaTime;
            if (this._longIdleTimer >= this.longIdleTimer)
            {
                this._animator.SetTrigger("LongIdle");
            }
        }
        else
        {
            this._longIdleTimer = 0f;
        }
    }

    private void Flip()
    {
        this._facingRight = !this._facingRight;
        float localScaleX = this.transform.localScale.x;
        localScaleX = localScaleX * -1f;
        this.transform.localScale = new Vector3(localScaleX, this.transform.localScale.y,this.transform.localScale.z);
    }
}
