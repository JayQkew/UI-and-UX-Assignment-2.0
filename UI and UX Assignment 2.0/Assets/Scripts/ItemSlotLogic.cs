using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/*
 * Exists in each itemSlot prefab.
 */

public class ItemSlotLogic : MonoBehaviour, IDropHandler
{
    #region VARIABLES:
    [SerializeField] public bool slotAvailable;
    #endregion

    #region COMPONENTS:
    [SerializeField] RectTransform rectTransform;
    #endregion

    #region OTHER SCRIPTS:
    [SerializeField] ItemLogic cs_itemLogic;
    [SerializeField] GameObject go_item; // can be dragged and dropped in (its a prefab + a prefab)

    [SerializeField] StorageManager cs_backpackManager;
    [SerializeField] StorageManager cs_chestManager;
    #endregion

    private void Awake()
    {
        #region GETTING OTHER SCRIPTS:
        cs_itemLogic = go_item.GetComponent<ItemLogic>();

        cs_backpackManager = GameObject.Find("p_Backpack").GetComponent<StorageManager>();
        cs_chestManager = GameObject.Find("p_Chest").GetComponent<StorageManager>();
        #endregion
    }
    public void OnDrop(PointerEventData eventData)
    {
        cs_itemLogic.startingParent = transform; // OnDrop, item starting parent is this itemSlot;
        eventData.pointerDrag.transform.SetParent(transform, true); // parent the dropped gameObject to the itemSlot. placement managed by grid.
        eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = rectTransform.anchoredPosition;

        if (transform.parent.name == "BackpackGrid") // if the itemSlots parent is "BackpackGrid"
        {
            cs_backpackManager.items.Add(eventData.pointerDrag); // add item onto the list
        }
        else if (transform.parent.name == "ChestGrid")
        {
            cs_chestManager.items.Add(eventData.pointerDrag);
        }
    }
}
