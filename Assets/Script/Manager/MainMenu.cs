using UnityEngine;
public class MainMenu : MonoBehaviour
{
    void Start()
    {
        // R�initialiser les pi�ces au lancement du jeu (uniquement au d�but d'une nouvelle partie)
        Game_Manager.Instance.ResetCoins();
    }

    // Autres m�thodes pour g�rer le menu principal
}
