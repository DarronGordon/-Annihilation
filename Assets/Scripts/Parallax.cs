using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    float length, startPos;
    [SerializeField] GameObject camera;
    [SerializeField] float parallaxEffect;

    void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x; 
    }

    void FixedUpdate()
    {
        float t = (camera.transform.position.x * (1 - parallaxEffect));

        float dist = (camera.transform.position.x * parallaxEffect);

        transform.position = new Vector3(startPos + dist, transform.position.y, transform.position.z);

        if (t > startPos + length) 
        {
            startPos += length;
        }
        else if(t  < startPos - length) 
        {
            startPos -= length;
        }
    }
}
