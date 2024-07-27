using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour
{
    public float speed = 0f;
    public Text coinText; // R�f�rence au texte UI
    private bool isCollected = false; // Variable pour v�rifier si la pi�ce a d�j� �t� collect�e

    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (!isCollected && other.CompareTag("Player"))
        {
            isCollected = true; // Marquer la pi�ce comme collect�e

            // Incr�mentez le score global en utilisant le Game_Manager
            Game_Manager.Instance.AddCoins(5);
            UpdateCoinText();

            // Incr�mente le compteur de pi�ces du joueur
            PlayerItems player = other.GetComponentInParent<PlayerItems>();
            if (player != null)
            {
                player.AddCoin();
            }

            Debug.Log("Coin collected, destroying gameObject..."); // Message de d�bogage

            // D�truit l'objet pi�ce
            Destroy(gameObject);
        }
    }

    void UpdateCoinText()
    {
        coinText.text = "Coins : " + Game_Manager.Instance.GetTotalCoins().ToString();
    }
}