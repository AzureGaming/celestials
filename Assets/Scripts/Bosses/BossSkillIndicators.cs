using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossSkillIndicators : MonoBehaviour {
    public List<Sprite> indicators;
    public GameObject spawnLoc;
    SpriteRenderer spriteRenderer;

    private void Awake() {
        spriteRenderer = spawnLoc.GetComponent<SpriteRenderer>();
    }

    protected void SetIndicator(int index) {
        Debug.Log("Aldsaf");
        spriteRenderer.sprite = indicators[index];
    }

    protected void ClearIndicator() {
        spriteRenderer.sprite = null;
    }
}
