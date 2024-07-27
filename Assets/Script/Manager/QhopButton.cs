using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QhopButton : MonoBehaviour
{
    // M�thode appel�e lors du clic sur le bouton
    public void OnShopButtonClicked()
    {
        // Charger la sc�ne "Shop"
        SceneManager.LoadScene("Shop");
    }
}
