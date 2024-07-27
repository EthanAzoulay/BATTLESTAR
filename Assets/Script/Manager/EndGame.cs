using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    public float minSpeed = 1f; // La vitesse minimale
    public float maxSpeed = 5f; // La vitesse maximale
    private float speed;

    private void OnEnable()
    {
        // Générer une vitesse aléatoire entre minSpeed et maxSpeed chaque fois que l'objet est activé
        speed = Random.Range(minSpeed, maxSpeed);
    }

    private void Update()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime);
    }

    // Assure-toi que le player a bien le tag "Player"
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            SceneManager.LoadScene("Scene");
        }
    }
}
