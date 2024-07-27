using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public Text coinsText;
    public PlayerItems player;

    void Start()
    {
        UpdateCoinsUI();
        InitializeShopItems();
    }

    void UpdateCoinsUI()
    {
        if (coinsText != null)
        {
            coinsText.text = "Coins : " + Game_Manager.Instance.GetTotalCoins().ToString();
        }
        Debug.Log("Coins UI updated: " + coinsText.text);
    }

    public void PurchaseItem(string itemName, int itemCost)
    {
        int totalCoins = Game_Manager.Instance.GetTotalCoins();
        Debug.Log("Attempting to purchase item: " + itemName + " for cost: " + itemCost);

        if (totalCoins >= itemCost)
        {
            Game_Manager.Instance.AddCoins(-itemCost);
            Game_Manager.Instance.AddPurchasedItem(itemName);
            Game_Manager.Instance.AddDestroyedItem(itemName);
            UpdateCoinsUI();
            Debug.Log("Purchase successful!");
            //ApplyItemEffects(itemName);
        }
        else
        {
            Debug.Log("Not enough coins!");
            DisableButtonCColliders();
        }
    }

    void InitializeShopItems()
    {
        ShopItem[] shopItems = FindObjectsOfType<ShopItem>();
        foreach (ShopItem shopItem in shopItems)
        {
            if (shopItem != null)
            {
                Debug.Log("Shop item initialized: " + shopItem.itemName);
            }
            else
            {
                Debug.LogWarning("ShopItem reference is missing!");
            }
        }
    }

    //void ApplyItemEffects(string itemName)
    //{
    //    switch (itemName)
    //    {
    //        case "Cadence":
    //            player.IncreaseFireRate(2);
    //            break;
    //        case "Puissance":
    //            player.IncreaseBulletDamage(1);
    //            break;

    //        case "Rotation":
    //            player.IncreaseRotationSpeed(5f);
    //            break;
    //        case "Rotation 2":
    //            player.IncreaseRotationSpeed(5f);
    //            break;
    //        case "Rotation 3":
    //            player.IncreaseRotationSpeed(5f);
    //            break;
    //        case "Rotation 4":
    //            player.IncreaseRotationSpeed(5f);
    //            break;

    //        case "Vitesse":
    //            player.IncreaseSpeed(5f);
    //            break;
    //        case "Vitesse 2":
    //            player.IncreaseSpeed(5f);
    //            break;
    //        case "Vitesse 3":
    //            player.IncreaseSpeed(5f);
    //            break;
    //        case "Vitesse 4":
    //            player.IncreaseSpeed(5f);
    //            break;

    //        case "Shield":
    //            player.DurationShield();
    //            break;
    //        case "Cannon":
    //            player.AddCannon();
    //            break;
    //        case "Cannon2":
    //            player.AddCannon2();
    //            break;

    //        // Ajoutez ce case pour l'article spécifique
    //        case "IncreaseAsteroidSpawn":
    //            Game_Manager.Instance.IncreaseAsteroidSpawnProbability(1.5f);
    //            break;

    //        default:
    //            Debug.LogWarning("Item effect not defined for item: " + itemName);
    //            break;
    //    }
    //}

    void DisableButtonCColliders()
    {
        GameObject[] buttonsC = GameObject.FindGameObjectsWithTag("ButtonC");
        foreach (GameObject buttonC in buttonsC)
        {
            Collider collider = buttonC.GetComponent<Collider>();
            if (collider != null)
            {
                collider.enabled = false;
                Debug.Log("Disabled collider for button with tag ButtonC: " + buttonC.name);
            }
            else
            {
                Debug.LogWarning("No collider found on button with tag ButtonC: " + buttonC.name);
            }
        }
    }
}
