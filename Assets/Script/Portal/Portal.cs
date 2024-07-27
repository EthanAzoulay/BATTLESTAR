using UnityEngine;
using System.Collections.Generic;

public class Portal : MonoBehaviour
{
    public List<GameObject> gameObjectsToInstantiate; // La liste des GameObjects à instancier aléatoirement
    public float speed = 1.0f; // Vitesse de déplacement du portail vers le haut
    private bool objectInstantiated = false; // Booléen pour vérifier si un GameObject a été instancié

    void Update()
    {
        // Déplacer le portal vers le haut continuellement
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        // Vérifier si l'objet qui entre dans le trigger est le player et si aucun objet n'a été instancié
        if (other.CompareTag("Player") && !objectInstantiated)
        {
            // Instancier un GameObject aléatoire de la liste
            InstantiateRandomGameObject(other.gameObject);
            objectInstantiated = true; // Marquer que l'objet a été instancié
        }
    }

    void InstantiateRandomGameObject(GameObject player)
    {
        if (gameObjectsToInstantiate.Count > 0)
        {
            // Sélectionner un GameObject aléatoire dans la liste
            int randomIndex = Random.Range(0, gameObjectsToInstantiate.Count);
            GameObject randomGameObject = gameObjectsToInstantiate[randomIndex];

            // Instancier le GameObject en utilisant la position, la rotation et l'échelle locales du prefab
            GameObject instantiatedObject = Instantiate(randomGameObject);

            // Appliquer la position, la rotation et l'échelle locales du prefab
            instantiatedObject.transform.localPosition = randomGameObject.transform.localPosition;
            instantiatedObject.transform.localRotation = randomGameObject.transform.localRotation;
            instantiatedObject.transform.localScale = randomGameObject.transform.localScale;

            // Si le GameObject a le tag "Beam", le définir comme enfant du player
            if (instantiatedObject.CompareTag("Beam"))
            {
                instantiatedObject.transform.parent = player.transform;
                instantiatedObject.transform.localPosition = randomGameObject.transform.localPosition;
                instantiatedObject.transform.localRotation = randomGameObject.transform.localRotation;
                instantiatedObject.transform.localScale = randomGameObject.transform.localScale;
            }
        }
    }

    // Méthode pour réinitialiser le portail afin qu'il puisse instancier un nouvel objet
    public void ResetPortal()
    {
        objectInstantiated = false;
    }
}
