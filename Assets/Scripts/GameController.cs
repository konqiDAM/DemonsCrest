using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private bool end;
    private byte transparency = 0;
    [SerializeField] Text livesText;
    [SerializeField] Text coinsText;
    [SerializeField] int minSpawnTime = 1;
    [SerializeField] int maxSpawnTime = 2;
    [SerializeField] Transform ghostPrefab;
    [SerializeField] Image gameOver;
    static private int lives = 5;
    static private int coins = 0;
    //private GameInfo game;
    static private int actualLevel = 1;
    static private int maxLevel = 3;


    void Start()
    {
        updateLivesText();
        updateCoinsText();
        StartCoroutine(spawnGhost(Random.Range(minSpawnTime, maxSpawnTime)));
        end = false;
        //game = FindObjectOfType<GameInfo>();
        //lives = game.lives;
        //coins = game.coins;
    }

    IEnumerator spawnGhost(int time)
    {
        yield return new WaitForSeconds(time);
        Vector3 a = transform.position;
        a.z = 0;
        a.y = -0.7f;//Floor height
        a.x = a.x + Random.Range(-3, 3);//camera render distance
        Transform e = Instantiate(ghostPrefab, a, Quaternion.identity);

        StartCoroutine(spawnGhost(Random.Range(minSpawnTime, maxSpawnTime)));
    }
    private void updateLivesText()
    {
        if(lives < 0)
            lives = 0;
        livesText.text = new string('♥', lives);
    }
    private void updateCoinsText()
    {
        coinsText.text = "Giles: " + coins;
    }

    public void playerHit()
    {
        lives--;
        updateLivesText();
        if (lives <= 0)
            FindObjectOfType<Player>().playerDies();

    }

    public void pickHealth()
    {
        lives++;
        updateLivesText();
    }


    public void pickCoin()
    {
        coins++;
        updateCoinsText();
    }


    public void nextLevel()
    {
        if (actualLevel >= maxLevel)
        {
            actualLevel = maxLevel;
            Time.timeScale *= 1.2f;
        }
        else
            actualLevel++;
        SceneManager.LoadScene("Level" + actualLevel);
    }

    public IEnumerator gameOverScreen()
    {
        yield return new WaitForSeconds(0.001f);
        if (transparency < 255)
            transparency++;
        gameOver.color = new Color32(255, 255, 255, transparency);
        StartCoroutine(gameOverScreen());

    }

    public IEnumerator changeToWelcome()
    {
        Time.timeScale = 1f;
        StartCoroutine(gameOverScreen());
        yield return new WaitForSeconds(3);

        lives = 5;
        coins = 0;
        actualLevel = 1;
        SceneManager.LoadScene("Welcome");
    }
}
