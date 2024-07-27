using UnityEngine;
using UnityEngine.SceneManagement;

public class Replay : MonoBehaviour
{
    // Le nom de la scène à charger
    public string sceneName = "Scene";

    // Méthode appelée lorsqu'un clic est détecté sur le Collider
    void OnMouseDown()
    {
        // Charger la scène spécifiée
        SceneManager.LoadScene(sceneName);
    }


   
}
