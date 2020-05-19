using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Summon : MonoBehaviour {
    Animator animator;
    SpriteRenderer spriteRenderer;
    SummonController controller;

    public virtual void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        controller = GetComponent<SummonController>();
        animator = GetComponent<Animator>();
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

    public virtual bool DoneMoving() {
        return controller.DoneMoving();
    }

    public virtual bool DoneAttacking() {
        return controller.DoneAttacking();
    }

    public int GetRange() {
        return controller.GetRange();
    }

    public void ActivateBarrier() {
        StartCoroutine(controller.ActivateBarrier());
    }

    public IEnumerator ActivateMarch() {
        yield return StartCoroutine(controller.ActivateMarch());
    }

    public int GetId() {
        return controller.GetId();
    }

    public int GetOrder() {
        return controller.GetOrder();
    }
}
