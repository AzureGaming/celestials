using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SummonController))]
public class Summon : MonoBehaviour {
    int order;
    Animator animator;
    SpriteRenderer spriteRenderer;
    SummonController controller;

    public virtual void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        controller = GetComponent<SummonController>();
        animator = GetComponent<Animator>();
    }

    public virtual void Walk(int tiles) {
        controller.Walk(tiles);
    }

    public virtual void ExecuteAction() {
        controller.ExecuteAction();
    }

    public virtual void Attack(int damage) {
        controller.Attack(damage);
    }

    public void Die() {
        controller.Die();
    }

    //private void Start() {
    //    setOrder(boardManager.GetCardOrder());
    //    boardManager.IncrementCardOrder(1);
    //}

    //public int getOrder() {
    //    return order;
    //}

    //public void setOrder(int value) {
    //    order = value;
    //}
}
