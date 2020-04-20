using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {
    int health;
    SpriteRenderer spriteRenderer;
    Color color;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start() {
        health = 30;
        color = spriteRenderer.color;
    }

    public IEnumerator SetupBoss() {
        yield break;
    }

    public int getHealth() {
        return health;
    }

    public IEnumerator RunTurnRoutine() {
        Debug.Log("Implement Boss AI turn AI");
        yield break;
    }

    public IEnumerator TakeDamage(int damage) {
        health -= damage;
        yield return StartCoroutine(FlashRed());
        if (health < 0) {
            Debug.Log("Implement win scneario");
        }
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
