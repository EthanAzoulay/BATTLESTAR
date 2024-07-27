using UnityEngine;
using System.Collections.Generic;


public class ActivateOnTouch : MonoBehaviour
{
    public GameObject[] objectsToActivate;
    public Asteroid_Moving asteroidMoving;
    public ParticleSystem particleSystem;

    private bool objectsActivated = false;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(touch.position);

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform == transform)
                    {
                        ActivateObjects();
                        Game_Manager.Instance.SetNewRoundStarted(true); // Indiquer le début d'une nouvelle manche
                        Game_Manager.Instance.SetCurrentShopItems(new List<string>()); // Réinitialiser les items du shop
                    }
                }
            }
        }
    }

    void ActivateObjects()
    {
        foreach (GameObject obj in objectsToActivate)
        {
            obj.SetActive(true);
        }

        if (asteroidMoving != null)
        {
            asteroidMoving.IncreaseSpeed(1f); // Utiliser la méthode pour augmenter la vitesse
        }

        if (particleSystem != null)
        {
            var main = particleSystem.main;
            main.startSpeedMultiplier += 50f;
        }

        objectsActivated = true;
        gameObject.SetActive(false); // Désactiver l'objet au lieu de le détruire
    }
}
