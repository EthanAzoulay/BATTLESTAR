using UnityEngine;

public class PersistentColliderManager : MonoBehaviour
{
    void Update()
    {
        int totalCoins = Game_Manager.Instance.GetTotalCoins();

        if (totalCoins < 1)
        {
            DisableButtonCColliders();
        }
    }

    void DisableButtonCColliders()
    {
        GameObject[] buttonsC = GameObject.FindGameObjectsWithTag("ButtonC");
        foreach (GameObject buttonC in buttonsC)
        {
            Collider collider = buttonC.GetComponent<Collider>();
            if (collider != null && collider.enabled)
            {
                collider.enabled = false;
                Debug.Log("Persistent check - Disabled collider for button with tag ButtonC: " + buttonC.name);
            }
        }
    }
}
