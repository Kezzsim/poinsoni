using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityOSC;

public class pointAction : MonoBehaviour
{
    
  
    public ParticleSystem particles;


    // Start is called before the first frame update
    void Start()
    {
        OSCHandler.Instance.Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider wave)
    {
 
        GameObject particle = Instantiate(particles.gameObject, this.transform);
        List<object> values = new List<object>();
        values.AddRange(new object[] { gameObject.name, this.transform.position.x, this.transform.position.y, this.transform.position.z});
        OSCHandler.Instance.SendMessageToClient("test", "/maxMSP/test/trigger", values);

    }


}
