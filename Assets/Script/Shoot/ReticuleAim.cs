using UnityEngine;

public class ReticuleAim : MonoBehaviour
{
    public Camera mainCamera; // Référence à la caméra principale
    public PlayerShooting playerShooting; // Référence au script PlayerShooting
    public float raycastRange = 100f; // Portée du Raycast
    public bool showRay = true; // Afficher le Raycast

    void Update()
    {
        Aim();
    }

    void Aim()
    {
        // Récupérer la position du réticule dans l'espace écran
        Vector3 screenPosition = mainCamera.WorldToScreenPoint(transform.position);

        // Créer un Ray à partir de la position de la caméra passant par la position du réticule
        Ray ray = mainCamera.ScreenPointToRay(screenPosition);
        RaycastHit hit;

        if (showRay)
        {
            Debug.DrawRay(ray.origin, ray.direction * raycastRange, Color.red);
        }

        if (Physics.Raycast(ray, out hit, raycastRange))
        {
            if (hit.collider.CompareTag("Asteroid"))
            {
                playerShooting.SetTargetAsteroid(hit.collider.transform);
            }
            else
            {
                playerShooting.SetTargetAsteroid(null);
            }
        }
        else
        {
            playerShooting.SetTargetAsteroid(null);
        }
    }
}
