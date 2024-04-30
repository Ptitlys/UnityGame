using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public TMP_Text LifeText;

    private int _lifeCount;

    // Start is called before the first frame update
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        instance = this;
    }

    public void UpdateLifeCounter(int life)
    {
        LifeText.text = $"Life: {++_lifeCount}";
    }
}
