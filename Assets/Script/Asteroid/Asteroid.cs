using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public int health = 0;
    public float rotationSpeed = 50f;
    public float minScale = 0f;
    public float maxScale = 0f;
    public int coinsReward = 1; // Nombre de pièces gagnées lorsque l'astéroïde est détruit

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
            TakeDamage(1); // Applique un dégât fixe au joueur
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Bullet"))
        {
            Bullet_Moving bullet = other.gameObject.GetComponent<Bullet_Moving>();
            if (bullet != null)
            {
                TakeDamage(bullet.damage, true); // Applique les dégâts de la balle à l'astéroïde et indique que c'est une balle
            }
            Destroy(other.gameObject);
        }
    }

    public void TakeDamage(int damage, bool isBullet = false)
    {
        health -= damage;

        if (health <= 0)
        {
            // Ajouter des pièces au joueur lorsque l'astéroïde est détruit
            Game_Manager.Instance.AddCoins(coinsReward);

            // Incrémente le compteur uniquement si détruit par une balle
            if (isBullet)
            {
                Game_Manager.Instance.IncrementDestroyedAsteroids();
            }

            // Détruire le parent si l'astéroïde est détruit
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }

            Destroy(gameObject);  // Détruit l'astéroïde quand la santé atteint 0
        }
    }
}
