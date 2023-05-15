using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BackgroundManager : MonoBehaviour, IDropHandler
{
    [SerializeField] public Vector3 itemStartingPosition;
    [SerializeField] public Transform itemStartingParent;
    public void OnDrop(PointerEventData eventData)
    {
        eventData.pointerDrag.transform.position = itemStartingPosition;
        eventData.pointerDrag.transform.SetParent(itemStartingParent);
    }
}
