using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossWeaponController : MonoBehaviour
{
    public GameObject shot;
    public Transform shotSpawn1;
    public Transform shotSpawn2;
    public float fireRate;
    public float delay;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        //InvokeRepeating("Fire", delay, fireRate);
        StartCoroutine (Fire());
    }

    IEnumerator Fire()
    {
        while (true)
        {
            yield return new WaitForSeconds (fireRate);
            Instantiate (shot, shotSpawn1.position, shotSpawn1.rotation);
            audioSource.Play();
            yield return new WaitForSeconds (delay);
            Instantiate (shot, shotSpawn2.position, shotSpawn2.rotation);
            audioSource.Play();
        }
    }
}
