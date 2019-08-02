using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class farPartSystem : MonoBehaviour
{
    private gameController gameController;
    private ParticleSystem part;


    void Start () //will find our gameController script
    {
        part = GetComponent<ParticleSystem>();
    }

    public void speedUp2()
    {
        var main = part.main;
        main.simulationSpeed = 15.0f;
        main.startSize = 0.15f;

        var ss = part.sizeBySpeed;
        ss.range = new Vector2(0.0f, 10.0f);
    }
}
