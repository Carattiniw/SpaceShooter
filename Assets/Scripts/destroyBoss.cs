using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class destroyBoss : MonoBehaviour
{
    public GameObject explosion;
    public GameObject playerExplosion;
    private GameObject player; //used to find player gameobject in hierarchy
    public AudioClip bossHit;
    public AudioSource audioSource;
    public int scoreValue;
    public int bossLife;
    private gameController gameController;


    void Start () //will find our gameController script
    {
        player = GameObject.Find("Player"); //finds the player in the scene
        audioSource = GetComponent<AudioSource> ();

        
        GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent <gameController>();
        }

        if (gameController == null)
        {
            Debug.Log ("Cannot find 'GameController' script");
        }
    }


    void OnTriggerEnter(Collider collider) 
    {
        if (collider.gameObject.tag == "playerBolt") //if player shoots boss
        {
            bossLife = bossLife - 1;
            Debug.Log ("Hit!");
            audioSource.PlayOneShot(bossHit, 1.0F);

            if(bossLife == 0)
            {
                
                if (explosion != null)
                {
                    Instantiate(explosion, transform.position, transform.rotation);
                }
                Destroy(gameObject); //destroy object that this script is attached to
                gameController.AddScore (scoreValue);
                gameController.endGame();
            }
            Destroy(GameObject.FindWithTag("playerBolt"));
        }

        if (collider.gameObject.tag == "Player") //if player collides with boss
        {
            Debug.Log ("Crashed!");

            if (explosion != null)
            {
                Instantiate(playerExplosion, player.transform.position, player.transform.rotation);
            }

            Destroy(GameObject.FindWithTag("Player"));
            gameController.GameOver();
        }
    }
}
