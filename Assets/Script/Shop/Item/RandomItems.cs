using UnityEngine;
using System.Collections.Generic;

public class RandomItems : MonoBehaviour
{
    public GameObject[] items;
    public Vector3[] spawnPositions;
    public float[] spawnProbabilities;
    private int numberOfItemsToSpawn = 3;
    private Quaternion spawnRotation = Quaternion.Euler(90, 0, -180);

    void Start()
    {
        if (spawnProbabilities.Length != items.Length)
        {
            Debug.LogError("Le tableau des probabilités doit avoir la même longueur que le tableau des items.");
            return;
        }

        List<string> currentShopItems = Game_Manager.Instance.GetCurrentShopItems();

        if (currentShopItems.Count == 0 && Game_Manager.Instance.HasNewRoundStarted())
        {
            if (items.Length < numberOfItemsToSpawn || spawnPositions.Length < numberOfItemsToSpawn)
            {
                Debug.LogError("Le tableau d'items ou de positions ne contient pas assez d'éléments.");
                return;
            }

            List<int> usedIndices = new List<int>();
            int itemsSpawned = 0;
            int attempts = 0;

            while (itemsSpawned < numberOfItemsToSpawn && attempts < items.Length * 2)
            {
                int randomIndex = Random.Range(0, items.Length);
                float randomValue = Random.value;

                if (!usedIndices.Contains(randomIndex) &&
                    randomValue <= spawnProbabilities[randomIndex] &&
                    !Game_Manager.Instance.IsItemPurchased(items[randomIndex].name) &&
                    !Game_Manager.Instance.IsItemDestroyed(items[randomIndex].name))
                {
                    GameObject itemToSpawn = items[randomIndex];
                    Instantiate(itemToSpawn, spawnPositions[itemsSpawned], spawnRotation);
                    usedIndices.Add(randomIndex);
                    itemsSpawned++;
                    currentShopItems.Add(items[randomIndex].name);
                }

                attempts++;
            }

            if (itemsSpawned < numberOfItemsToSpawn)
            {
                Debug.LogWarning("Not enough non-purchased items to instantiate the required number of items.");
            }

            Game_Manager.Instance.SetCurrentShopItems(currentShopItems);
            Game_Manager.Instance.SetNewRoundStarted(false);
        }
        else
        {
            for (int i = 0; i < currentShopItems.Count; i++)
            {
                foreach (GameObject item in items)
                {
                    if (item.name == currentShopItems[i] && !Game_Manager.Instance.IsItemDestroyed(item.name))
                    {
                        Instantiate(item, spawnPositions[i], spawnRotation);
                        break;
                    }
                }
            }
        }
    }
}
