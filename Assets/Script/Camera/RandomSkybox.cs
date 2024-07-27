using UnityEngine;

public class RandomSkybox : MonoBehaviour
{
    // Tableau pour stocker les skyboxes
    public Material[] skyboxes;

    void Start()
    {
        // V�rifie s'il y a des skyboxes dans le tableau
        if (skyboxes.Length > 0)
        {
            // S�lectionne une skybox al�atoire
            int randomIndex = Random.Range(0, skyboxes.Length);
            Material selectedSkybox = skyboxes[randomIndex];

            // Applique la skybox s�lectionn�e
            RenderSettings.skybox = selectedSkybox;
            DynamicGI.UpdateEnvironment();
        }
        else
        {
            Debug.LogWarning("Le tableau des skyboxes est vide!");
        }
    }
}
