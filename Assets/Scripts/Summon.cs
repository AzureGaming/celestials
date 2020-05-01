using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SummonController))]
public abstract class Summon : MonoBehaviour {
    int order;
    Animator animator;
    SpriteRenderer spriteRenderer;
    SummonController controller;
    BoardManager boardManager;
    Entity entity;

    public virtual void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        controller = GetComponent<SummonController>();
        animator = GetComponent<Animator>();
        boardManager = FindObjectOfType<BoardManager>();
        entity = GetComponent<Entity>();
    }

    public virtual void Walk() {
        controller.Walk();
    }

    public virtual void ExecuteAction() {
        controller.ExecuteAction();
    }

    public virtual void Attack() {
        controller.Attack();
    }

    public virtual void Die() {
        controller.Die();
    }

    public virtual void Start() {
        setOrder(boardManager.GetCardOrder());
        boardManager.IncrementCardOrder(1);
    }

    public virtual int getOrder() {
        return order;
    }

    public virtual void setOrder(int value) {
        order = value;
    }

    public virtual bool DoneMoving() {
        return !controller.movementRoutineRunning;
    }

    public virtual bool DoneAttacking() {
        return !controller.attackRoutineRunning;
    }

    public int GetRange() {
        return entity.range;
    }
}
