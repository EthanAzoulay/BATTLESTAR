using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QhopButton : MonoBehaviour
{
    // Méthode appelée lors du clic sur le bouton
    public void OnShopButtonClicked()
    {
        // Charger la scène "Shop"
        SceneManager.LoadScene("Shop");
    }
}
