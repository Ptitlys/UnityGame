using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject player;

    public GameObject caisse;

    private GameObject canvas;

    // Start is called before the first frame update
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
    }

    private void Start()
    {
        canvas = GameObject.Find("Canvas");
    }

    private void initiateLife()
    {
        GameObject life = new();
    }
    public void GameOver()
    {
        Debug.Log("Gros nullos");
    }

    public void CaisseLostLife()
    {

    }
}
