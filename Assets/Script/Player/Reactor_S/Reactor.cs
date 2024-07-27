using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Reactor", menuName = "My Game/Reactor")]
public class Reactor : ScriptableObject
{
    public GameObject model;
    public float speed;
    public float rotationSpeed;
    public float fireRate;

}
