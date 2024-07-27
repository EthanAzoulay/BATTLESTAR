using UnityEngine;

public class Asteroid_Moving : MonoBehaviour
{
    public float minSpeed = 1f;
    public float maxSpeed = 2f;

    public float initialMinSpeed = 3f;
    public  float initialMaxSpeed = 9f;

    private Rigidbody rb;

    private float randomSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialMinSpeed = minSpeed;
        initialMaxSpeed = maxSpeed;

        SetRandomSpeed();
    }

    private void FixedUpdate()
    {
        rb.AddForce(Vector3.back * randomSpeed, ForceMode.Force);
    }

    private void SetRandomSpeed()
    {
        randomSpeed = Random.Range(minSpeed, maxSpeed);
    }

    public void ResetSpeed()
    {
        minSpeed = initialMinSpeed;
        maxSpeed = initialMaxSpeed;
        SetRandomSpeed();
    }

    public void IncreaseSpeed(float increment)
    {
        minSpeed += increment;
        maxSpeed += increment;
        SetRandomSpeed();
    }
}
