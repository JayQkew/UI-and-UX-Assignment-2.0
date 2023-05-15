using UnityEngine;

[CreateAssetMenu (fileName = "ScriptableObjects", menuName = "Item")]
public class SO_items : ScriptableObject
{
    [SerializeField] public string itemName;
    [SerializeField] public int itemCost;
    [SerializeField] public int itemStock;
    [SerializeField] public int itemResellCost;
}
