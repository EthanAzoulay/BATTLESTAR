using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wings", menuName = "My Game/Wings")]
public class Wings : ScriptableObject
{
    public GameObject model;
    public float speed;
    public float rotationSpeed;
    public float fireRate;

}
