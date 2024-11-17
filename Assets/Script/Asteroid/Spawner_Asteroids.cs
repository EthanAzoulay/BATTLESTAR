using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public struct AsteroidPrefab
{
    public GameObject prefab;
    public float spawnProbability; // Probabilité d'apparition
}

public class Spawner_Asteroids : MonoBehaviour
{
    // Tableau d'objets à instancier avec leurs probabilités d'apparition
    public AsteroidPrefab[] asteroidPrefabs;

    // Cadence d'instanciation (en secondes)
    public float spawnRate;

    // Plage de positions aléatoires en X et Y
    public Vector2 xRange = new Vector2(-10, 10);
    public Vector2 yRange = new Vector2(-5, 5);

    // Plage de positions aléatoires en X et Y pour les objets "Portal"
    public Vector2 portalXRange = new Vector2(-15, 15);
    public Vector2 portalYRange = new Vector2(-10, 10);

    // Timer pour gérer la cadence d'instanciation
    private float timer = 0f;

    void Start()
    {
        // Initialisation de spawnRate avec une valeur aléatoire entre 0.1 et 0.5
        //spawnRate = Random.Range(0.1f, 0.5f);

        // Appliquez la probabilité de spawn augmentée
        //float increaseAmount = Game_Manager.Instance.GetAsteroidSpawnProbabilityIncrease();
        //if (increaseAmount > 0)
        //{
        //    IncreaseSpawnProbability(2, increaseAmount);
        //}
    }

    void Update()
    {
        // Mise à jour du timer
        timer += Time.deltaTime;

        // Vérification si le timer a atteint la cadence d'instanciation
        if (timer >= spawnRate)
        {
            // Instanciation de l'astéroïde
            SpawnAsteroid();

            // Réinitialisation du timer
            timer = 0f;
        }
    }

    void SpawnAsteroid()
    {
        // Sélection d'un prefab en fonction de sa probabilité
        GameObject selectedPrefab = SelectAsteroidPrefab();

        // Instanciation de l'astéroïde
        if (selectedPrefab != null)
        {
            Vector3 spawnPosition;

            // Génération de positions aléatoires en fonction du tag
            if (selectedPrefab.tag == "Portal")
            {
                float randomX = Random.Range(portalXRange.x, portalXRange.y);
                float randomY = Random.Range(portalYRange.x, portalYRange.y);
                spawnPosition = new Vector3(randomX, randomY, transform.position.z);
            }
            else
            {
                float randomX = Random.Range(xRange.x, xRange.y);
                float randomY = Random.Range(yRange.x, yRange.y);
                spawnPosition = new Vector3(randomX, randomY, transform.position.z);
            }

            Quaternion spawnRotation = selectedPrefab.tag == "Portal" ? selectedPrefab.transform.rotation : Quaternion.identity;
            Instantiate(selectedPrefab, spawnPosition, spawnRotation);
        }
    }

    GameObject SelectAsteroidPrefab()
    {
        float totalProbability = 0f;
        foreach (var asteroid in asteroidPrefabs)
        {
            totalProbability += asteroid.spawnProbability;
        }

        float randomPoint = Random.value * totalProbability;

        foreach (var asteroid in asteroidPrefabs)
        {
            if (randomPoint < asteroid.spawnProbability)
            {
                return asteroid.prefab;
            }
            else
            {
                randomPoint -= asteroid.spawnProbability;
            }
        }

        return null; // Par sécurité, en cas de problème
    }

    public void IncreaseSpawnProbability(int index, float amount)
    {
        if (index >= 0 && index < asteroidPrefabs.Length)
        {
            asteroidPrefabs[index].spawnProbability += amount;
            Debug.Log("Spawn probability increased for index " + index + " by " + amount);
        }
        else
        {
            Debug.LogWarning("Invalid index for increasing spawn probability: " + index);
        }
    }
}
