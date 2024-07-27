using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;  // R�f�rence au joueur
    public float positionA = 5f;  // Position Y � partir de laquelle la cam�ra monte
    public float positionB = -5f; // Position Y � partir de laquelle la cam�ra descend
    public float speed = 2f;      // Vitesse de d�placement de la cam�ra
    public Vector3 middlePosition; // Position cible lorsque le joueur est entre les positions A et B
    public Vector3 positionATarget; // Position cible lorsque le joueur d�passe la position A
    public Vector3 positionBTarget; // Position cible lorsque le joueur est en dessous de la position B
    public float middleRotationX = 0f; // Rotation en X lorsque le joueur est entre les positions A et B
    public float rotationAX = 20f; // Rotation en X lorsque le joueur d�passe la position A
    public float rotationBX = -20f; // Rotation en X lorsque le joueur est en dessous de la position B

    private Vector3 targetPosition;
    private Quaternion targetRotation;

    void Update()
    {
        // V�rifie si le joueur d�passe la positionA
        if (player.position.y > positionA)
        {
            // D�finit la nouvelle position cible pour la cam�ra � la positionATarget
            targetPosition = positionATarget;
            // D�finit la nouvelle rotation cible pour la cam�ra
            targetRotation = Quaternion.Euler(rotationAX, transform.eulerAngles.y, transform.eulerAngles.z);
        }
        // V�rifie si le joueur est en dessous de la positionB
        else if (player.position.y < positionB)
        {
            // D�finit la nouvelle position cible pour la cam�ra � la positionBTarget
            targetPosition = positionBTarget;
            // D�finit la nouvelle rotation cible pour la cam�ra
            targetRotation = Quaternion.Euler(rotationBX, transform.eulerAngles.y, transform.eulerAngles.z);
        }
        else
        {
            // La cam�ra se dirige vers la position sp�cifi�e pour le milieu
            targetPosition = middlePosition;
            targetRotation = Quaternion.Euler(middleRotationX, transform.eulerAngles.y, transform.eulerAngles.z);
        }

        // D�place la cam�ra vers la position cible � une vitesse d�finie
        transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);
        // Change la rotation de la cam�ra vers la rotation cible � une vitesse d�finie
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, speed * Time.deltaTime);
    }
}
