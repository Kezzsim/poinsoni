using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pointAction : MonoBehaviour
{

  
    public ParticleSystem particles;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider wave)
    {
 
        GameObject particle = Instantiate(particles.gameObject, this.transform);

    }


}
