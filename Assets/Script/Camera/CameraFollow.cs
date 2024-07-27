using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;  // Référence au joueur
    public float positionA = 5f;  // Position Y à partir de laquelle la caméra monte
    public float positionB = -5f; // Position Y à partir de laquelle la caméra descend
    public float speed = 2f;      // Vitesse de déplacement de la caméra
    public Vector3 middlePosition; // Position cible lorsque le joueur est entre les positions A et B
    public Vector3 positionATarget; // Position cible lorsque le joueur dépasse la position A
    public Vector3 positionBTarget; // Position cible lorsque le joueur est en dessous de la position B
    public float middleRotationX = 0f; // Rotation en X lorsque le joueur est entre les positions A et B
    public float rotationAX = 20f; // Rotation en X lorsque le joueur dépasse la position A
    public float rotationBX = -20f; // Rotation en X lorsque le joueur est en dessous de la position B

    private Vector3 targetPosition;
    private Quaternion targetRotation;

    void Update()
    {
        // Vérifie si le joueur dépasse la positionA
        if (player.position.y > positionA)
        {
            // Définit la nouvelle position cible pour la caméra à la positionATarget
            targetPosition = positionATarget;
            // Définit la nouvelle rotation cible pour la caméra
            targetRotation = Quaternion.Euler(rotationAX, transform.eulerAngles.y, transform.eulerAngles.z);
        }
        // Vérifie si le joueur est en dessous de la positionB
        else if (player.position.y < positionB)
        {
            // Définit la nouvelle position cible pour la caméra à la positionBTarget
            targetPosition = positionBTarget;
            // Définit la nouvelle rotation cible pour la caméra
            targetRotation = Quaternion.Euler(rotationBX, transform.eulerAngles.y, transform.eulerAngles.z);
        }
        else
        {
            // La caméra se dirige vers la position spécifiée pour le milieu
            targetPosition = middlePosition;
            targetRotation = Quaternion.Euler(middleRotationX, transform.eulerAngles.y, transform.eulerAngles.z);
        }

        // Déplace la caméra vers la position cible à une vitesse définie
        transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);
        // Change la rotation de la caméra vers la rotation cible à une vitesse définie
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, speed * Time.deltaTime);
    }
}
