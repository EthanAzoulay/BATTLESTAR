using UnityEngine;
using System.Collections;
public class LaserBeam : MonoBehaviour
{
    private GameObject blaster;

    private void OnEnable()
    {
        // Trouve le GameObject avec le tag "Blaster" et le d�sactive
        blaster = GameObject.FindWithTag("Blaster");
        if (blaster != null)
        {
            blaster.SetActive(false);
        }

        // D�marre la coroutine qui va d�sactiver ce GameObject apr�s 10 secondes
        StartCoroutine(DeactivateAfterTime(10f));
    }

    private void OnDisable()
    {
        // R�active le GameObject avec le tag "Blaster"
        if (blaster != null)
        {
            blaster.SetActive(true);
        }
    }

    // Coroutine pour d�sactiver ce GameObject apr�s un certain temps
    private IEnumerator DeactivateAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }

    // OnTriggerEnter est appel� lorsque le Collider attach� � cet objet entre en collision avec un autre Collider
    private void OnTriggerEnter(Collider other)
    {
        // V�rifie si l'objet avec lequel nous entrons en collision a le tag "Asteroid"
        if (other.CompareTag("Asteroid"))
        {
            // D�truit l'objet avec lequel nous entrons en collision
            Destroy(other.gameObject);

           
        }
    }
}
