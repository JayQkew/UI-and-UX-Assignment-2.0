using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackpackManager : MonoBehaviour
{
    #region VARIABLES:
    [SerializeField] public List<GameObject> items = new List<GameObject>();
    [SerializeField] public List<GameObject> itemSlots = new List<GameObject>();
    [SerializeField] GameObject[] existingSlots = new GameObject[9];
    #endregion

    #region GAMEOBJECTS:
    [SerializeField] GameObject go_backpackSlot;
    [SerializeField] GameObject go_backpackGrid;
    #endregion
    private void Start()
    {
        foreach (var slot in existingSlots) // for each existing itemSlot, put it in the List
        {
            itemSlots.Add(slot);
        }
    }
    public void AddSlot() // method to add slots (for Upgrades)
    {
        itemSlots.Add(Instantiate(go_backpackSlot, go_backpackGrid.transform)); // spawns an itemSlot as a child of backpackGrid
    }
}
