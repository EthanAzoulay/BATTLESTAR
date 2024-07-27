using UnityEngine;

public class ReticleRaycast : MonoBehaviour
{
    public Transform reticle; // Assignez ceci dans l'éditeur avec le GameObject du réticule
    public float raycastDistance = 100f; // La distance du raycast
    public PlayerShooting playerShooting; // Référence au script PlayerShooting
    public Transform mainCamera; // Référence à la caméra

    private Transform asteroidTransform; // Référence à l'astéroïde touché

    void Update()
    {
        Vector3 raycastDirection = (reticle.position - mainCamera.position).normalized;
        Ray ray = new Ray(mainCamera.position, raycastDirection);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, raycastDistance))
        {
            Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.red);
            if (hit.collider.CompareTag("Asteroid"))
            {
                Debug.Log("Asteroid detected at: " + hit.point);
                asteroidTransform = hit.transform;
                playerShooting.AimAt(hit.point);
            }
            else
            {
                asteroidTransform = null;
                playerShooting.AimAt(null);
            }
        }
        else
        {
            Debug.DrawRay(ray.origin, ray.direction * raycastDistance, Color.red);
            asteroidTransform = null;
            playerShooting.AimAt(null);
        }

        playerShooting.SetTargetAsteroid(asteroidTransform);
    }
}
