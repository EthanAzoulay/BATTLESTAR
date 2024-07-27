using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject shootPoint;
    public GameObject bulletPrefab;
    public float fireRate = 0.5f;
    private float nextFire = 0.0f;

    private Vector3? targetPosition = null;
    private bool aimAtTarget = false;

    private Transform targetAsteroid;

    [HideInInspector]
    public float horizontalInput;
    [HideInInspector]
    public float verticalInput;

    void Start()
    {
        if (shootPoint == null)
        {
            Debug.LogError("Shoot point is not assigned.");
        }
        if (bulletPrefab == null)
        {
            Debug.LogError("Bullet prefab is not assigned.");
        }
    }

    void Update()
    {
        if (Time.time > nextFire && (horizontalInput != 0 || verticalInput != 0))
        {
            nextFire = Time.time + fireRate;
            Shoot();
        }

        if (aimAtTarget && targetPosition.HasValue)
        {
            RotateShootPoint(targetPosition.Value);
        }
    }

    public void AimAt(Vector3? targetPosition)
    {
        this.targetPosition = targetPosition;
        aimAtTarget = targetPosition.HasValue;

        if (targetPosition.HasValue)
        {
            Debug.Log("Aiming at: " + targetPosition.Value);
        }
        else
        {
            Debug.Log("No target to aim at.");
        }
    }

    private void RotateShootPoint(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - shootPoint.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        shootPoint.transform.rotation = Quaternion.Slerp(shootPoint.transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    public void SetTargetAsteroid(Transform asteroidTransform)
    {
        targetAsteroid = asteroidTransform;

        if (asteroidTransform != null)
        {
            Debug.Log("Target asteroid set to: " + asteroidTransform.name);
        }
        else
        {
            Debug.Log("No asteroid target.");
        }
    }

    void Shoot()
    {
        if (bulletPrefab == null || shootPoint == null)
        {
            Debug.LogError("Bullet prefab or shoot point is not assigned.");
            return;
        }

        GameObject bullet = Instantiate(bulletPrefab, shootPoint.transform.position, shootPoint.transform.rotation);
        Bullet_Moving bulletScript = bullet.GetComponent<Bullet_Moving>();
        if (bulletScript != null)
        {
            bulletScript.SetTarget(targetAsteroid);
        }
        Debug.Log("Bullet shot towards: " + (targetAsteroid != null ? targetAsteroid.name : "No target"));
    }
}
