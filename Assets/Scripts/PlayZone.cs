using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayZone : MonoBehaviour, IDropHandler {
    public CardSnapToFutureSightDisplay futureSightCardSnap;
    BoardManager boardManager;
    Player player;
    Hand hand;

    private void Awake() {
        boardManager = FindObjectOfType<BoardManager>();
        hand = FindObjectOfType<Hand>();
        player = FindObjectOfType<Player>();
        futureSightCardSnap = GetComponent<CardSnapToFutureSightDisplay>();
    }

    private void Start() {
        futureSightCardSnap.enabled = false;
    }

    public void OnDrop(PointerEventData eventData) {
        Draggable droppedObject = eventData.pointerDrag.GetComponent<Draggable>();
        Card card = droppedObject.GetComponent<Card>();
        if (droppedObject != null) {
            if (card.GetManaCost() > player.GetMana()) {
                eventData.pointerDrag.transform.SetParent(hand.transform);
            } else {
                eventData.pointerDrag.transform.SetParent(transform);
                card.GetComponent<CanvasGroup>().alpha = 0;
                boardManager.PlayCard(card);
            }
        }
        eventData.pointerDrag.gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}
