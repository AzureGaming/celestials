using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardSnapToFutureSightDisplay : MonoBehaviour, IDropHandler {
    GameObject displayArea;
    private void Awake() {
        displayArea = FindObjectOfType<UIManager>().futureSightDisplay;
    }
    public void OnDrop(PointerEventData eventData) {
        eventData.pointerDrag.transform.SetParent(displayArea.transform.GetChild(0).transform);
        eventData.pointerDrag.gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}
