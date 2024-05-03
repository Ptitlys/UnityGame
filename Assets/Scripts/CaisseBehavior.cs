using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaisseBehavior : MonoBehaviour
{
    public int life;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void TakeDamage()
    {
        life -= 1;
        GameManager.instance.CaisseLostLife();
        if (life <= 0)
        {
            Destroy(gameObject);
            GameManager.instance.GameOver();
            Debug.Log("Caisse is dead");
        }
    }

}
