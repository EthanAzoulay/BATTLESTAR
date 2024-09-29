using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public int health = 0;
    public float rotationSpeed = 50f;
    public float minScale = 0f;
    public float maxScale = 0f;
    public int coinsReward = 1; // Nombre de pièces gagnées lorsque l'astéroïde est détruit
    public GameObject explosionParticles; // L'objet à instancier lors de la destruction de l'astéroïde
    public GameObject vfx; // Les particules d'explosion à activer lors de la destruction
    public float destroyDelay = 0.01f; // Temps avant la destruction du GameObject instancié

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
            Game_Manager.Instance.ResetDestroyedAsteroids(); // Réinitialise le compteur d'astéroïdes détruits
            Game_Manager.Instance.ResetGame(); // Réinitialise les pièces et les items

            // Ajouter ici le code pour gérer la réinitialisation de la scène ou des éléments visuels
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Bullet"))
        {
            // Instancie un nouvel objet à la position actuelle de l'astéroïde
            GameObject instantiatedObject = Instantiate(explosionParticles, transform.position, transform.rotation);

            // Détruit cet objet après 5 secondes
            Destroy(instantiatedObject, destroyDelay);

            // Instancie les particules d'explosion à la position de l'astéroïde
            GameObject explosion = Instantiate(vfx, transform.position, transform.rotation);

            // Détruit les particules après 5 secondes
            Destroy(explosion, destroyDelay);

            // Ajouter des pièces au joueur lorsque l'astéroïde est détruit
            Game_Manager.Instance.AddCoins(coinsReward);

            // Incrémente le compteur d'astéroïdes détruits
            Game_Manager.Instance.IncrementDestroyedAsteroids();

            // Détruit la balle
            Destroy(other.gameObject);

            // Détruit l'astéroïde
            Destroy(gameObject);
        }
    }
}
