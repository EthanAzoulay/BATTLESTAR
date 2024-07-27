using UnityEngine;
using System.Collections.Generic;

public class Game_Manager : MonoBehaviour
{
    public static Game_Manager Instance { get; private set; }

    private int totalCoins;
    private int destroyedAsteroids = 0;
    private Dictionary<string, int> purchasedItems = new Dictionary<string, int>();
    public Asteroid_Moving asteroidPrefab;
    private bool newRoundStarted = true;
    private List<string> currentShopItems = new List<string>();
    private List<string> destroyedItems = new List<string>();

    private const string AsteroidSpawnProbabilityKey = "AsteroidSpawnProbability";
    private float asteroidSpawnProbabilityIncrease = 0f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadCoins();
            LoadPurchasedItems();
            LoadDestroyedItems();
            LoadDestroyedAsteroids();
            LoadAsteroidSpawnProbabilityIncrease();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void LoadAsteroidSpawnProbabilityIncrease()
    {
        asteroidSpawnProbabilityIncrease = PlayerPrefs.GetFloat(AsteroidSpawnProbabilityKey, 0f);
    }

    private void SaveAsteroidSpawnProbabilityIncrease()
    {
        PlayerPrefs.SetFloat(AsteroidSpawnProbabilityKey, asteroidSpawnProbabilityIncrease);
        PlayerPrefs.Save();
    }

    public void IncreaseAsteroidSpawnProbability(float amount)
    {
        asteroidSpawnProbabilityIncrease += amount;
        SaveAsteroidSpawnProbabilityIncrease();
    }

    public float GetAsteroidSpawnProbabilityIncrease()
    {
        return asteroidSpawnProbabilityIncrease;
    }

    public void ResetAsteroidSpawnProbabilityIncrease()
    {
        asteroidSpawnProbabilityIncrease = 0f;
        SaveAsteroidSpawnProbabilityIncrease();
    }

    public void AddCoins(int amount)
    {
        totalCoins += amount;
        SaveCoins();
        Debug.Log("Coins added: " + amount + ", Total Coins: " + totalCoins);
    }

    public int GetTotalCoins()
    {
        return totalCoins;
    }

    public void ResetCoins()
    {
        totalCoins = 0;
        SaveCoins();
        Debug.Log("Coins reset to 0");
    }

    private void SaveCoins()
    {
        PlayerPrefs.SetInt("TotalCoins", totalCoins);
        PlayerPrefs.Save();
    }

    private void LoadCoins()
    {
        totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);
    }

    public void AddPurchasedItem(string itemName)
    {
        if (purchasedItems.ContainsKey(itemName))
        {
            purchasedItems[itemName]++;
        }
        else
        {
            purchasedItems[itemName] = 1;
        }
        SavePurchasedItems();
        Debug.Log("Purchased item: " + itemName + ", Total purchased: " + purchasedItems[itemName]);
    }

    public bool IsItemPurchased(string itemName)
    {
        return purchasedItems.ContainsKey(itemName);
    }

    public int GetPurchasedItemCount(string itemName)
    {
        return purchasedItems.ContainsKey(itemName) ? purchasedItems[itemName] : 0;
    }

    public void ResetPurchasedItems()
    {
        purchasedItems.Clear();
        SavePurchasedItems();
        Bullet_Moving.defaultDamage = 1;
        Debug.Log("Purchased items reset");
    }

    private void SavePurchasedItems()
    {
        List<string> keys = new List<string>(purchasedItems.Keys);
        PlayerPrefs.SetString("PurchasedItems", string.Join(",", keys));

        foreach (var item in purchasedItems)
        {
            PlayerPrefs.SetInt("PurchasedItem_" + item.Key, item.Value);
        }
        PlayerPrefs.Save();
    }

    private void LoadPurchasedItems()
    {
        purchasedItems.Clear();
        string savedItems = PlayerPrefs.GetString("PurchasedItems", "");
        if (!string.IsNullOrEmpty(savedItems))
        {
            foreach (string key in savedItems.Split(','))
            {
                if (!string.IsNullOrEmpty(key))
                {
                    purchasedItems[key] = PlayerPrefs.GetInt("PurchasedItem_" + key, 0);
                    Debug.Log($"Loaded item {key} with count {purchasedItems[key]}");
                }
            }
        }
    }

    public void ResetGame()
    {
        ResetCoins();
        ResetPurchasedItems();
        ResetDestroyedItems();
        ResetAsteroidSpawnProbabilityIncrease();
        SetCurrentShopItems(new List<string>());
        newRoundStarted = true;

        if (asteroidPrefab != null)
        {
            asteroidPrefab.ResetSpeed();
        }

        // Trouver le Player et réinitialiser ses ScriptableObjects
        PlayerMovement player = FindObjectOfType<PlayerMovement>();
        if (player != null)
        {
            player.ResetToDefault();
        }
    }

    public bool HasNewRoundStarted()
    {
        return newRoundStarted;
    }

    public void SetNewRoundStarted(bool started)
    {
        newRoundStarted = started;
    }

    public List<string> GetCurrentShopItems()
    {
        return currentShopItems;
    }

    public void SetCurrentShopItems(List<string> items)
    {
        currentShopItems = items;
    }

    public void IncrementDestroyedAsteroids()
    {
        destroyedAsteroids++;
        SaveDestroyedAsteroids();
        Debug.Log("Asteroids destroyed: " + destroyedAsteroids);
    }

    public int GetDestroyedAsteroidsCount()
    {
        return destroyedAsteroids;
    }

    private void SaveDestroyedItems()
    {
        PlayerPrefs.SetString("DestroyedItems", string.Join(",", destroyedItems));
        PlayerPrefs.Save();
    }

    private void LoadDestroyedItems()
    {
        destroyedItems.Clear();
        string savedItems = PlayerPrefs.GetString("DestroyedItems", "");
        if (!string.IsNullOrEmpty(savedItems))
        {
            foreach (string item in savedItems.Split(','))
            {
                if (!string.IsNullOrEmpty(item))
                {
                    destroyedItems.Add(item);
                }
            }
        }
    }

    public void AddDestroyedItem(string itemName)
    {
        if (!destroyedItems.Contains(itemName))
        {
            destroyedItems.Add(itemName);
            SaveDestroyedItems();
        }
    }

    public bool IsItemDestroyed(string itemName)
    {
        return destroyedItems.Contains(itemName);
    }

    public void ResetDestroyedItems()
    {
        destroyedItems.Clear();
        SaveDestroyedItems();
        Debug.Log("Destroyed items reset");
    }

    public void ResetDestroyedAsteroids()
    {
        destroyedAsteroids = 0;
        SaveDestroyedAsteroids();
        Debug.Log("Destroyed asteroids reset to 0");
    }

    private void SaveDestroyedAsteroids()
    {
        PlayerPrefs.SetInt("DestroyedAsteroids", destroyedAsteroids);
        PlayerPrefs.Save();
    }

    private void LoadDestroyedAsteroids()
    {
        destroyedAsteroids = PlayerPrefs.GetInt("DestroyedAsteroids", 0);
    }
}
