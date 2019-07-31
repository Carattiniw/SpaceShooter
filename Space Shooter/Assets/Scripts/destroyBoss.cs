using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class destroyBoss : MonoBehaviour
{
    public GameObject explosion;
    public GameObject playerExplosion;
    public GameObject playerPosition;
    public AudioClip bossHit;
    public AudioSource audioSource;
    public int scoreValue;
    public int bossLife;
    private gameController gameController;


    void Start () //will find our gameController script
    {
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


    //void OnTriggerEnter(Collider other) 
    void OnTriggerEnter(Collider collider) 
    {
        /*
        //if (other.tag == "Boundary" || other.tag == "Enemy") //will ignore the boundary gameObject
        if (other.CompareTag ("Boundary") || other.CompareTag("Enemy"))
        {
            return;//will ignore the boundary gameObject and enemy
        }

        if (other.CompareTag ("Boss"))
        {
            return;//will ignore the boss
        }

        if (explosion != null)
        {
            Instantiate(explosion, transform.position, transform.rotation);
            Debug.Log ("My fault!");
        }
        
        if (other.tag == "Player")
        {
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            //gameController.GameOver();
        }

        if (other.tag == "Boss")
        {
            bossLife = bossLife - 1;
            Debug.Log ("Hit!");

            if(bossLife == 0)
            {
                Destroy(GameObject.FindWithTag("Boss"));
                //gameController.AddScore (scoreValue);
            }
        }
        */

        
        /* */
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
                //Destroy(GameObject.FindWithTag("Boss"));
                Destroy(gameObject); //destroy object that this script is attached to
                //gameController.AddScore (scoreValue);
            }
            Destroy(GameObject.FindWithTag("playerBolt"));
        }

        if (collider.gameObject.tag == "Player") //if player collides with boss
        {
            Debug.Log ("Crashed!");

            if (explosion != null)
            {
                Instantiate(playerExplosion, playerPosition.transform.position, playerPosition.transform.rotation);
            }

            Destroy(GameObject.FindWithTag("Player"));
            //gameController.GameOver();
        }

        if (collider.gameObject.tag == "bossBolt" && collider.gameObject.tag == "Player") //if player collides with boss bolt
        //if (collider.gameObject.tag == "bossBolt")
        {
            Debug.Log ("Shot!");

            if (explosion != null)
            {
                Instantiate(playerExplosion, playerPosition.transform.position, playerPosition.transform.rotation);
            }

            Destroy(GameObject.FindWithTag("Player"));
            //gameController.GameOver();
        }
        

        //Destroy(other.gameObject); //destroy the players laser bolt
        //Destroy(gameObject); //destroy object that this script is attached to

        //Destroy(GameObject.FindWithTag("Enemy"));
        //Destroy(GameObject.FindWithTag("Player"));
    }
}
