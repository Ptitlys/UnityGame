using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehavior : MonoBehaviour
{

    private SpriteRenderer attackSprite;
    private Animator attackAnimator;
    private Transform attackPosition;

    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
    private void Start()
    {
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Slime"))
        {
            other.GetComponent<SlimeBehavior>().TakeDamage(1);
        }
    }

    public IEnumerator Attack(Direction direction, float attackDuration)
    {
        attackAnimator = GetComponent<Animator>();
        attackSprite = GetComponent<SpriteRenderer>();
        attackPosition = GetComponent<Transform>();
        attackAnimator.Play("attack");
        attackPosition.localScale = new Vector3(1f, 1f, 1f);

        switch (direction)
        {
            case Direction.Up:
                attackPosition.localPosition = new Vector3(0, 0.1f, 0);
                attackPosition.localRotation = Quaternion.Euler(0, 0, 160);
                break;
            case Direction.Down:
                attackPosition.localPosition = new Vector3(0, -0.1f, 0);
                attackPosition.localRotation = Quaternion.Euler(0, 0, 160);

                break;
            case Direction.Left:
                attackPosition.localPosition = new Vector3(-0.1f, 0, 0);
                attackPosition.localRotation = Quaternion.Euler(-170, 10, -264);
                break;
            case Direction.Right:
                attackPosition.localPosition = new Vector3(-0.1f, 0, 0);
                attackPosition.localRotation = Quaternion.Euler(-170, 10, -264);
                break;
        }

        yield return new WaitForSeconds(attackDuration);
        Destroy(gameObject);
    }
}
