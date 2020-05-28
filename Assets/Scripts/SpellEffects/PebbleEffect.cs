using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PebbleEffect : CardEffect {
    CardManager cardManager;

    private void Awake() {
        cardManager = FindObjectOfType<CardManager>();
    }

    public override void Apply() {
        cardManager.TrashCard(GetComponent<Card>());
    }
}
