using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mesh_Portal : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Game_Manager.Instance.ResetGame(); // R�initialise les pi�ces et les items
            Destroy(other.gameObject);


        }
    }
}
