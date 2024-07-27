using UnityEngine;
using UnityEngine.SceneManagement;

public class ReplayUI: MonoBehaviour
{
    // Méthode appelée lors du clic sur le bouton
    public void OnShopButtonClicked()
    {
        // Charger la scène "Shop"
        SceneManager.LoadScene("Scene");
    }
}



