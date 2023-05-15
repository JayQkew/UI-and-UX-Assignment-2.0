using UnityEngine;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    #region VARIABLES:
    [SerializeField] public int playerCurrency = 100;
    #endregion

    #region OTHER GAMEOBJECTS:
    [SerializeField] TextMeshProUGUI tmp_playerCurrency;
    #endregion

    private void Update()
    {
        tmp_playerCurrency.text = playerCurrency.ToString();
    }

    public void ExitFunction()
    {
        Application.Quit();
    }
}
