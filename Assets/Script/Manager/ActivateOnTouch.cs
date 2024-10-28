using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActivateOnTouch : MonoBehaviour
{
    public GameObject[] objectsToActivate;
    public Asteroid_Moving asteroidMoving;
    public ParticleSystem particleSystem;
    public Camera mainCamera;
    public float fovIncrease = 10f;
    public float fovSpeed = 2f;
    public float shakeMagnitude = 0.3f;
    private bool objectsActivated = false;
    private bool increaseFov = false;
    private float targetFov;
    private Vector3 originalCameraPosition;

    // Objets à désactiver
    public GameObject targetRawImage;

    // Références aux systèmes de particules
    public GameObject playParticles;
    public GameObject playParticles2;

    // Référence au GameObject "Band" avec le composant Animation
    public GameObject band;

    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
        originalCameraPosition = mainCamera.transform.localPosition;
    }

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
                        Game_Manager.Instance.SetNewRoundStarted(true);
                        Game_Manager.Instance.SetCurrentShopItems(new List<string>());
                    }
                }
            }
        }

        if (increaseFov && mainCamera.fieldOfView < targetFov)
        {
            mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, targetFov, fovSpeed * Time.deltaTime);
            if (Mathf.Abs(mainCamera.fieldOfView - targetFov) < 0.1f)
            {
                mainCamera.fieldOfView = targetFov;
                increaseFov = false;
            }
        }
    }

    void ActivateObjects()
    {
        targetRawImage.SetActive(false);

        foreach (GameObject obj in objectsToActivate)
        {
            obj.SetActive(true);
        }

        if (asteroidMoving != null)
        {
            asteroidMoving.IncreaseSpeed(1f);
        }

        if (particleSystem != null)
        {
            var main = particleSystem.main;
            main.startSpeedMultiplier += 50f;
        }

        targetFov = mainCamera.fieldOfView + fovIncrease;
        increaseFov = true;

        BoxCollider boxCollider = GetComponent<BoxCollider>();
        if (boxCollider != null)
        {
            boxCollider.enabled = false;
        }

        StartCoroutine(CameraShake());

        objectsActivated = true;

        if (playParticles != null)
        {
            Destroy(playParticles);
        }

        if (playParticles2 != null)
        {
            playParticles2.SetActive(true);
        }

        // Activer le composant Animation de "Band" et jouer l'animation "Band_Anim"
        if (band != null)
        {
            Animation bandAnimation = band.GetComponent<Animation>();
            if (bandAnimation != null)
            {
                bandAnimation.enabled = true; // Activer le composant Animation
                bandAnimation.Play("Band_Anim"); // Jouer l'animation "Band_Anim"
            }
            else
            {
                Debug.LogError("Le composant Animation est manquant sur le GameObject 'Band'.");
            }
        }
    }

    IEnumerator CameraShake()
    {
        while (true)
        {
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;

            mainCamera.transform.localPosition = new Vector3(originalCameraPosition.x + x, originalCameraPosition.y + y, originalCameraPosition.z);

            yield return null;
        }
    }
}
