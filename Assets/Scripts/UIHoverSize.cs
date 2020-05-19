using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIHoverSize : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    public GameState hoverTrigger;
    TurnManager turnManager;
    Vector3 startingScale;

    private void Awake() {
        turnManager = FindObjectOfType<TurnManager>();
    }

    private void Start() {
        startingScale = transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData) {
        if (turnManager.state == hoverTrigger) {
            transform.localScale = startingScale * 1.5f;
        }
    }

    public void OnPointerExit(PointerEventData eventData) {
        transform.localScale = startingScale;
    }
}
