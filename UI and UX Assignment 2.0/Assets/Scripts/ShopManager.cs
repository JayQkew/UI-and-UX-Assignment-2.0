using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;
/*
 * Manage the items that can be bought from the shop
 */
public class ShopManager : MonoBehaviour, IPointerClickHandler
{
    #region VARIABLES:
    [SerializeField] GameObject go_item;
    [SerializeField] RectTransform rt_spawnArea;
    [SerializeField] Vector3 v3_spawnArea;
    [SerializeField] Vector3[] v3_spawnAreaCorners;
    [SerializeField] GameObject go_itemSpawn;

    [SerializeField] public int currentStock;
    [SerializeField] public float currentTime;
    [SerializeField] private float restockTime = 25f;
 
    private float xPos;
    private float yPos;

    public Vector3 itemStartingPosition;
    #endregion

    #region OTHER SCRIPTS:
    [SerializeField] SO_items s0_items;
    [SerializeField] GameObject go_player;
    [SerializeField] PlayerManager cs_playerManager;
    [SerializeField] ItemLogic cs_itemLogic;
    [SerializeField] GameObject go_items;
    #endregion

    #region OTHER GAMEOBJECTS:
    [SerializeField] TextMeshProUGUI tmp_itemCost;
    [SerializeField] TextMeshProUGUI tmp_itemStock;
    [SerializeField] TextMeshProUGUI tmp_restockTimer;

    [SerializeField] GameObject go_itemStatus;
    [SerializeField] TextMeshProUGUI tmp_itemStatus;

    [SerializeField] GameObject go_itemImage;
    #endregion

    private void Awake()
    {
        cs_playerManager = go_player.GetComponent<PlayerManager>();
        cs_itemLogic = go_item.GetComponent<ItemLogic>();
    }
    private void Start()
    {
        currentStock = s0_items.itemStock;
        currentTime = restockTime;
        go_itemStatus.SetActive(false);

        tmp_itemStock.text = currentStock.ToString(); // display the stock of an item
        tmp_itemCost.text = s0_items.itemCost.ToString(); // display the cost of an item
        v3_spawnAreaCorners = new Vector3[4];
        rt_spawnArea.GetWorldCorners(v3_spawnAreaCorners); // puts the world co-ords of the 4 corners in the array v3_spawnAreaCorners
    }

    private void Update()
    {
        currentTime -= 1 * Time.deltaTime;

        if (currentTime <= 0)
        {
            currentStock = s0_items.itemStock; // restock the items
            currentTime = restockTime; // reset the restock timer
        }

        if (currentStock > 0)
        {
            go_itemImage.SetActive(true);
            go_itemStatus.SetActive(false);
        }
        else if (currentStock <= 0)
        {
            go_itemImage.SetActive(false);
            tmp_itemStatus.text = "OUT OF STOCK";
            go_itemStatus.SetActive(true);
        }
        else if (cs_playerManager.playerCurrency < s0_items.itemCost)// player cant afford
        {
            go_itemImage.SetActive (false);
            tmp_itemStatus.text = "CANT AFFORD";
            go_itemStatus.SetActive(true);
        }

        tmp_itemStock.text = currentStock.ToString();
        tmp_restockTimer.text = currentTime.ToString("00"); // shows current time
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        xPos = Random.Range(v3_spawnAreaCorners[0].x, v3_spawnAreaCorners[2].x); // random x-value within the spawnAreaCorners
        yPos = Random.Range(v3_spawnAreaCorners[0].y, v3_spawnAreaCorners[2].y); // random y-value within the spawnAreaCorners

        v3_spawnArea = new Vector3(xPos, yPos);

        if (currentStock > 0 && cs_playerManager.playerCurrency >= s0_items.itemCost) // ADD LATER THE PLAYER MUST HAVE ENOUGH MONEY
        {
            Instantiate(go_item, v3_spawnArea, Quaternion.identity, go_itemSpawn.transform); // spawn the item within the spawnArea, parent it to itemSpawn

            currentStock -= 1; // reduce current stock
            cs_playerManager.playerCurrency -= s0_items.itemCost;
        }
        
        if(cs_playerManager.playerCurrency < s0_items.itemCost)
        {
            go_itemImage.SetActive(false);
            tmp_itemStatus.text = "CANT AFFORD";
            go_itemStatus.SetActive(true);
        }
    }
}
