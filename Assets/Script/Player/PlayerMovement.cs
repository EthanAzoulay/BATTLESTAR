using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float sensitivity = 1f; // Sensibilit� du joystick
    public Joystick joystick;
    public GameObject shooterObject; // Le GameObject avec PlayerShooting

    private Rigidbody rb;

    // Variables pour contr�ler la rotation du joueur
    public float maxRotationZ = 45f; // Angle maximum de rotation en Z
    public float maxRotationX = 45f; // Angle maximum de rotation en X
    public float rotationSpeed = 5f; // Vitesse de rotation

    private PlayerShooting playerShooting;

    // Expose joystick input values
    [HideInInspector]
    public float horizontalInput;
    [HideInInspector]
    public float verticalInput;

    // R�f�rence au ScriptableObject Wings
    public Wings wings;

    // R�f�rence au ScriptableObject Reactor
    public Reactor reactor;

    //// R�f�rences aux valeurs par d�faut des ScriptableObjects
    private Wings defaultWings;
    private Reactor defaultReactor;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (shooterObject != null)
        {
            playerShooting = shooterObject.GetComponent<PlayerShooting>();

            if (playerShooting == null)
            {
                Debug.LogError("PlayerShooting script is not attached to the shooterObject.");
            }
        }
        else
        {
            Debug.LogError("Shooter object is not assigned.");
        }

        //// Stocker les valeurs par d�faut des ScriptableObjects
        defaultWings = wings;
        defaultReactor = reactor;

        ApplyWings();
        ApplyReactor();
        ApplyUpgrades();
    }

    void FixedUpdate()
    {
        // Joystick Entry
        horizontalInput = joystick.Horizontal * sensitivity;
        verticalInput = joystick.Vertical * sensitivity;

        Debug.Log("Horizontal Joystick: " + horizontalInput + ", Vertical Joystick: " + verticalInput);

        // Moving on Joystick entry
        Vector2 movement = new Vector2(horizontalInput, verticalInput) * speed;

        // Apply Velocity
        rb.velocity = movement;

        // Rotation en Z en fonction de l'entr�e horizontale
        float targetRotationZ = -horizontalInput * maxRotationZ;
        float currentRotationZ = transform.eulerAngles.z;

        // Ajuster l'angle pour �viter des valeurs incorrectes
        if (currentRotationZ > 180)
        {
            currentRotationZ -= 360;
        }

        float newRotationZ = Mathf.Lerp(currentRotationZ, targetRotationZ, Time.deltaTime * rotationSpeed);

        // Rotation en X en fonction de l'entr�e verticale
        float targetRotationX = verticalInput * maxRotationX;
        float currentRotationX = transform.eulerAngles.x;

        // Ajuster l'angle pour �viter des valeurs incorrectes
        if (currentRotationX > 180)
        {
            currentRotationX -= 360;
        }

        float newRotationX = Mathf.Lerp(currentRotationX, targetRotationX, Time.deltaTime * rotationSpeed);

        // Appliquer les rotations en X et Z
        transform.rotation = Quaternion.Euler(newRotationX, transform.eulerAngles.y, newRotationZ);

        // Transmettre les inputs au PlayerShooting
        if (playerShooting != null)
        {
            playerShooting.horizontalInput = horizontalInput;
            playerShooting.verticalInput = verticalInput;
        }
    }

    public void AimAt(Vector3? targetPosition)
    {
        if (playerShooting != null)
        {
            playerShooting.AimAt(targetPosition);
        }
    }

    public void SetTargetAsteroid(Transform asteroidTransform)
    {
        if (playerShooting != null)
        {
            playerShooting.SetTargetAsteroid(asteroidTransform);
        }
    }

    public void ApplyWings()
    {
        if (wings != null && wings.model != null)
        {
            GameObject wingsInstance = Instantiate(wings.model, transform);
            wingsInstance.transform.localPosition = wings.model.transform.localPosition;
            wingsInstance.transform.localRotation = wings.model.transform.localRotation;
        }
    }

    public void ApplyReactor()
    {
        if (reactor != null && reactor.model != null)
        {
            GameObject reactorInstance = Instantiate(reactor.model, transform);
            reactorInstance.transform.localPosition = reactor.model.transform.localPosition;
            reactorInstance.transform.localRotation = reactor.model.transform.localRotation;
        }
    }

    public void ApplyUpgrades()
    {
        // R�initialiser les valeurs de base avant d'appliquer les am�liorations
        speed = 0f; // Valeur de base de la vitesse, ajustez cette valeur selon vos besoins
        rotationSpeed = 0f; // Valeur de base de la vitesse de rotation, ajustez cette valeur selon vos besoins

        // Appliquer les am�liorations des wings
        if (wings != null)
        {
            speed += wings.speed;
            rotationSpeed += wings.rotationSpeed;
        }

        // Appliquer les am�liorations du reactor
        if (reactor != null)
        {
            speed += reactor.speed;
            rotationSpeed += reactor.rotationSpeed;

            // Appliquer l'am�lioration de la cadence de tir si playerShooting est d�fini
            //if (playerShooting != null)
            //{
            //    playerShooting.fireRate = playerShooting.baseFireRate + reactor.fireRate;
            //}
        }
    }


    //M�thode pour r�initialiser les ScriptableObjects aux valeurs par d�faut
    public void ResetToDefault()
    {
        wings = defaultWings;
        reactor = defaultReactor;

        // R�appliquer les valeurs par d�faut
        ApplyWings();
        ApplyReactor();
        ApplyUpgrades();
    }
}
