using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hand : MonoBehaviour {
    Text text;
    [SerializeField] public List<int> handCardIds;
    CardManager cardManager;
    HandSlot[] handSlots;
    Text idsDisplay;

    private void Awake() {
        text = GetComponentInChildren<Text>();
        cardManager = FindObjectOfType<CardManager>();
        handSlots = GetComponentsInChildren<HandSlot>();
        idsDisplay = GameObject.Find("Ids").GetComponent<Text>();
    }

    private void Start() {
        handCardIds = new List<int>();
        foreach (HandSlot handSlot in handSlots) {
            handSlot.DestroyChildren();
        }
    }

    public IEnumerator DrawCard() {
        GameObject card = cardManager.CreateCard();
        foreach (HandSlot handSlot in handSlots) {
            if (handSlot.transform.childCount < 1) {
                handSlot.InsertCard(card);
                handCardIds.Add(card.GetComponent<Card>().id);
                break;
            }
        }
        UpdateIdsDisplay();
        yield break;
    }

    public void RemoveCard(int id) {
        int handSlotIndex = 0;
        foreach (HandSlot handSlot in handSlots) {
            // Unsure why GetComponentInChildren is undefined sometimes
            if (handSlot.GetComponentInChildren<Card>()?.id == id) {
                handCardIds.RemoveAt(handCardIds.FindIndex((handCardId => handCardId == id)));
                handSlot.DestroyChildren();
            }
            handSlotIndex++;
        }
        UpdateIdsDisplay();
    }

    void UpdateIdsDisplay() {
        idsDisplay.text = "";
        foreach (int handCardId in handCardIds) {
            idsDisplay.text += handCardId.ToString() + "\n";
        }
        text.text = "Hand Counter: " + handCardIds.Count;
    }
}
