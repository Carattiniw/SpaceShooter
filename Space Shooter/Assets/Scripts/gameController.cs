using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class gameController : MonoBehaviour
{
    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI restartText;
    public TextMeshProUGUI gameoverText;
    public TextMeshProUGUI winText;

    private bool gameOver;
    private bool restart;
    private int score;
    private controlParticles m_particleSystem;
    private BGScroller bgSpeed;

    void Start ()
    {
        gameOver = false;
        restart = false;
        scoreText.text = "";
        gameoverText.text = "";
        restartText.text = "";
        winText.text = "";
        score = 0;
        UpdateScore();
        StartCoroutine (SpawnWaves ());

        GameObject m_particleSystemObject = GameObject.FindWithTag ("ParticleSystem");
        if (m_particleSystemObject != null)
        {
            m_particleSystem = m_particleSystemObject.GetComponent <controlParticles>();
        }

        if (m_particleSystem == null)
        {
            Debug.Log ("Cannot find 'ParticleSystem' script");
        }

        GameObject bgSpeedObject = GameObject.FindWithTag ("background");
        if (bgSpeedObject != null)
        {
            bgSpeed = bgSpeedObject.GetComponent <BGScroller>();
        }

        if (bgSpeed == null)
        {
            Debug.Log ("Cannot find 'ParticleSystem' script");
        }
    }

    void Update()
    {
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.T))//restart game with r key
            {
                SceneManager.LoadScene(0);
            }
        }

        if (Input.GetKey("escape"))//exit game with escape key
        {
            Application.Quit();
        }
    }

    IEnumerator SpawnWaves ()
    {
        yield return new WaitForSeconds (startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range (0, hazards.Length)];
                Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate (hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds (spawnWait);
            }
            yield return new WaitForSeconds (waveWait);

            if (gameOver)
            {
                restartText.text = "Press 'T' for Restart or 'Esc' to quit";
                restart = true;
                break;
            }
        }
    }

    public void AddScore (int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Points: " + score;

        if (score >= 100)
        {
            gameOver = true;
            restart = true;
            endGame();
        }
    }

    public void GameOver()
    {
        gameOver = true;
    }

    void endGame()
    {
        gameOver = true;
        gameoverText.text = "Game Over!";
        winText.text = "GAME CREATED BY WILLIAM CARATTINI";
        m_particleSystem.speedUp();
        bgSpeed.bgScrollSpeedUp();
    }
}
