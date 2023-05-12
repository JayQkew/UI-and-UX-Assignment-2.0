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
    #region OTHER SCRIPTS:
    [SerializeField] SO_items s0_items;

    [SerializeField] ItemSlotLogic cs_itemSlotLogic; // might have to make it an array
    [SerializeField] GameObject go_itemSlot; // prefab, might have to make it an array
    [SerializeField] BackpackManager cs_backpackManager;
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
        cs_backpackManager = GameObject.Find("p_Backpack").GetComponent<BackpackManager>();
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
        Debug.Log("parent: " + transform.parent.name);
        Debug.Log("parent of parent: " + transform.parent.parent.name);

        startingParent = transform.parent; // make the startingParent its current parent
        transform.SetParent(go_player.transform, true); // make the object a child of Player -> on the top layer so that you can see object
        canvasGroup.blocksRaycasts = false; // allows for rays to pass through the object and detect anything behind it.
        canvasGroup.alpha = 0.85f; // change transparency
        // Debug.Log(startingParent.name);

        if(transform.parent.parent.name == "BackpackGrid") // if the parent of the parent is "BackpackGrid"
        {
            Debug.Log("here1");
            cs_backpackManager.items.RemoveAt(cs_itemSlotLogic.itemListIndex); // remove the item from the items list
            Debug.Log("here2");
        }
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
