using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {
    public Entity entity;
    Image imageDisplay;
    TextMeshProUGUI nameDisplay;
    TextMeshProUGUI manaDisplay;
    TextMeshProUGUI attackDisplay;
    TextMeshProUGUI descriptionDisplay;
    Vector3 startingScale;

    private void Awake() {
        imageDisplay = transform.Find("Artwork").GetComponent<Image>();
        nameDisplay = transform.Find("Name").GetComponent<TextMeshProUGUI>();
        manaDisplay = transform.Find("Mana").GetComponentInChildren<TextMeshProUGUI>();
        attackDisplay = transform.Find("Attack").GetComponentInChildren<TextMeshProUGUI>();
        descriptionDisplay = transform.Find("Description").GetComponent<TextMeshProUGUI>();
    }

    private void Start() {
        startingScale = transform.localScale;
        imageDisplay.sprite = entity.artwork;
        nameDisplay.text = entity.name;
        manaDisplay.text = entity.manaCost.ToString();
        attackDisplay.text = entity.attack.ToString();
        descriptionDisplay.text = entity.description;
    }

    public void OnPointerEnter(PointerEventData eventData) {
        transform.localScale = startingScale * 2;
    }

    public void OnPointerExit(PointerEventData eventData) {
        transform.localScale = startingScale;
    }

    public void OnPointerClick(PointerEventData eventData) {
        //    if (turnManager.state == GameState.MULLIGAN) {
        //        gameManager.SetMulligan(id);
        //    }
    }

    public void ActivateEffect() {
        //    effect?.Apply();
    }

    public void SummonAt(Tile tile) {
        if (tile.GetComponentInChildren<Summon>()) {
            Debug.LogWarning("Overwriting summon at column, row: " + tile.column + " " + tile.row);
        }
        Instantiate(entity.summonPrefab, tile.transform);
    }

    public new CardType GetType() {
        return entity.type;
    }

    public Entity GetEntity() {
        return entity;
    }

    public int GetManaCost() {
        return entity.manaCost;
    }
}
