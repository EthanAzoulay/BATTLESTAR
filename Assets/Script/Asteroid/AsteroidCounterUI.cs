using UnityEngine;
using UnityEngine.UI;

public class AsteroidCounterUI : MonoBehaviour
{
    public Text asteroidCounterText;

    void Update()
    {
        asteroidCounterText.text = "Asteroids Destroyed : " + Game_Manager.Instance.GetDestroyedAsteroidsCount();
    }
}
