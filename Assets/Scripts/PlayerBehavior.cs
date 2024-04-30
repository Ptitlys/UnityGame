using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    //[HideInInspector]  Hide in the inspector
    //[SerializeField]  Show in the inspector even if private
    public float speed;
    public Animator animator;

    private Vector2 moveInput;
    private bool isFacingRight = true;

    private Rigidbody2D _rb;
    private SpriteRenderer _sr;

    private Animator dashAnimator;


    // Dash settings
    public UnityEngine.Object dashEffect;
    private bool isDashing = false;
    private SpriteRenderer dashSprite;
    public int dashSpeed;
    public float dashLength = 0.5f;
    public float dashCooldown = 0.1f;
    private float dashCoolCounter;
    private float dashCounter;
    private float activeSpeed;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
        dashAnimator = dashEffect.GetComponent<Animator>();
        dashSprite = dashEffect.GetComponent<SpriteRenderer>();
        dashSprite.enabled = false;
        activeSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        if (Mathf.Abs(moveInput.x) > Mathf.Abs(moveInput.y))
        {
            if (moveInput.x > 0 && isFacingRight)
            {
                Flip();
            }
            else if (moveInput.x < 0 && !isFacingRight)
            {
                Flip();
            }

            moveInput.y = 0;
        }
        else
        {
            if (moveInput.y > 0)
            {

            }
            else if (moveInput.y < 0)
            {

            }
            moveInput.x = 0;
        }

        moveInput.Normalize();

        Dash();
        _rb.velocity = moveInput * activeSpeed;
        Debug.Log(String.Format("x:{0} y:{1} speed:{2}", moveInput.x, moveInput.y, activeSpeed));
        animator.SetInteger("x_velocity", Convert.ToInt32(moveInput.x));
        animator.SetInteger("y_velocity", Convert.ToInt32(moveInput.y));
    }

    private void Dash()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (dashCoolCounter <= 0 && dashCounter <= 0)
            {
                activeSpeed = dashSpeed;
                dashCounter = dashLength;
                dashSprite.enabled = true;
                dashAnimator.SetBool("isDashing", true);
            }
        }

        if (dashCounter > 0)
        {
            dashCounter -= Time.deltaTime;
            if (dashCounter <= 0)
            {
                activeSpeed = speed;
                dashCoolCounter = dashCooldown;
                dashAnimator.SetBool("isDashing", false);
                dashSprite.enabled = false;
            }
        }

        if (dashCoolCounter > 0)
        {
            dashCoolCounter -= Time.deltaTime;
        }
    }

    private void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        isFacingRight = !isFacingRight;
    }
}
