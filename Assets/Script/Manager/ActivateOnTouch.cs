using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActivateOnTouch : MonoBehaviour
{
    public GameObject[] objectsToActivate;
    public Asteroid_Moving asteroidMoving;
    public ParticleSystem particleSystem;
    public Camera mainCamera; // Référence à la caméra
    public float fovIncrease = 10f; // Augmentation du FOV
    public float fovSpeed = 2f; // Vitesse de changement du FOV
    public float shakeMagnitude = 0.3f; // Intensité du shake
    private bool objectsActivated = false;
    private bool increaseFov = false; // Indique si on doit augmenter le FOV
    private float targetFov; // Stocke la valeur cible du FOV
    private Vector3 originalCameraPosition; // Position originale de la caméra

    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main; // Si aucune caméra n'est assignée, utiliser la caméra principale
        }
        originalCameraPosition = mainCamera.transform.localPosition; // Sauvegarder la position originale de la caméra
    }

    void Update()
    {
        // Gestion du toucher
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

        // Augmentation progressive du FOV
        if (increaseFov && mainCamera.fieldOfView < targetFov)
        {
            mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, targetFov, fovSpeed * Time.deltaTime);
            if (Mathf.Abs(mainCamera.fieldOfView - targetFov) < 0.1f) // Arrêter quand la différence est minimale
            {
                mainCamera.fieldOfView = targetFov;
                increaseFov = false;
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

        // Lancer l'augmentation du FOV
        targetFov = mainCamera.fieldOfView + fovIncrease;
        increaseFov = true;

        // Désactiver le MeshRenderer et le BoxCollider
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        if (meshRenderer != null)
        {
            meshRenderer.enabled = false;
        }

        BoxCollider boxCollider = GetComponent<BoxCollider>();
        if (boxCollider != null)
        {
            boxCollider.enabled = false;
        }

        // Démarrer l'effet de secousse de caméra constant
        StartCoroutine(CameraShake());

        objectsActivated = true;
    }

    IEnumerator CameraShake()
    {
        while (true) // Boucle infinie pour secouer constamment la caméra
        {
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;

            // Secouer autour de la position originale
            mainCamera.transform.localPosition = new Vector3(originalCameraPosition.x + x, originalCameraPosition.y + y, originalCameraPosition.z);

            yield return null; // Attendre le prochain frame
        }
    }
}
