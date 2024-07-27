using UnityEngine;
using System.Collections;
public class LaserBeam : MonoBehaviour
{
    private GameObject blaster;

    private void OnEnable()
    {
        // Trouve le GameObject avec le tag "Blaster" et le désactive
        blaster = GameObject.FindWithTag("Blaster");
        if (blaster != null)
        {
            blaster.SetActive(false);
        }

        // Démarre la coroutine qui va désactiver ce GameObject après 10 secondes
        StartCoroutine(DeactivateAfterTime(10f));
    }

    private void OnDisable()
    {
        // Réactive le GameObject avec le tag "Blaster"
        if (blaster != null)
        {
            blaster.SetActive(true);
        }
    }

    // Coroutine pour désactiver ce GameObject après un certain temps
    private IEnumerator DeactivateAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }

    // OnTriggerEnter est appelé lorsque le Collider attaché à cet objet entre en collision avec un autre Collider
    private void OnTriggerEnter(Collider other)
    {
        // Vérifie si l'objet avec lequel nous entrons en collision a le tag "Asteroid"
        if (other.CompareTag("Asteroid"))
        {
            // Détruit l'objet avec lequel nous entrons en collision
            Destroy(other.gameObject);

           
        }
    }
}
