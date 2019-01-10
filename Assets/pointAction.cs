using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pointAction : MonoBehaviour
{

    public AudioSource testSound;
    public ParticleSystem particles;


    // Start is called before the first frame update
    void Start()
    {
        testSound = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider wave)
    {
        testSound.Play();
        GameObject particle = Instantiate(particles.gameObject, this.transform);

    }


}
