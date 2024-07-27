using UnityEngine;

public class Buy_Butterfly : MonoBehaviour
{
    // R�f�rence au ScriptableObject Butterfly
    public Wings butterfly;

    // R�f�rence au GameObject du joueur pour acc�der � son script PlayerMovement
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
            // Assigner le ScriptableObject Butterfly � la r�f�rence Wings du script PlayerMovement
            playerMovement.wings = butterfly;

            // Appliquer les nouvelles ailes et les am�liorations
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
