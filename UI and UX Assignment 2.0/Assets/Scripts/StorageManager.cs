using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Manage the Backpack and the Chest
 */
public class StorageManager : MonoBehaviour
{
    #region VARIABLES:
    [SerializeField] public List<GameObject> items = new List<GameObject>();
    [SerializeField] public List<GameObject> itemSlots = new List<GameObject>();
    [SerializeField] GameObject[] existingSlots = new GameObject[9];
    [SerializeField] GridLayoutGroup gridLayoutGroup;

    [SerializeField] private int bp_upgrade1 = 8;
    [SerializeField] private int bp_upgrade2 = 11;

    [SerializeField] private int c_upgrade1 = 10;
    [SerializeField] private int c_upgrade2 = 15;
    [SerializeField] public bool infiniteChest;

    [SerializeField] List<GameObject> buns = new List<GameObject>();
    [SerializeField] List<GameObject> cheese = new List<GameObject>();
    [SerializeField] List<GameObject> lettuce = new List<GameObject>();
    [SerializeField] List<GameObject> patty = new List<GameObject>();
    [SerializeField] List<GameObject> sauce = new List<GameObject>();
    [SerializeField] List<GameObject> tomato = new List<GameObject>();
    #endregion

    #region GAMEOBJECTS:
    [SerializeField] GameObject go_itemSlot;
    [SerializeField] GameObject go_itemGrid;

    [SerializeField] GameObject Upgrade1;
    [SerializeField] GameObject Upgrade2;

    [SerializeField] GameObject go_divider;
    #endregion

    #region OTHER SCRIPTS:
    [SerializeField] PlayerManager cs_playerManager;
    [SerializeField] ItemLogic cs_itemLogic;
    [SerializeField] GameObject go_item;
    #endregion

    private void Awake()
    {
        cs_playerManager = GameObject.Find("Player").GetComponent<PlayerManager>();
        cs_itemLogic = go_item.GetComponent<ItemLogic>();
    }
    private void Start()
    {
        infiniteChest = false;
        foreach (GameObject slot in existingSlots) // for each existing itemSlot, put it in the List
        {
            itemSlots.Add(slot);
        }
    }


    public void AddSlot() // method to add slots (for Upgrades)
    {
        itemSlots.Add(Instantiate(go_itemSlot, go_itemGrid.transform)); // spawns an itemSlot as a child of backpackGrid
    }

    public void BackpackUpgrade1()
    {
        if(cs_playerManager.playerCurrency >= bp_upgrade1)
        {
            for (int i = 0; i < 3; i++)
            {
                AddSlot();
            }
            Upgrade1.SetActive(false);
            Upgrade2.SetActive(true);
            cs_playerManager.playerCurrency -= bp_upgrade1;
        }
    }

    public void BackpackUpgrade2()
    {
        if(cs_playerManager.playerCurrency >= bp_upgrade2)
        {
            gridLayoutGroup.constraintCount = 4;
            for (int i = 0; i < 4; i++)
            {
                AddSlot();
            }
            Upgrade2.SetActive(false);
            cs_playerManager.playerCurrency -= bp_upgrade2;
        }
    }

    public void ChestUpgrade1()
    {
        if (cs_playerManager.playerCurrency >= c_upgrade1)
        {
            gridLayoutGroup.constraintCount = 4;
            for (int i = 0; i < 7; i++)
            {
                AddSlot();
            }
            Upgrade1.SetActive(false);
            Upgrade2.SetActive(true);
            cs_playerManager.playerCurrency -= c_upgrade1;
        }
    }

    public void ChestUpgrade2()
    {
        if (cs_playerManager.playerCurrency >= c_upgrade2)
        {
            gridLayoutGroup.constraintCount = 5;
            for (int i = 0; i < 9; i++)
            {
                AddSlot();
            }
            infiniteChest = true;
            cs_playerManager.playerCurrency -= c_upgrade2;
        }
    }

    public void ItemSorting()
    {
        foreach (GameObject item in items) // goes throught each item in items
        {
            switch (item.tag) // if the tag matches, add the item to the specified list
            {
                case "buns":
                    buns.Add(item);
                    break;
                case "cheese":
                    cheese.Add(item);
                    break;
                case "lettuce":
                    lettuce.Add(item);
                    break;
                case "patty":
                    patty.Add(item);
                    break;
                case "sauce":
                    sauce.Add(item);
                    break;
                case "tomato":
                    tomato.Add(item);
                    break;
                default: break;
            }
        }

        items.Clear(); // clear the items list
        
        AddItemToList(buns);
        AddItemToList(cheese);
        AddItemToList(lettuce);
        AddItemToList(patty);
        AddItemToList(sauce);
        AddItemToList(tomato);
        
        for(int i = 0;i < items.Count; i++)
        {
            items[i].transform.SetParent(itemSlots[i].transform); // parents the item to the itemSlot
            items[i].transform.position = itemSlots[i].transform.position + new Vector3(0,0,2); // // makes it the same transfrom as the object
        }
        
    }

    public void AddItemToList(List<GameObject> list) // loops though each item list and adds that item to the items list
    {
        for(int i  = 0; i < list.Count; i++)
        {
            items.Add(list[i]);
        }
        list.Clear();
    }
}
