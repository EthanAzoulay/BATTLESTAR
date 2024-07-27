using UnityEngine;

public class SimplePlayerShooting : MonoBehaviour
{
    public GameObject shootPoint;
    public GameObject bulletPrefab;
    private float nextFire = 0.0f;

    private PlayerShooting playerShooting;

    [HideInInspector]
    public float horizontalInput;
    [HideInInspector]
    public float verticalInput;

    // Assign the GameObject that contains the PlayerShooting script in the Inspector
    public GameObject player;

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

        if (player != null)
        {
            playerShooting = player.GetComponent<PlayerShooting>();

            if (playerShooting == null)
            {
                Debug.LogError("PlayerShooting script not found on the specified GameObject.");
            }
        }
        else
        {
            Debug.LogError("Player GameObject is not assigned.");
        }

        Debug.Log("SimplePlayerShooting initialized. Shoot point: " + shootPoint + ", Bullet prefab: " + bulletPrefab + ", Player: " + player);
    }

    void Update()
    {
        if (playerShooting != null)
        {
            // Utilisation des entrées du script de mouvement du joueur
            horizontalInput = playerShooting.horizontalInput;
            verticalInput = playerShooting.verticalInput;

            if (Time.time > nextFire && (horizontalInput != 0 || verticalInput != 0))
            {
                Debug.Log("Time check passed. Next fire time: " + nextFire + ", Current time: " + Time.time);
                nextFire = Time.time + playerShooting.fireRate;
                Debug.Log("Next fire time updated: " + nextFire);
                Shoot();
            }
            else if (horizontalInput == 0 && verticalInput == 0)
            {
                Debug.Log("Player is not moving. No shooting.");
            }
            else
            {
                Debug.Log("Time check failed. Next fire time: " + nextFire + ", Current time: " + Time.time);
            }
        }
        else
        {
            Debug.LogError("PlayerShooting script reference is missing.");
        }
    }

    void Shoot()
    {
        if (bulletPrefab == null || shootPoint == null)
        {
            Debug.LogError("Bullet prefab or shoot point is not assigned.");
            return;
        }

        Debug.Log("Shooting bullet");
        Instantiate(bulletPrefab, shootPoint.transform.position, shootPoint.transform.rotation);
        Debug.Log("Bullet instantiated at position: " + shootPoint.transform.position);
    }

    // Méthode publique pour assigner le joueur
    public void SetPlayer(GameObject player)
    {
        this.player = player;
        playerShooting = player.GetComponent<PlayerShooting>();

        if (playerShooting == null)
        {
            Debug.LogError("PlayerShooting script not found on the specified GameObject.");
        }
        else
        {
            Debug.Log("Player assigned successfully.");
        }
    }
}
