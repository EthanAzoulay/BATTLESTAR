using UnityEngine;

public class Shield : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Vérifier si l'objet avec lequel le bouclier entre en collision a le tag "Asteroid"
        if (other.gameObject.CompareTag("Asteroid"))
        {
            // Détruire l'astéroïde
            Destroy(other.gameObject);

            // Détruire le bouclier
            Destroy(gameObject);
        }
    }
}
