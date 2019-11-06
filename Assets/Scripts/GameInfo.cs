using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInfo : MonoBehaviour
{
    public int lives = 5;
    public int coins = 0;
    public int level = 1;
    public int maxLevel = 3;

    private void Awake()
    {
        int GameInfoCount = FindObjectsOfType<GameInfo>().Length;
        if (GameInfoCount > 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
