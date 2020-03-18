using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {
    public CardObject cardObject;
    public System.Guid id;
    //SpriteRenderer spriteR;
    Image imageDisplay;
    TextMeshProUGUI nameDisplay;
    GameManager gameManager;
    TurnManager turnManager;
    Sprite artwork;
    new string name;
    string description;
    int manaCost;
    int attack;
    Vector3 startingScale;
    Ray ray;
    RaycastHit hit;

    private void Awake() {
        //spriteR = GetComponent<SpriteRenderer>();
        imageDisplay = transform.Find("Artwork").GetComponent<Image>();
        nameDisplay = transform.Find("Name").GetComponent<TextMeshProUGUI>();
        gameManager = FindObjectOfType<GameManager>();
        turnManager = FindObjectOfType<TurnManager>();
    }

    private void Start() {
        startingScale = transform.localScale;
    }

    public void LoadCard(CardObject card) {
        name = card.name;
        description = card.description;
        artwork = card.artwork;
        manaCost = card.manaCost;
        attack = card.attack;
        id = System.Guid.NewGuid();

        imageDisplay.sprite = artwork;
        nameDisplay.text = name;
    }

    public void OnPointerEnter(PointerEventData eventData) {
        transform.localScale = startingScale * 2;
    }

    public void OnPointerExit(PointerEventData eventData) {
        transform.localScale = startingScale;
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (turnManager.state == GameState.MULLIGAN) {
            gameManager.SetMulligan(id);
        }
    }
}
