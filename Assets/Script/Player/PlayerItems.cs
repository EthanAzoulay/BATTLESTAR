using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class RotationSpeedEffect
{
    public GameObject rotationPrefab;
    public float threshold;
    [HideInInspector]
    public bool hasInstantiated; // Indicateur pour vérifier si l'effet a été instancié
}

[System.Serializable]
public class SpeedEffect
{
    public GameObject speedPrefab;
    public float threshold;
    [HideInInspector]
    public bool hasInstantiated; // Indicateur pour vérifier si l'effet a été instancié
}


public class PlayerItems : MonoBehaviour
{
    private int coinCount;

    //Références aux objets achetés
    public GameObject shieldPrefab;

    public GameObject rightCannonPrefab;
    public GameObject leftCannonPrefab;

    public List<RotationSpeedEffect> rotationSpeedPrefabs; // Liste des effets de rotation et leurs seuils
    public List<SpeedEffect> speedPrefab;

    private PlayerMovement playerMovement;
    public PlayerShooting playerShooting; // Référence publique au script PlayerShooting
    public GameObject Blaster; // Le GameObject du joueur


    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();


        //S'assurer que PlayerShooting est bien référencé
        if (playerShooting == null)
        {
            Debug.LogError("PlayerShooting script is not assigned.");
        }

        coinCount = 0;
        Bullet_Moving.defaultDamage = 1; // Réinitialiser les dégâts par défaut au démarrage
        LoadPurchasedItems(); // Charger les items achetés
    }

    public void AddCoin()
    {
        coinCount++;
        Debug.Log("Coins:" + coinCount);
    }

    public int GetCoinCount()
    {
        return coinCount;
    }

    public void IncreaseFireRate(float amount)
    {
        if (playerShooting != null)
        {
            playerShooting.fireRate -= amount;
            Debug.Log("Fire rate increased to: " + playerShooting.fireRate);
        }
        else
        {

        }
    }
    public void Add5Coins(float amount)
    {

    }

    public void IncreaseBulletDamage(int amount)
    {
        Bullet_Moving.defaultDamage += amount;
        Debug.Log("Bullet damage increased to: " + Bullet_Moving.defaultDamage);
    }

    public void IncreaseRotationSpeed(float amount)
    {
        playerMovement.rotationSpeed += amount;
        Debug.Log("Rotation speed increased to: " + playerMovement.rotationSpeed);

        //Vérifier chaque effet dans la liste des effets de rotation
        foreach (var effect in rotationSpeedPrefabs)
        {
            if (!effect.hasInstantiated && playerMovement.rotationSpeed >= effect.threshold)
            {
                //Instancier le GameObject en conservant son transform d'origine et le définir comme enfant du joueur
                if (effect.rotationPrefab != null)
                {
                    Vector3 originalPosition = effect.rotationPrefab.transform.position;
                    Quaternion originalRotation = effect.rotationPrefab.transform.rotation;
                    Vector3 originalScale = effect.rotationPrefab.transform.localScale;

                    GameObject instantiatedEffect = Instantiate(effect.rotationPrefab, originalPosition, originalRotation);
                    instantiatedEffect.transform.localScale = originalScale;
                    instantiatedEffect.transform.SetParent(transform, true); // 'true' conserve les valeurs locales de l'objet

                    //Marquer l'effet comme instancié
                    effect.hasInstantiated = true;
                }
            }
        }
    }

    public void IncreaseSpeed(float amount)
    {
        playerMovement.speed += amount;
        Debug.Log("Speed increased to: " + playerMovement.speed);
        // Vérifier chaque effet dans la liste des effets de rotation
        foreach (var effect in speedPrefab)
        {
            if (!effect.hasInstantiated && playerMovement.speed >= effect.threshold)
            {
                // Instancier le GameObject en conservant son transform d'origine et le définir comme enfant du joueur
                if (effect.speedPrefab != null)
                {
                    Vector3 originalPosition = effect.speedPrefab.transform.position;
                    Quaternion originalRotation = effect.speedPrefab.transform.rotation;
                    Vector3 originalScale = effect.speedPrefab.transform.localScale;

                    GameObject instantiatedEffect = Instantiate(effect.speedPrefab, originalPosition, originalRotation);
                    instantiatedEffect.transform.localScale = originalScale;
                    instantiatedEffect.transform.SetParent(transform, true); // 'true' conserve les valeurs locales de l'objet

                    // Marquer l'effet comme instancié
                    effect.hasInstantiated = true;
                }
            }
        }
    }

    public void AddShield()
    {
        Vector3 originalPosition = shieldPrefab.transform.position;
        Quaternion originalRotation = shieldPrefab.transform.rotation;
        Vector3 originalScale = shieldPrefab.transform.localScale;

        GameObject instantiatedCannon = Instantiate(shieldPrefab);
        instantiatedCannon.transform.position = originalPosition;
        instantiatedCannon.transform.rotation = originalRotation;
        instantiatedCannon.transform.localScale = originalScale;
        instantiatedCannon.transform.SetParent(transform, false); // 'false' conserve les valeurs mondiales de l'objet

    }

    public void DurationShield()
    {
        DoubleTapShield.shieldDuration += 3;
        Debug.Log("Shield duration increased to: " + DoubleTapShield.shieldDuration);
    }


    public void AddCannon()
    {
        Vector3 originalPosition = rightCannonPrefab.transform.position;
        Quaternion originalRotation = rightCannonPrefab.transform.rotation;
        Vector3 originalScale = rightCannonPrefab.transform.localScale;

        GameObject instantiatedCannon = Instantiate(rightCannonPrefab);
        instantiatedCannon.transform.position = originalPosition;
        instantiatedCannon.transform.rotation = originalRotation;
        instantiatedCannon.transform.localScale = originalScale;
        instantiatedCannon.transform.SetParent(transform, false); // 'false' conserve les valeurs mondiales de l'objet

        //Assigner le joueur au script SimplePlayerShooting
        SimplePlayerShooting shootingScript = instantiatedCannon.GetComponent<SimplePlayerShooting>();
        if (shootingScript != null)
        {
            shootingScript.SetPlayer(Blaster);
        }
    }

    public void AddCannon2()
    {
        Vector3 originalPosition = leftCannonPrefab.transform.position;
        Quaternion originalRotation = leftCannonPrefab.transform.rotation;
        Vector3 originalScale = leftCannonPrefab.transform.localScale;

        GameObject instantiatedCannon = Instantiate(leftCannonPrefab);
        instantiatedCannon.transform.position = originalPosition;
        instantiatedCannon.transform.rotation = originalRotation;
        instantiatedCannon.transform.localScale = originalScale;
        instantiatedCannon.transform.SetParent(transform, false); // 'false' conserve les valeurs mondiales de l'objet

        //Assigner le joueur au script SimplePlayerShooting
        SimplePlayerShooting shootingScript = instantiatedCannon.GetComponent<SimplePlayerShooting>();
        if (shootingScript != null)
        {
            shootingScript.SetPlayer(Blaster);
        }
    }

    private void LoadPurchasedItems()
    {
        // Assurez-vous que ces effets sont cumulatifs
        ApplyPurchasedItemEffect("Cadence", 0.05f, Game_Manager.Instance.GetPurchasedItemCount("Cadence"));
        ApplyPurchasedItemEffect("Puissance", 1, Game_Manager.Instance.GetPurchasedItemCount("Puissance"));

        ApplyPurchasedItemEffect("Rotation", 1f, Game_Manager.Instance.GetPurchasedItemCount("Rotation"));
        ApplyPurchasedItemEffect("Rotation 2", 1f, Game_Manager.Instance.GetPurchasedItemCount("Rotation 2"));
        ApplyPurchasedItemEffect("Rotation 3", 1f, Game_Manager.Instance.GetPurchasedItemCount("Rotation 3"));
        ApplyPurchasedItemEffect("Rotation 4", 1f, Game_Manager.Instance.GetPurchasedItemCount("Rotation 4"));

        ApplyPurchasedItemEffect("Vitesse", 1f, Game_Manager.Instance.GetPurchasedItemCount("Vitesse"));
        ApplyPurchasedItemEffect("Vitesse 2", 1f, Game_Manager.Instance.GetPurchasedItemCount("Vitesse 2"));
        ApplyPurchasedItemEffect("Vitesse 3", 1f, Game_Manager.Instance.GetPurchasedItemCount("Vitesse 3"));
        ApplyPurchasedItemEffect("Vitesse 4", 1f, Game_Manager.Instance.GetPurchasedItemCount("Vitesse 4"));

        ApplyPurchasedItemEffect("Shield", 0, Game_Manager.Instance.GetPurchasedItemCount("Shield"));
        ApplyPurchasedItemEffect("Cannon", 0, Game_Manager.Instance.GetPurchasedItemCount("Cannon"));
        ApplyPurchasedItemEffect("Cannon2", 0, Game_Manager.Instance.GetPurchasedItemCount("Cannon2"));

    }

    private void ApplyPurchasedItemEffect(string itemName, float value, int count)
    {
        for (int i = 0; i < count; i++)
        {
            switch (itemName)
            {
                case "Cadence":
                    IncreaseFireRate(value);
                    break;
                case "Puissance":
                    IncreaseBulletDamage((int)value);
                    break;

                case "Rotation":
                    IncreaseRotationSpeed(value);
                    break;
                case "Rotation 2":
                    IncreaseRotationSpeed(value);
                    break;
                case "Rotation 3":
                    IncreaseRotationSpeed(value);
                    break;
                case "Rotation 4":
                    IncreaseRotationSpeed(value);
                    break;

                case "Vitesse":
                    IncreaseSpeed(value);
                    break;
                case "Vitesse 2":
                    IncreaseSpeed(value);
                    break;
                case "Vitesse 3":
                    IncreaseSpeed(value);
                    break;
                case "Vitesse 4":
                    IncreaseSpeed(value);
                    break;

                case "Shield":
                    DurationShield();
                    break;
                case "Cannon":
                    AddCannon();
                    break;
                case "Cannon2":
                    AddCannon2();
                    break;
                default:
                    break;
            }
        }
    }
}
