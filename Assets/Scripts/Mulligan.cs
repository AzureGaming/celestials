using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mulligan : MonoBehaviour {
    CardManager cardManager;

    private void Awake() {
        cardManager = FindObjectOfType<CardManager>();
    }

    public void AddCard(Card card) {
        card.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        card.transform.SetParent(transform);
    }
}
