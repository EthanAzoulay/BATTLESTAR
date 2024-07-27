using UnityEngine;

public class Return: MonoBehaviour
{
    private GameObject instanceB;
    private GameObject instanceC;
    private GameObject instanceD;

    public void SetInstances(GameObject b, GameObject c, GameObject d)
    {
        instanceB = b;
        instanceC = c;
        instanceD = d;
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform == this.transform)
                    {
                        Destroy(instanceB);
                        Destroy(instanceC);
                        Destroy(instanceD);
                    }
                }
            }
        }
    }
}
