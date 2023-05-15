using UnityEngine;
using UnityEngine.EventSystems;

/*
 * Exisits on each item prefab.
 */
public class ItemLogic : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    #region VARIABLES:
    [SerializeField] Transform startingParent;
    #endregion

    #region OTHER SCRIPTS:
    [SerializeField] public SO_items s0_items;

    [SerializeField] StorageManager cs_backpackManager;
    [SerializeField] StorageManager cs_chestManager;

    [SerializeField] PlayerManager cs_playerManager;

    [SerializeField] ResellLogic cs_resellLogic;
    #endregion

    #region GAMEOBJECTS:
    #endregion

    #region COMPONENTS:
    #endregion

    #region OTHER COMPONENTS:
    #endregion

    private void Awake()
    {
        #region GETTING OTHER SCRIPTS:
        cs_backpackManager = GameObject.Find("p_Backpack").GetComponent<StorageManager>();
        cs_chestManager = GameObject.Find("p_Chest").GetComponent<StorageManager>();

        cs_playerManager = GameObject.Find("Player").GetComponent<PlayerManager>();

        cs_resellLogic = GameObject.Find("ResellArea").GetComponent<ResellLogic>();
        #endregion

        #region GETTING GAMEOBJECTS:
        #endregion

        #region GETTING COMPONENTS:
        #endregion
    }

    private void Update()
    {
        InfiniteStorage();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        eventData.pointerDrag.GetComponent<CanvasGroup>().blocksRaycasts = false;
        startingParent = eventData.pointerDrag.transform.parent;

        if (eventData.pointerDrag.transform.parent.name == "ResellArea" && eventData.pointerDrag.transform.position.x <= 500f)
        {
            cs_resellLogic.resellAmount -= s0_items.itemResellCost;
            cs_resellLogic.tmp_resellAmount.text = cs_resellLogic.resellAmount.ToString();
        }
        else if (eventData.pointerDrag.transform.parent.name == "ResellArea" && eventData.pointerDrag.transform.position.y <= 103f)
        {
            cs_resellLogic.resellAmount -= s0_items.itemResellCost;
            cs_resellLogic.tmp_resellAmount.text = cs_resellLogic.resellAmount.ToString();
        }

    }

    public void OnDrag(PointerEventData eventData)
    {
        eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition += eventData.delta / GameObject.Find("Canvas").GetComponent<Canvas>().scaleFactor;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        eventData.pointerDrag.GetComponent<CanvasGroup>().blocksRaycasts = true;

        switch (transform.parent.parent.name) // if the parent of the parent is ...
        {
            case "BackpackGrid":
                cs_backpackManager.items.Remove(eventData.pointerDrag); // remove from the backpack items list
                cs_chestManager.items.Add(eventData.pointerDrag); // add it to the chest items list

                SortItem(eventData, cs_chestManager);
                break;
            case "ChestGrid":
                cs_chestManager.items.Remove(eventData.pointerDrag); // remove from the chest items list
                cs_backpackManager.items.Add(eventData.pointerDrag); // add to the backpack items list

                InfiniteStorage();
                SortItem(eventData, cs_backpackManager);
                break;
            case "p_Shop":
                cs_backpackManager.items.Add(eventData.pointerDrag); // add to the backpack items list

                SortItem(eventData, cs_backpackManager);
                break;
            default:
                break;
        }

        if (startingParent.name == "ResellArea" && eventData.pointerDrag.transform.position.x >= 500f)
        {
            if (cs_backpackManager.items.Contains(eventData.pointerDrag) == false) 
            {
                cs_backpackManager.items.Add(eventData.pointerDrag);
            }
            // cs_resellLogic.resellAmount -= s0_items.itemResellCost;
            SortItem(eventData, cs_backpackManager);
        }
        else if (startingParent.name == "ResellArea" && eventData.pointerDrag.transform.position.y >= 103f)
        {
            if (cs_backpackManager.items.Contains(eventData.pointerDrag) == false)
            {
                cs_backpackManager.items.Add(eventData.pointerDrag);
            }
            // cs_resellLogic.resellAmount -= s0_items.itemResellCost;
            SortItem(eventData, cs_backpackManager);
        }

        cs_resellLogic.tmp_resellAmount.text = cs_resellLogic.resellAmount.ToString();
    }

    public void SortItem(PointerEventData eventData, StorageManager script) // the place youre moving the item to
    {
        for (int i = 0; i < script.itemSlots.Count; i++)
        {
            if (script.itemSlots[i].transform.childCount == 0) // if the slot has no children (child count = 0)
            {
                eventData.pointerDrag.transform.position = script.itemSlots[i].transform.position; //  make the transform the same as the itemSot with the matching index
                eventData.pointerDrag.transform.SetParent(script.itemSlots[i].transform); // parent it to that slot
            }
        }

    }

    private void InfiniteStorage()
    {
        if(cs_chestManager.infiniteChest == true) // if player has upgraded chest to lvl 2
        {
            if (cs_chestManager.items.Count >= cs_chestManager.itemSlots.Count) // if the amount of chest items >= chest itemSlots - 5, then spawn 5 more item slots
            {
                for(int i = 0; i < 5; i++)
                {
                    cs_chestManager.AddSlot(); // add 5 more slots
                }
            }
        }
    }

}
