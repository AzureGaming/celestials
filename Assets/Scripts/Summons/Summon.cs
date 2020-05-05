
using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SummonController))]
public class Summon : MonoBehaviour {
    public Entity entity;
    Entity entityRef;
    int order;
    Animator animator;
    SpriteRenderer spriteRenderer;
    SummonController controller;
    BoardManager boardManager;
    GameManager gameManager;
    Nullable<int> id;

    public virtual void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        controller = GetComponent<SummonController>();
        animator = GetComponent<Animator>();
        boardManager = FindObjectOfType<BoardManager>();
        gameManager = FindObjectOfType<GameManager>();
    }

    public virtual void Start() {
        entityRef = Instantiate(entity);
        setOrder(boardManager.GetCardOrder());
        boardManager.IncrementCardOrder(1);
    }

    public virtual void Walk() {
        controller.Walk(entity.movementSpeed, GetId());
    }

    public virtual void ExecuteAction() {
        controller.ExecuteAction();
    }

    public virtual void Attack() {
        controller.Attack(entity.range, GetId());
    }

    public virtual void Die() {
        controller.Die();
    }

    public virtual int getOrder() {
        return order;
    }

    public virtual void setOrder(int value) {
        order = value;
    }

    public virtual bool DoneMoving() {
        return controller.DoneMoving();
    }

    public virtual bool DoneAttacking() {
        return controller.DoneAttacking();
    }

    public void SummonToTile(Tile tile) {
        GameObject summonObj = Instantiate(gameObject, tile.transform);
    }

    public int GetRange() {
        return entity.range;
    }

    public int GetId() {
        if (id == null) {
            id = gameManager.GetNextEntityId();
        }
        return (int)id;
    }
}
