using UnityEngine;
using UnityEngine.SceneManagement;

public class ReplayUI: MonoBehaviour
{
    // M�thode appel�e lors du clic sur le bouton
    public void OnShopButtonClicked()
    {
        // Charger la sc�ne "Shop"
        SceneManager.LoadScene("Scene");
    }
}



