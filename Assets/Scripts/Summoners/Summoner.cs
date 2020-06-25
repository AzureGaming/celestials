using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SummonerController))]
public class Summoner : MonoBehaviour {
    SummonerController controller;
    SpriteRenderer spriteRenderer;
    Animator animator;
    protected int health = 30;
    protected Color color;

    public virtual void Awake() {
        controller = GetComponent<SummonerController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    public virtual void Start() {
        color = spriteRenderer.color;
    }

    public void Summon() {
        controller.Summon();
    }

    public void OnCastAnimationEventEnd() {

    }

    public virtual IEnumerator TakeDamage(int damage) {
        animator.SetTrigger("isHurt");
        health -= damage;
        yield return StartCoroutine(FlashRed());
        if (health < 0) {
            yield return StartCoroutine(Die());
        }
    }

    public virtual IEnumerator Die() {
        Debug.Log("Implement summoner death");
        yield break;
    }

    IEnumerator FlashRed() {
        int duration = 2;
        for (int t = 0; t < duration; t++) {
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(0.1f);
        }
        spriteRenderer.color = color;
        yield break;
    }
}
