using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public string[] scenes; // Tableau des noms des sc�nes � charger

    private int clickCount;

    void Start()
    {
        if (scenes.Length == 0)
        {
            Debug.LogError("Le tableau des sc�nes est vide. Veuillez ajouter des sc�nes.");
        }

        // Charger le clickCount sauvegard�
        clickCount = PlayerPrefs.GetInt("ClickCount", 0);
    }

    void OnMouseDown()
    {
        if (clickCount < scenes.Length)
        {
            // Charger la sc�ne correspondante
            SceneManager.LoadScene(scenes[clickCount]);
        }
        else
        {
            Debug.Log("Toutes les sc�nes ont d�j� �t� charg�es.");
        }
    }

    public void IncrementClickCount()
    {
        clickCount++;
        PlayerPrefs.SetInt("ClickCount", clickCount);
        PlayerPrefs.Save();
    }
}
