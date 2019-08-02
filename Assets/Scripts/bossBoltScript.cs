using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossBoltScript : MonoBehaviour
{
    // Start is called before the first frame update
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
        if (other.CompareTag ("Boundary"))
        {
            return;//will not remove boundary
        }

        if (other.CompareTag ("Enemy"))
        {
            return;//will not remove enemy
        }

        if (other.CompareTag ("Boss"))
        {
            return;//will not remove boss
        }

        if (explosion != null)
        {
            Instantiate(explosion, transform.position, transform.rotation);
        }
        
        if (other.tag == "Player") //will destroy if player makes contact with enemy
        {
            Debug.Log ("I'm here");
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            gameController.GameOver();
        }

        gameController.AddScore (scoreValue);

        Destroy(other.gameObject); //destroy the players laser bolt
        Destroy(gameObject); //destroy object that this script is attached to
    }
}
