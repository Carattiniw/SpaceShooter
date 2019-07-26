using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyByContact : MonoBehaviour
{
    public GameObject explosion;
    public GameObject playerExplosion;
    public int scoreValue;
    private gameController gameController;


    void Start () //will find our gameController script
    {
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


    void OnTriggerEnter(Collider other) 
    {
        //if (other.tag == "Boundary" || other.tag == "Enemy") //will ignore the boundary gameObject
        if (other.CompareTag ("Boundary") || other.CompareTag("Enemy"))
        {
            return;//will ignore the boundary gameObject and enemy
        }

        if (explosion != null)
        {
            Instantiate(explosion, transform.position, transform.rotation);
        }
        
        if (other.tag == "Player")
        {
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            gameController.GameOver();
        }

        gameController.AddScore (scoreValue);
        Destroy(other.gameObject); //destroy any object shot by player
        Destroy(gameObject); //destroy player if make contact with asteroid or enemy
    }
}
