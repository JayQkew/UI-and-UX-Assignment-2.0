using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

/*
 * Exisits on each item prefab.
 */
public class ItemLogic : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    #region VARIABLES:
    #endregion

    #region OTHER SCRIPTS:
    [SerializeField] SO_items s0_items;

    [SerializeField] ItemSlotLogic cs_itemSlotLogic; // might have to make it an array
    [SerializeField] GameObject go_itemSlot; // prefab, might have to make it an array
    [SerializeField] StorageManager cs_backpackManager;
    [SerializeField] StorageManager cs_chestManager;
    #endregion

    #region GAMEOBJECTS:
    [SerializeField] GameObject go_divider;
    [SerializeField] GameObject go_player;
    #endregion

    #region COMPONENTS:
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] RectTransform rectTransform;
    #endregion

    #region OTHER COMPONENTS:
    public Transform startingParent;
    [SerializeField] Canvas canvas;
    #endregion

    private void Awake()
    {
        #region GETTING OTHER SCRIPTS:
        go_itemSlot = GameObject.FindGameObjectWithTag("itemSlot"); // might have to make it FindGameObjectsWithTag
        cs_itemSlotLogic = go_itemSlot.GetComponent<ItemSlotLogic>(); // go_itemSlot = GameObject.Find("");
        cs_backpackManager = GameObject.Find("p_Backpack").GetComponent<StorageManager>();
        cs_chestManager = GameObject.Find("p_Chest").GetComponent<StorageManager>();
        #endregion

        #region GETTING GAMEOBJECTS:
        go_divider = GameObject.Find("Divider");
        go_player = GameObject.Find("Player");
        #endregion

        #region GETTING COMPONENTS:
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>(); // find object + get component
        #endregion
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        startingParent = transform.parent; // make the startingParent its current parent

        if (transform.parent.parent.name == "BackpackGrid") // if the parent of the parent is "BackpackGrid"
        {
            cs_backpackManager.items.Remove(eventData.pointerDrag); // Removes the pointerDrag Gameobject from the list On begin drag. 
        }
        else if(transform.parent.parent.name == "ChestGrid")
        {
            cs_chestManager.items.Remove(eventData.pointerDrag);
        }

        transform.SetParent(go_player.transform, true); // make the object a child of Player -> on the top layer so that you can see object
        canvasGroup.blocksRaycasts = false; // allows for rays to pass through the object and detect anything behind it.
        canvasGroup.alpha = 0.85f; // change transparency
        // Debug.Log(startingParent.name);

    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;
    }
}
