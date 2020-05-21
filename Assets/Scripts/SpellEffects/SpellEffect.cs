﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpellEffect : MonoBehaviour {
    protected Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    public virtual IEnumerator Activate() {
        Debug.Log("Spell effect acitvate");
        animator.SetTrigger("Active");
        yield break;
    }

    public virtual void Deactivate() {
        animator.SetTrigger("Inactive");
    }

    public void OnDestroyAnimationEnd() {
        gameObject.SetActive(false);
    }
}