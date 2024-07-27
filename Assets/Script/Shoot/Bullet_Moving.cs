using UnityEngine;
public class Bullet_Moving : MonoBehaviour
{
    public static int defaultDamage = 1; // D�g�ts par d�faut des balles
    public float speed = 10f; // Ajustez la vitesse selon vos besoins
    public int damage; // D�g�ts inflig�s par la balle

    private Rigidbody rb;
    private Transform target;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        damage = defaultDamage;
        // Initialisez les d�g�ts avec la valeur par d�faut
    }

    public void SetTarget(Transform targetTransform)
    {
        target = targetTransform;
    }

    private void FixedUpdate()
    {
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            rb.velocity = direction * speed;
        }
        else
        {
            rb.velocity = transform.forward * speed; // Direction par d�faut : vers l'avant
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Asteroid"))
        {
            Asteroid asteroid = other.gameObject.GetComponent<Asteroid>();
            if (asteroid != null)
            {
                //asteroid.TakeDamage(damage); 
                // Applique les d�g�ts � l'ast�ro�de
                Destroy(gameObject);

            }
        }
    }
}
