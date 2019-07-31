using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroller : MonoBehaviour
{
    public float scrollSpeed;
    //private float scrollSpeed;
    public float tileSizedZ;
    private Vector3 startPosition;
    private BGScroller bg;

    // Start is called before the first frame update
    void Start()
    {
        //scrollSpeed = -0.25f;
        bg = GetComponent<BGScroller>();
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileSizedZ);
        //float newPosition = Mathf.Repeat(Time.smoothDeltaTime * scrollSpeed, tileSizedZ);

        transform.position = startPosition + Vector3.forward * newPosition;
        //transform.position = startPosition + Vector3.forward * newPosition;
    }

    public void bgScrollSpeedUp()
    {
        //scrollSpeed = -0.75f * Time.deltaTime;
        scrollSpeed = -0.75f;
        
        float newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileSizedZ);
        //float newPosition = Mathf.Repeat(Time.smoothDeltaTime * scrollSpeed, tileSizedZ);

        //transform.position = startPosition + Vector3.forward * newPosition;
        transform.position = startPosition + Vector3.forward * newPosition;
    }
}
