using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public int health = 0;
    public float rotationSpeed = 50f;
    public float minScale = 0f;
    public float maxScale = 0f;
    public int coinsReward = 1; // Nombre de pi�ces gagn�es lorsque l'ast�ro�de est d�truit
    public GameObject explosionParticles; // L'objet � instancier lors de la destruction de l'ast�ro�de
    public GameObject vfx; // Les particules d'explosion � activer lors de la destruction
    public float destroyDelay = 0.01f; // Temps avant la destruction du GameObject instanci�

    private void Start()
    {
        // Scale
        float randomScale = Random.Range(minScale, maxScale);
        transform.localScale = new Vector3(randomScale, randomScale, randomScale);
    }

    void Update()
    {
        // Rotation
        transform.Rotate(Vector3.right * rotationSpeed * Time.deltaTime);
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Game_Manager.Instance.ResetDestroyedAsteroids(); // R�initialise le compteur d'ast�ro�des d�truits
            Game_Manager.Instance.ResetGame(); // R�initialise les pi�ces et les items

            // Ajouter ici le code pour g�rer la r�initialisation de la sc�ne ou des �l�ments visuels
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Bullet"))
        {
            // Instancie un nouvel objet � la position actuelle de l'ast�ro�de
            GameObject instantiatedObject = Instantiate(explosionParticles, transform.position, transform.rotation);

            // D�truit cet objet apr�s 5 secondes
            Destroy(instantiatedObject, destroyDelay);

            // Instancie les particules d'explosion � la position de l'ast�ro�de
            GameObject explosion = Instantiate(vfx, transform.position, transform.rotation);

            // D�truit les particules apr�s 5 secondes
            Destroy(explosion, destroyDelay);

            // Ajouter des pi�ces au joueur lorsque l'ast�ro�de est d�truit
            Game_Manager.Instance.AddCoins(coinsReward);

            // Incr�mente le compteur d'ast�ro�des d�truits
            Game_Manager.Instance.IncrementDestroyedAsteroids();

            // D�truit la balle
            Destroy(other.gameObject);

            // D�truit l'ast�ro�de
            Destroy(gameObject);
        }
    }
}
