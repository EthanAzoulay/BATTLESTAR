using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InstantiateOnClick : MonoBehaviour
{
    // R�f�rences aux GameObjects � instancier
    public GameObject gameObjectB;
    public GameObject gameObjectC;
    public GameObject gameObjectD;

    // Positions o� les GameObjects seront instanci�s
    public Vector3 instantiatePositionB;
    public Vector3 instantiatePositionC;
    public Vector3 instantiatePositionD;

    // Vitesse de l'animation de scale
    public float scalingSpeed = 1.0f; // Param�trable via l'inspecteur Unity

    // R�f�rences aux instances des GameObjects
    private GameObject instanceB;
    private GameObject instanceC;
    private GameObject instanceD;

    // Update est appel�e une fois par frame
    void Update()
    {
        // V�rifie s'il y a un toucher et si les GameObjects ne sont pas encore instanci�s
        if (Input.touchCount > 0 && instanceB == null && instanceC == null && instanceD == null)
        {
            Touch touch = Input.GetTouch(0);

            // V�rifie si le toucher a commenc�
            if (touch.phase == TouchPhase.Began)
            {
                // Convertit la position de l'�cran en un rayon dans le monde
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;

                // V�rifie si le rayon a touch� le GameObjectA
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform == this.transform)
                    {
                        // Instancie les GameObjects aux positions param�tr�es
                        instanceB = Instantiate(gameObjectB, instantiatePositionB, Quaternion.identity);
                        instanceC = Instantiate(gameObjectC, instantiatePositionC, Quaternion.Euler(-90, 0, 0));
                        instanceD = Instantiate(gameObjectD, instantiatePositionD, Quaternion.Euler(-90, 0, 0));

                        // D�marre l'animation de scale pour l'instanceB
                        StartCoroutine(ScaleObject(instanceB, scalingSpeed));

                        // Ajoute le script de gestion des clics au GameObjectD
                        instanceD.AddComponent<Return>().SetInstances(instanceB, instanceC, instanceD);

                        // Ajoute un gestionnaire de clics � GameObjectC
                        instanceC.AddComponent<DestroyOnClick>().SetTargets(instanceB, instanceC, instanceD, this.gameObject);

                        // D�sactive les interactions avec les autres boutons
                        DisableOtherButtons();
                    }
                }
            }
        }
    }

    void DisableOtherButtons()
    {
        // Trouve tous les boutons sauf ceux avec les tags "ButtonC" et "ButtonD"
        Button[] allButtons = FindObjectsOfType<Button>();
        foreach (Button button in allButtons)
        {
            if (button.gameObject.tag != "ButtonC" && button.gameObject.tag != "ButtonD")
            {
                button.interactable = false;
            }
        }
    }

    // Coroutine pour animer l'apparition du GameObjectB
    private IEnumerator ScaleObject(GameObject target, float speed)
    {
        Vector3 originalScale = target.transform.localScale; // Stocke l'�chelle d'origine
        target.transform.localScale = Vector3.zero; // D�finit l'�chelle initiale � z�ro

        float progress = 0f;

        while (progress < 1f)
        {
            progress += Time.deltaTime * speed;
            target.transform.localScale = Vector3.Lerp(Vector3.zero, originalScale, progress);
            yield return null; // Attendre la frame suivante
        }

        // Assure que l'objet atteint exactement son �chelle finale
        target.transform.localScale = originalScale;
    }
}

public class DestroyOnClick : MonoBehaviour
{
    private GameObject targetB;
    private GameObject targetC;
    private GameObject targetD;
    private GameObject parent;

    public void SetTargets(GameObject b, GameObject c, GameObject d, GameObject parentObject)
    {
        targetB = b;
        targetC = c;
        targetD = d;
        parent = parentObject;
    }

    void OnMouseDown()
    {
        DestroyAll();
    }

    void DestroyAll()
    {
        // D�truire les GameObjects
        Destroy(targetB);
        Destroy(targetC);
        Destroy(targetD);
        Destroy(parent);
    }
}
