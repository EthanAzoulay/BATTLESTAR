using UnityEngine;
using UnityEngine.EventSystems;

public class ShopItem : MonoBehaviour
{
    public string itemName;
    public int itemCost;

    private ShopManager shopManager;

    void Start()
    {
        shopManager = FindObjectOfType<ShopManager>();
        if (shopManager == null)
        {
            Debug.LogError("ShopManager not found!");
        }
    }

    void OnMouseDown()
    {
        OnPurchaseButtonClicked();
    }

    void OnPurchaseButtonClicked()
    {
        if (shopManager != null)
        {
            Debug.Log($"Attempting to purchase {itemName} for {itemCost} coins.");
            shopManager.PurchaseItem(itemName, itemCost);
        }
        else
        {
            Debug.LogError("ShopManager reference is missing!");
        }
    }
}
