using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Boundary
{
     public float xMin, xMax, zMin, zMax;
}

public class playerController : MonoBehaviour
{
     public float speed;
     public float tilt;
     public Boundary boundary;
     public GameObject shot;
     public Transform shotSpawn;
     public float fireRate;
     private AudioSource audioSource;
     public AudioClip playerShoot;

     private Rigidbody rb;
     private float nextFire;


     private void Start()
     {
          rb = GetComponent<Rigidbody>();
          audioSource = GetComponent<AudioSource> ();
     }

     void Update()
     {
         if (Input.GetButton("Fire1") && Time.time > nextFire)
         {
             nextFire = Time.time + fireRate;
             Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
             audioSource.PlayOneShot(playerShoot, 0.5F);
         }
     }

     void FixedUpdate()
     {
          float moveHorizontal = Input.GetAxis("Horizontal");
          float moveVertical = Input.GetAxis("Vertical");

          Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
          rb.velocity = movement * speed;

          rb.position = new Vector3
          (
               Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
               0.0f,
               Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
          );

          rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
     }
}