using UnityEngine;
using UnityEngine.SceneManagement;

public class Replay : MonoBehaviour
{
    // Le nom de la sc�ne � charger
    public string sceneName = "Scene";

    // M�thode appel�e lorsqu'un clic est d�tect� sur le Collider
    void OnMouseDown()
    {
        // Charger la sc�ne sp�cifi�e
        SceneManager.LoadScene(sceneName);
    }


   
}
