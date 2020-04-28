using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Entity : MonoBehaviour {
    public System.Guid id = System.Guid.NewGuid();
    public new string name;
    public int manaCost;
    public int attack;
    public int range;
    public CardType type;
    public int movementSpeed;
    public string description;
    public Sprite artwork;
    public int health = 1;

    //Vector3 startingScale;
    //GameManager gameManager;
    //TurnManager turnManager;

    //public void OnPointerEnter(PointerEventData eventData) {
    //    transform.localScale = startingScale * 2;
    //}

    //public void OnPointerExit(PointerEventData eventData) {
    //    transform.localScale = startingScale;
    //}

    //public void OnPointerClick(PointerEventData eventData) {
    //    if (turnManager.state == GameState.MULLIGAN) {
    //        gameManager.SetMulligan(id);
    //    }
    //}

    //public virtual void ActivateEffect() {
    //}

    //public virtual void Move() {
    //}

    //public virtual void Attack() {
    //}
}
