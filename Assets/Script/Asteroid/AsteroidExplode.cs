using UnityEngine;

public class AsteroidExplode : MonoBehaviour
{
    // Variable pour la vitesse de recul
    public float retreatSpeed = 5f;

    // Temps avant la destruction de l'objet (en secondes)
    public float destructionDelay = 10f;

    // Vitesse de réduction de l'échelle des enfants
    public float downscaleSpeed = 0.1f;

    // Start est appelé avant la première mise à jour du frame
    void Start()
    {
        // Détruire l'objet après un délai spécifié
        Destroy(gameObject, destructionDelay);
    }

    // Update est appelé une fois par frame
    void Update()
    {
        // Faire reculer l'astéroïde sur l'axe Z
        transform.position -= new Vector3(0, 0, retreatSpeed * Time.deltaTime);

        // Downscale des enfants
        foreach (Transform child in transform)
        {
            // Réduire l'échelle du child
            child.localScale -= Vector3.one * downscaleSpeed * Time.deltaTime;

            // Empêcher l'échelle de devenir négative
            if (child.localScale.x < 0) child.localScale = Vector3.zero;
        }
    }
}
