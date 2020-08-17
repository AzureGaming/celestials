using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossSkillIndicators : MonoBehaviour {
    public List<Sprite> indicators;
    public GameObject pos1;
    public GameObject pos2;
    SpriteRenderer spriteRenderer1;
    SpriteRenderer spriteRenderer2;

    private void Awake() {
        spriteRenderer1 = pos1.GetComponent<SpriteRenderer>();
        spriteRenderer2 = pos2.GetComponent<SpriteRenderer>();
    }

    protected void SetIndicator(int index) {
        if (spriteRenderer1.sprite) {
            spriteRenderer2.sprite = indicators[index];
        } else {
            spriteRenderer1.sprite = indicators[index];
        }
    }

    public void ClearIndicator() {
        spriteRenderer1.sprite = null;
        if (spriteRenderer2.sprite) {
            spriteRenderer1.sprite = spriteRenderer2.sprite;
            spriteRenderer2.sprite = null;
        }
    }
}
