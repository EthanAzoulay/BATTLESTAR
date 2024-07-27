using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public string[] scenes; // Tableau des noms des scènes à charger

    private int clickCount;

    void Start()
    {
        if (scenes.Length == 0)
        {
            Debug.LogError("Le tableau des scènes est vide. Veuillez ajouter des scènes.");
        }

        // Charger le clickCount sauvegardé
        clickCount = PlayerPrefs.GetInt("ClickCount", 0);
    }

    void OnMouseDown()
    {
        if (clickCount < scenes.Length)
        {
            // Charger la scène correspondante
            SceneManager.LoadScene(scenes[clickCount]);
        }
        else
        {
            Debug.Log("Toutes les scènes ont déjà été chargées.");
        }
    }

    public void IncrementClickCount()
    {
        clickCount++;
        PlayerPrefs.SetInt("ClickCount", clickCount);
        PlayerPrefs.Save();
    }
}
