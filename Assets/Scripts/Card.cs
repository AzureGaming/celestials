using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour {
    public CardObject cardObject;
    SpriteRenderer spriteR;
    GameManager gameManager;
    Sprite artwork;
    string id;
    new string name;
    string description;
    int manaCost;
    int attack;

    private void Awake() {
        spriteR = GetComponent<SpriteRenderer>();
        gameManager = FindObjectOfType<GameManager>();
    }

    void OnMouseUpAsButton() {
        gameManager.SetMulligan(id);
    }

    public void LoadCard(CardObject card) {
        name = card.name;
        description = card.description;
        artwork = card.artwork;
        manaCost = card.manaCost;
        attack = card.attack;
        id = System.Guid.NewGuid().ToString();

        spriteR.sprite = artwork;
    }
}
