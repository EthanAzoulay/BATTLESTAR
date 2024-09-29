using UnityEngine;

public class Buy_Butterfly : MonoBehaviour
{
    // R�f�rence au ScriptableObject Butterfly
    public Wings butterfly;

   
    [SerializeField]
    private SOItemManager _itemManager;

   

    void OnMouseDown()
    {
        if (butterfly != null)
        {
            // Assigner le ScriptableObject Butterfly � la r�f�rence Wings du script PlayerMovement
            _itemManager.Instance.wings = butterfly;

           

            Debug.Log("Butterfly wings have been assigned to the player.");
        }
        else
        {
            Debug.LogError("Butterfly or PlayerMovement is not assigned.");
        }
    }
}
