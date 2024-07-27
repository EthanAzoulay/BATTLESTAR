using UnityEngine;
using UnityEngine.UI;

public class CoinUIUpdater : MonoBehaviour
{
    public Text coinText; // R�f�rence au texte UI

    void Start()
    {
        UpdateCoinText();
    }

    public void UpdateCoinText()
    {
        coinText.text = "Coins : " + Game_Manager.Instance.GetTotalCoins().ToString();
    }

    // Si vous voulez mettre � jour le texte � chaque frame (ce n'est pas optimal)
    void Update()
    {
        UpdateCoinText();
    }
}
