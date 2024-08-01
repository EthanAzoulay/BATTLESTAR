using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float sensitivity = 1f; // Sensibilité du joystick
    public Joystick joystick;
    public GameObject shooterObject; // Le GameObject avec PlayerShooting

    private Rigidbody rb;

    // Variables pour contrôler la rotation du joueur
    public float maxRotationZ = 45f; // Angle maximum de rotation en Z
    public float maxRotationX = 45f; // Angle maximum de rotation en X
    public float rotationSpeed = 5f; // Vitesse de rotation

    private PlayerShooting playerShooting;

    // Expose joystick input values
    [HideInInspector]
    public float horizontalInput;
    [HideInInspector]
    public float verticalInput;

    [SerializeField]
    SOItemManager _itemManager;

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


        ReinitialisedUpgrades();
        ApplyWings();
        ApplyReactor();

    }

    void FixedUpdate()
    {
        // Joystick Entry
        if (joystick != null)
        {
            horizontalInput = joystick.Horizontal * sensitivity;
            verticalInput = joystick.Vertical * sensitivity;
        }


        Debug.Log("Horizontal Joystick: " + horizontalInput + ", Vertical Joystick: " + verticalInput);

        // Moving on Joystick entry
        Vector2 movement = new Vector2(horizontalInput, verticalInput) * speed;

        // Apply Velocity
        rb.velocity = movement;

        // Rotation en Z en fonction de l'entrée horizontale
        float targetRotationZ = -horizontalInput * maxRotationZ;
        float currentRotationZ = transform.eulerAngles.z;

        // Ajuster l'angle pour éviter des valeurs incorrectes
        if (currentRotationZ > 180)
        {
            currentRotationZ -= 360;
        }

        float newRotationZ = Mathf.Lerp(currentRotationZ, targetRotationZ, Time.deltaTime * rotationSpeed);

        // Rotation en X en fonction de l'entrée verticale
        float targetRotationX = verticalInput * maxRotationX;
        float currentRotationX = transform.eulerAngles.x;

        // Ajuster l'angle pour éviter des valeurs incorrectes
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
        Wings wings = _itemManager.Instance.wings;

        if (wings != null && wings.model != null)
        {
            GameObject wingsInstance = Instantiate(wings.model, transform);
            wingsInstance.transform.localPosition = wings.model.transform.localPosition;
            wingsInstance.transform.localRotation = wings.model.transform.localRotation;
            speed += wings.speed;
            rotationSpeed += wings.rotationSpeed;
        }
    }

    public void ApplyReactor()
    {
        Reactor reactor = _itemManager.Instance.reactor;


        if (reactor != null && reactor.model != null)
        {
            GameObject reactorInstance = Instantiate(reactor.model, transform);
            reactorInstance.transform.localPosition = reactor.model.transform.localPosition;
            reactorInstance.transform.localRotation = reactor.model.transform.localRotation;
            speed += reactor.speed;
            rotationSpeed += reactor.rotationSpeed;
        }
    }

    public void ReinitialisedUpgrades()
    {
        // Réinitialiser les valeurs de base avant d'appliquer les améliorations
        speed = 0f; // Valeur de base de la vitesse, ajustez cette valeur selon vos besoins
        rotationSpeed = 0f; // Valeur de base de la vitesse de rotation, ajustez cette valeur selon vos besoins


        // Appliquer l'amélioration de la cadence de tir si playerShooting est défini
        //if (playerShooting != null)
        //{
        //    playerShooting.fireRate = playerShooting.baseFireRate + reactor.fireRate;
        //}

    }


    public void ResetToDefault()
    {
        if (_itemManager != null)
        {
            // Réinitialiser les instances de l'item manager
            _itemManager.ResetInstance();

            //// Appliquer les valeurs par défaut
            //ReinitialisedUpgrades();
            //ApplyWings();
            //ApplyReactor();
        }
    }

}