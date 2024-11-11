using UnityEngine;

public class CardRotation : MonoBehaviour
{
    private Vector2 touchStartPos; // Position initiale du toucher
    private float rotationSpeed = 0.2f; // Vitesse de rotation ajustable

    // Limites de rotation (param�trables depuis l'inspecteur Unity)
    [Header("Rotation Limits")]
    public float minRotationX = -30f;
    public float maxRotationX = 30f;
    public float minRotationY = -45f;
    public float maxRotationY = 45f;

    private Vector3 currentRotation; // Stocke la rotation actuelle
    private bool isRotating = false;

    void Start()
    {
        // Initialise la rotation actuelle � la rotation initiale de l'objet
        currentRotation = transform.eulerAngles;
    }

    void Update()
    {
        // V�rifie s'il y a un toucher sur l'�cran
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                // Enregistre la position initiale du toucher
                touchStartPos = touch.position;
                isRotating = true;
            }
            else if (touch.phase == TouchPhase.Moved && isRotating)
            {
                // Calcule la diff�rence de mouvement sur les axes X et Y
                Vector2 touchCurrentPos = touch.position;
                Vector2 direction = touchCurrentPos - touchStartPos;

                // Calcule la rotation demand�e sur chaque axe
                float rotationAmountY = direction.x * rotationSpeed;
                float rotationAmountX = direction.y * rotationSpeed;

                // Applique la rotation tout en respectant les limites
                currentRotation.y = Mathf.Clamp(currentRotation.y - rotationAmountY, minRotationY, maxRotationY);
                currentRotation.x = Mathf.Clamp(currentRotation.x + rotationAmountX, minRotationX, maxRotationX);

                // Applique la rotation au transform
                transform.eulerAngles = currentRotation;

                // Met � jour la position de d�part pour un mouvement fluide
                touchStartPos = touchCurrentPos;
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                // Arr�te la rotation quand le toucher se termine
                isRotating = false;
            }
        }
    }
}
