using System;
using Unity.VisualScripting;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerBehavior : MonoBehaviour
{
    public float speed;
    public Animator animator;
    private Vector2 moveInput;
    private bool isFacingRight = true;
    private Rigidbody2D _rb;
    private SpriteRenderer _sr;

    // Dash settings
    // private AttackBehavior attackBehavior;
    public GameObject attackEffectPrefab;
    public int dashSpeed;
    public float dashLength = 0.5f;
    public float dashCooldown = 0.1f;
    private float dashCoolCounter;
    private float dashCounter;
    private float activeSpeed;
    private AttackBehavior.Direction direction;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();

        activeSpeed = speed;
        direction = AttackBehavior.Direction.Down;
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
                direction = AttackBehavior.Direction.Left;
                FlipX();
            }
            else if (moveInput.x < 0 && !isFacingRight)
            {
                direction = AttackBehavior.Direction.Right;
                FlipX();
            }

            moveInput.y = 0;
        }
        else
        {
            if (moveInput.y > 0)
            {
                direction = AttackBehavior.Direction.Up;
            }
            else if (moveInput.y < 0)
            {
                direction = AttackBehavior.Direction.Down;
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
                Debug.Log("Dash");
                GameObject attackEffect = Instantiate(attackEffectPrefab, gameObject.transform.position, Quaternion.identity);
                if (attackEffect != null)
                {
                    attackEffect.transform.SetParent(gameObject.transform);
                    AttackBehavior attackBehavior = attackEffect.GetComponent<AttackBehavior>();
                    if (attackBehavior != null)
                    {
                        StartCoroutine(attackBehavior.Attack(direction, dashLength));
                    }
                }
            }
        }

        if (dashCounter > 0)
        {
            dashCounter -= Time.deltaTime;
            if (dashCounter <= 0)
            {
                activeSpeed = speed;
                dashCoolCounter = dashCooldown;
            }
        }

        if (dashCoolCounter > 0)
        {
            dashCoolCounter -= Time.deltaTime;
        }
    }

    private void FlipX()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        isFacingRight = !isFacingRight;
    }
}
