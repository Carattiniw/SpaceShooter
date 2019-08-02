using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroller : MonoBehaviour
{
    //public float scrollSpeed;
    private float scrollSpeed;
    public float tileSizedZ;

    private Vector3 startPosition;
    private BGScroller bg;
    private float newSpeed;

    private Material material;
    private Vector2 offSet;



    [Range(0f,5f)][SerializeField]private float xVel = 0.01f, yVel = 0.3f;


    private void Awake()
    {
        material = GetComponent<Renderer>().material;
    }



    void Update()
    {
        offSet = new Vector2(xVel, yVel);
        material.mainTextureOffset += offSet*Time.deltaTime;
    }

    public void bgScrollSpeedUp()
    {
        xVel = 0.06f;
        yVel = 0.7f;
    }
}
