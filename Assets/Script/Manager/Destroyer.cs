using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Asteroid") || other.gameObject.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Asteroid") || other.gameObject.CompareTag("Bullet") || other.gameObject.CompareTag("Portal"))
        {
            Destroy(other.gameObject);
        }
    }


}
