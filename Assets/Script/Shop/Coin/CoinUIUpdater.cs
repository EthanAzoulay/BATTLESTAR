using UnityEngine;
using UnityEngine.UI;

public class CoinUIUpdater : MonoBehaviour
{
    public Text coinText; // Référence au texte UI

    void Start()
    {
        UpdateCoinText();
    }

    public void UpdateCoinText()
    {
        coinText.text = " " + Game_Manager.Instance.GetTotalCoins().ToString();
    }

    // Si vous voulez mettre à jour le texte à chaque frame (ce n'est pas optimal)
    void Update()
    {
        UpdateCoinText();
    }
}
