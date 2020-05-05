
using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SummonController))]
public class Summon : MonoBehaviour {
    public Entity entity;
    int order;
    Animator animator;
    SpriteRenderer spriteRenderer;
    SummonController controller;
    GameManager gameManager;
    Nullable<int> id;

    public virtual void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        controller = GetComponent<SummonController>();
        animator = GetComponent<Animator>();
        gameManager = FindObjectOfType<GameManager>();
    }

    public virtual void Start() {
        SetOrder(gameManager.GetNextCardOrder());
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

    public virtual int GetOrder() {
        return order;
    }

    public virtual void SetOrder(int value) {
        order = value;
    }

    public virtual bool DoneMoving() {
        return controller.DoneMoving();
    }

    public virtual bool DoneAttacking() {
        return controller.DoneAttacking();
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

    public void SetEffect(GameObject prefab) {
        controller.SetEffectPrefab(prefab);
    }

    public void RemoveEffect() {
        controller.RemoveEffectPrefab();
    }
}
