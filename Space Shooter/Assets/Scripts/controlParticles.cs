using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controlParticles : MonoBehaviour
{
    private gameController gameController;
    private ParticleSystem ps;


    void Start () //will find our gameController script
    {
        ps = GetComponent<ParticleSystem>();
    }

    public void speedUp()
    {
        var main = ps.main;
        main.simulationSpeed = 15.0f;
        main.startSize = 0.3f;

        var ss = ps.sizeBySpeed;
        ss.range = new Vector2(0.0f, 10.0f);
    }
}
