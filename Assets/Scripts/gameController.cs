using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class gameController : MonoBehaviour
{
    public GameObject[] hazards;
    public GameObject boss;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public float timeLeft = 60;
    private bool timerActive = true;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI restartText;
    public TextMeshProUGUI gameoverText;
    public TextMeshProUGUI winText;
    public TextMeshProUGUI timerText;

    public AudioClip bossMusic;
    public AudioClip gameOverMusic;
    public AudioClip winMusic;

    private bool gameOver;
    private bool bossBattle;
    private bool restart;

    private int score;
    private float time;
    private controlParticles m_particleSystem;
    private farPartSystem m_particleSystem2;
    private BGScroller bgSpeed;
    private AudioSource myAudioSource;



    void Start ()
    {
        myAudioSource = GetComponent<AudioSource>();

        gameOver = false;
        bossBattle = false;
        restart = false;
        scoreText.text = "";
        gameoverText.text = "";
        restartText.text = "";
        winText.text = "";
        timerText.text = "";
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


        GameObject m_particleSystem2Object = GameObject.FindWithTag ("ParticleSystem2");
        if (m_particleSystem2Object != null)
        {
            m_particleSystem2 = m_particleSystem2Object.GetComponent <farPartSystem>();
        }

        if (m_particleSystem2 == null)
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
            if (Input.GetKeyDown(KeyCode.T))//restart game with t key
            {
                SceneManager.LoadScene(0);
            }
        }

        if (Input.GetKey("escape"))//exit game with escape key
        {
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.Q))//restart game with t key
        {
            myAudioSource.Stop();
        }

        if (timerActive)
        {
            timeLeft -= Time.deltaTime;
            timerText.text = timeLeft.ToString("Timer: " + "0");

            if(timeLeft <= 0)
            {
                timeLeft = 0;
                timerActive = false;
                winText.text = "Time's Up!";
                GameOver();
            }
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
                restartText.text = "Press 'T' to try again or 'Esc' to quit";
                restart = true;
                break;
            }

            if (bossBattle)
            {
                StartCoroutine(SpawnBoss());
                break;
            }
        }
    }


    IEnumerator SpawnBoss()
    {
        myAudioSource.Stop();
        myAudioSource.PlayOneShot(bossMusic, 0.7F);
        m_particleSystem.speedUp();
        m_particleSystem2.speedUp2();
        bgSpeed.bgScrollSpeedUp();


        yield return new WaitForSeconds (startWait);
        Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
        Quaternion spawnRotation = Quaternion.identity;
        Instantiate (boss, spawnPosition, spawnRotation);
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
            bossBattle = true;
        }
    }

    public void GameOver()
    {
        timerActive = false;
        myAudioSource.Stop();
        myAudioSource.PlayOneShot(gameOverMusic, 0.7F);
        gameOver = true;
        gameoverText.text = "Game Over!";
        restartText.text = "Press 'T' to try again or 'Esc' to quit";
        restart = true;
    }

    public void endGame()
    {
        timerActive = false;
        myAudioSource.Stop();
        myAudioSource.PlayOneShot(winMusic, 0.7F);
        gameOver = true;
        gameoverText.text = "Game Over!";
        winText.text = "GAME CREATED BY WILLIAM CARATTINI";
        restartText.text = "Press 'T' to try again or 'Esc' to quit";
        restart = true;
    }
}
