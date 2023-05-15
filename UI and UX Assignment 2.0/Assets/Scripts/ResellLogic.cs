using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ResellLogic : MonoBehaviour, IDropHandler
{
    #region VARIABLES:
    [SerializeField] int resellCost;
    [SerializeField] public int resellAmount = 0;
    [SerializeField] public List<GameObject> resoldItems = new List<GameObject>();
    #endregion

    #region OTHER SCRIPTS:
    [SerializeField] PlayerManager cs_playerManager;
    [SerializeField] SO_items[] s0_items = new SO_items[6];
    #endregion

    #region COMPONENTS:
    [SerializeField] TextMeshProUGUI tmp_resellAmount;
    #endregion

    private void Awake()
    {
        cs_playerManager = GameObject.Find("Player").GetComponent<PlayerManager>();
    }
    public void OnDrop(PointerEventData eventData)
    {
        eventData.pointerDrag.transform.SetParent(transform); // parent the object to this object
        switch (eventData.pointerDrag.tag)
        {
            case "buns":
                resellAmount += s0_items[0].itemResellCost;
                break;
            case "cheese":
                resellAmount += s0_items[1].itemResellCost;
                break;
            case "lettuce":
                resellAmount += s0_items[2].itemResellCost;
                break;
            case "patty":
                resellAmount += s0_items[3].itemResellCost;
                break;
            case "sauce":
                resellAmount += s0_items[4].itemResellCost;
                break;
            case "tomato":
                resellAmount += s0_items[5].itemResellCost;
                break;
            default: break;
            }
        tmp_resellAmount.text = resellAmount.ToString();
    }

    public void SellButton()
    {
        for (int i = 0; i < transform.childCount; i++) // loop for as long as the childCount
        {
            switch (transform.GetChild(i).tag)
            {
                case "buns":
                    resellCost = s0_items[0].itemResellCost;
                    break;
                case "cheese":
                    resellCost = s0_items[1].itemResellCost;
                    break;
                case "lettuce":
                    resellCost = s0_items[2].itemResellCost;
                    break;
                case "patty":
                    resellCost = s0_items[3].itemResellCost;
                    break;
                case "sauce":
                    resellCost = s0_items[4].itemResellCost;
                    break;
                case "tomato":
                    resellCost = s0_items[5].itemResellCost;
                    break;
                    default: break;
            }
            cs_playerManager.playerCurrency += resellCost; // add the resell value to the player currency
            transform.GetChild(i).gameObject.SetActive(false);
        }

        resellAmount = 0;
        tmp_resellAmount.text =resellAmount.ToString();
    }
}
