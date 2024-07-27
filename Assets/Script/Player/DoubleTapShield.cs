using UnityEngine;
using System.Collections;

public class DoubleTapShield : MonoBehaviour
{
    public PlayerItems playerItems;
    public static float shieldDuration = 5f; // Duration the shield will be active

    private float lastTapTime = 0f;
    private float doubleTapThreshold = 0.5f; // Maximum time between taps to consider it a double tap
    private bool shieldInstantiated = false; // Indicator for shield instantiation

    void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Ended)
            {
                float currentTime = Time.time;

                if (currentTime - lastTapTime <= doubleTapThreshold)
                {
                    if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Scene" && !shieldInstantiated)
                    {
                        StartCoroutine(ActivateShield());
                    }
                }

                lastTapTime = currentTime;
            }
        }
    }

    private IEnumerator ActivateShield()
    {
        playerItems.AddShield();
        shieldInstantiated = true;
        yield return new WaitForSeconds(shieldDuration);
        DestroyShield();
    }

    private void DestroyShield()
    {
        Transform shield = transform.Find(playerItems.shieldPrefab.name + "(Clone)");
        if (shield != null)
        {
            Destroy(shield.gameObject);
        }
    }
}
