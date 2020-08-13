using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summoner : MonoBehaviour {
    Healthbar healthbar;
    SummonerController controller;
    SpriteRenderer spriteRenderer;
    Animator animator;
    protected int health = 30;
    protected Color color;

    public virtual void Awake() {
        controller = GetComponent<SummonerController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        healthbar = GetComponentInChildren<Healthbar>();
    }

    public virtual void Start() {
        color = spriteRenderer.color;
    }

    public void Summon(Tile tile) {
        controller.Summon(tile);
    }

    public void CastSpell() {
        controller.CastSpell();
    }

    public IEnumerator CastStasis() {
        yield return StartCoroutine(controller.CastStasis());
    }

    public IEnumerator CastStockPile() {
        yield return StartCoroutine(controller.CastStockPile());
    }

    public bool GetAnimationDone() {
        return controller.GetDoneAnimation();
    }

    public bool GetDone() {
        return !controller.GetRoutineRunning();
    }

    public void FlyingCardDone(FlyingCard flyingCard) {
        controller.SetRoutineRunning(false);
        Destroy(flyingCard.gameObject);
    }

    public virtual IEnumerator TakeDamage(int damage) {
        animator.SetTrigger("isHurt");
        health -= damage;
        healthbar.SetHealth(health);
        yield return StartCoroutine(FlashRed());
        if (health < 0) {
            yield return StartCoroutine(Die());
        }
    }

    public virtual IEnumerator Die() {
        Debug.Log("Implement summoner death");
        yield break;
    }

    public int GetHealth() {
        return health;
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
