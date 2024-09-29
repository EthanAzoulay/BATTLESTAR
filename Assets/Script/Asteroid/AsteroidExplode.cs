using UnityEngine;

public class AsteroidExplode : MonoBehaviour
{
    // Variable pour la vitesse de recul
    public float retreatSpeed = 5f;

    // Temps avant la destruction de l'objet (en secondes)
    public float destructionDelay = 10f;

    // Vitesse de r�duction de l'�chelle des enfants
    public float downscaleSpeed = 0.1f;

    // Start est appel� avant la premi�re mise � jour du frame
    void Start()
    {
        // D�truire l'objet apr�s un d�lai sp�cifi�
        Destroy(gameObject, destructionDelay);
    }

    // Update est appel� une fois par frame
    void Update()
    {
        // Faire reculer l'ast�ro�de sur l'axe Z
        transform.position -= new Vector3(0, 0, retreatSpeed * Time.deltaTime);

        // Downscale des enfants
        foreach (Transform child in transform)
        {
            // R�duire l'�chelle du child
            child.localScale -= Vector3.one * downscaleSpeed * Time.deltaTime;

            // Emp�cher l'�chelle de devenir n�gative
            if (child.localScale.x < 0) child.localScale = Vector3.zero;
        }
    }
}
