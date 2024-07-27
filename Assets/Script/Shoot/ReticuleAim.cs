using UnityEngine;

public class ReticuleAim : MonoBehaviour
{
    public Camera mainCamera; // R�f�rence � la cam�ra principale
    public PlayerShooting playerShooting; // R�f�rence au script PlayerShooting
    public float raycastRange = 100f; // Port�e du Raycast
    public bool showRay = true; // Afficher le Raycast

    void Update()
    {
        Aim();
    }

    void Aim()
    {
        // R�cup�rer la position du r�ticule dans l'espace �cran
        Vector3 screenPosition = mainCamera.WorldToScreenPoint(transform.position);

        // Cr�er un Ray � partir de la position de la cam�ra passant par la position du r�ticule
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
