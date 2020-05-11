using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class SummonerController : MonoBehaviour {
    Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();    
    }

    public void Summon() {
        animator.SetTrigger("isCasting");
    }
}
