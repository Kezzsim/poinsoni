using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pulsewave : MonoBehaviour
{
    Vector3 value;
    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            //OnMouseClick and hold, box increaces in size, colliding with points and generating events
            //Eventually will also be mapped to vive controller trigger
            float t = Time.deltaTime;
            value = transform.localScale;
            value.x += t;
            value.y += t;
            value.z += t;

            transform.localScale = value;
        }
    }
}
