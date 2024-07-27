using UnityEngine;
public class MainMenu : MonoBehaviour
{
    void Start()
    {
        // Réinitialiser les pièces au lancement du jeu (uniquement au début d'une nouvelle partie)
        Game_Manager.Instance.ResetCoins();
    }

    // Autres méthodes pour gérer le menu principal
}
