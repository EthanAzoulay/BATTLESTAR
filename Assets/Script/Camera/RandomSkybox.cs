using UnityEngine;

public class RandomSkybox : MonoBehaviour
{
    // Tableau pour stocker les skyboxes
    public Material[] skyboxes;

    void Start()
    {
        // Vérifie s'il y a des skyboxes dans le tableau
        if (skyboxes.Length > 0)
        {
            // Sélectionne une skybox aléatoire
            int randomIndex = Random.Range(0, skyboxes.Length);
            Material selectedSkybox = skyboxes[randomIndex];

            // Applique la skybox sélectionnée
            RenderSettings.skybox = selectedSkybox;
            DynamicGI.UpdateEnvironment();
        }
        else
        {
            Debug.LogWarning("Le tableau des skyboxes est vide!");
        }
    }
}
