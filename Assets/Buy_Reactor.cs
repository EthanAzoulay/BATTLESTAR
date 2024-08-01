using UnityEngine;

public class Buy_Reactor : MonoBehaviour
{
    // R�f�rence au ScriptableObject Butterfly
    public Reactor reactor;


    [SerializeField]
    private SOItemManager _itemManager;



    void OnMouseDown()
    {
        if (reactor != null)
        {
            // Assigner le ScriptableObject Butterfly � la r�f�rence Wings du script PlayerMovement
            _itemManager.Instance.reactor = reactor;



            Debug.Log("Butterfly wings have been assigned to the player.");
        }
        else
        {
            Debug.LogError("Butterfly or PlayerMovement is not assigned.");
        }
    }
}
