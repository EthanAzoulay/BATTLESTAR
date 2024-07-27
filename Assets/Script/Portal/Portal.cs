using UnityEngine;
using System.Collections.Generic;

public class Portal : MonoBehaviour
{
    public List<GameObject> gameObjectsToInstantiate; // La liste des GameObjects � instancier al�atoirement
    public float speed = 1.0f; // Vitesse de d�placement du portail vers le haut
    private bool objectInstantiated = false; // Bool�en pour v�rifier si un GameObject a �t� instanci�

    void Update()
    {
        // D�placer le portal vers le haut continuellement
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        // V�rifier si l'objet qui entre dans le trigger est le player et si aucun objet n'a �t� instanci�
        if (other.CompareTag("Player") && !objectInstantiated)
        {
            // Instancier un GameObject al�atoire de la liste
            InstantiateRandomGameObject(other.gameObject);
            objectInstantiated = true; // Marquer que l'objet a �t� instanci�
        }
    }

    void InstantiateRandomGameObject(GameObject player)
    {
        if (gameObjectsToInstantiate.Count > 0)
        {
            // S�lectionner un GameObject al�atoire dans la liste
            int randomIndex = Random.Range(0, gameObjectsToInstantiate.Count);
            GameObject randomGameObject = gameObjectsToInstantiate[randomIndex];

            // Instancier le GameObject en utilisant la position, la rotation et l'�chelle locales du prefab
            GameObject instantiatedObject = Instantiate(randomGameObject);

            // Appliquer la position, la rotation et l'�chelle locales du prefab
            instantiatedObject.transform.localPosition = randomGameObject.transform.localPosition;
            instantiatedObject.transform.localRotation = randomGameObject.transform.localRotation;
            instantiatedObject.transform.localScale = randomGameObject.transform.localScale;

            // Si le GameObject a le tag "Beam", le d�finir comme enfant du player
            if (instantiatedObject.CompareTag("Beam"))
            {
                instantiatedObject.transform.parent = player.transform;
                instantiatedObject.transform.localPosition = randomGameObject.transform.localPosition;
                instantiatedObject.transform.localRotation = randomGameObject.transform.localRotation;
                instantiatedObject.transform.localScale = randomGameObject.transform.localScale;
            }
        }
    }

    // M�thode pour r�initialiser le portail afin qu'il puisse instancier un nouvel objet
    public void ResetPortal()
    {
        objectInstantiated = false;
    }
}
