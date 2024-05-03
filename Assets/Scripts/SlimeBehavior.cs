using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBehavior : MonoBehaviour
{
    public SlimeData slimeData;

    private int life;
    private float speed;

    private GameObject caisse;

    // Start is called before the first frame update
    void Start()
    {
        life = slimeData.life;
        speed = slimeData.speed;
        caisse = GameManager.instance.caisse;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, caisse.transform.position, speed * Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
        life -= damage;
        if (life <= 0)
        {
            Destroy(gameObject);
            Debug.Log("Slime is dead");
        }
    }
}
