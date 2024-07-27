using UnityEngine;

public class Shield : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // V�rifier si l'objet avec lequel le bouclier entre en collision a le tag "Asteroid"
        if (other.gameObject.CompareTag("Asteroid"))
        {
            // D�truire l'ast�ro�de
            Destroy(other.gameObject);

            // D�truire le bouclier
            Destroy(gameObject);
        }
    }
}
