using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public int health = 0;
    public float rotationSpeed = 50f;
    public float minScale = 0f;
    public float maxScale = 0f;
    public int coinsReward = 1; // Nombre de pi�ces gagn�es lorsque l'ast�ro�de est d�truit

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
            TakeDamage(1); // Applique un d�g�t fixe au joueur
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Bullet"))
        {
            Bullet_Moving bullet = other.gameObject.GetComponent<Bullet_Moving>();
            if (bullet != null)
            {
                TakeDamage(bullet.damage, true); // Applique les d�g�ts de la balle � l'ast�ro�de et indique que c'est une balle
            }
            Destroy(other.gameObject);
        }
    }

    public void TakeDamage(int damage, bool isBullet = false)
    {
        health -= damage;

        if (health <= 0)
        {
            // Ajouter des pi�ces au joueur lorsque l'ast�ro�de est d�truit
            Game_Manager.Instance.AddCoins(coinsReward);

            // Incr�mente le compteur uniquement si d�truit par une balle
            if (isBullet)
            {
                Game_Manager.Instance.IncrementDestroyedAsteroids();
            }

            // D�truire le parent si l'ast�ro�de est d�truit
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }

            Destroy(gameObject);  // D�truit l'ast�ro�de quand la sant� atteint 0
        }
    }
}
