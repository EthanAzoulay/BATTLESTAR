using UnityEngine;

public class Buy_Butterfly : MonoBehaviour
{
    // Référence au ScriptableObject Butterfly
    public Wings butterfly;

    // Référence au GameObject du joueur pour accéder à son script PlayerMovement
    public GameObject playerObject;

    private PlayerMovement playerMovement;

    void Start()
    {
        if (playerObject != null)
        {
            playerMovement = playerObject.GetComponent<PlayerMovement>();

            if (playerMovement == null)
            {
                Debug.LogError("PlayerMovement script is not attached to the playerObject.");
            }
        }
        else
        {
            Debug.LogError("Player object is not assigned.");
        }
    }

    void OnMouseDown()
    {
        if (playerMovement != null && butterfly != null)
        {
            // Assigner le ScriptableObject Butterfly à la référence Wings du script PlayerMovement
            playerMovement.wings = butterfly;

            // Appliquer les nouvelles ailes et les améliorations
            playerMovement.ApplyWings();
            playerMovement.ApplyUpgrades();

            Debug.Log("Butterfly wings have been assigned to the player.");
        }
        else
        {
            Debug.LogError("Butterfly or PlayerMovement is not assigned.");
        }
    }
}
