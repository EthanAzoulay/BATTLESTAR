using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mesh_Portal : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Game_Manager.Instance.ResetGame(); // Réinitialise les pièces et les items
            Destroy(other.gameObject);


        }
    }
}
