using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour
{
    public float speed = 0f;
    public Text coinText; // Référence au texte UI
    private bool isCollected = false; // Variable pour vérifier si la pièce a déjà été collectée

    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (!isCollected && other.CompareTag("Player"))
        {
            isCollected = true; // Marquer la pièce comme collectée

            // Incrémentez le score global en utilisant le Game_Manager
            Game_Manager.Instance.AddCoins(5);
            UpdateCoinText();

            // Incrémente le compteur de pièces du joueur
            PlayerItems player = other.GetComponentInParent<PlayerItems>();
            if (player != null)
            {
                player.AddCoin();
            }

            Debug.Log("Coin collected, destroying gameObject..."); // Message de débogage

            // Détruit l'objet pièce
            Destroy(gameObject);
        }
    }

    void UpdateCoinText()
    {
        coinText.text = "Coins : " + Game_Manager.Instance.GetTotalCoins().ToString();
    }
}