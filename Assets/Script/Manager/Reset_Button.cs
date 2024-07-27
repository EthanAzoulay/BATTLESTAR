using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Reset_Button : MonoBehaviour
{
    public void OnShopButtonClicked()
    {
        Game_Manager.Instance.ResetGame();
    }
}
