using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Healthbar : MonoBehaviour {
    public Sprite[] sprites;
    public GameObject[] bars;
    public HealthValue healthValue;
    int health = 30;
    List<Sprite> greenBars = new List<Sprite>();
    List<Sprite> yellowBars = new List<Sprite>();
    List<Sprite> orangeBars = new List<Sprite>();
    List<Sprite> redBars = new List<Sprite>();

    private void Awake() {
        healthValue = GetComponentInChildren<HealthValue>();
    }

    private void Start() {
        foreach (Sprite sprite in sprites.Take(4)) {
            greenBars.Add(sprite);
        }

        foreach (Sprite sprite in sprites.Skip(4).Take(4)) {
            yellowBars.Add(sprite);
        }

        foreach (Sprite sprite in sprites.Skip(8).Take(4)) {
            orangeBars.Add(sprite);
        }

        foreach (Sprite sprite in sprites.Skip(12).Take(4)) {
            redBars.Add(sprite);
        }

        UpdateSprites();
        healthValue.SetValue(health);
    }

    public void TakeDamage(int value) {
        health -= value;
        UpdateSprites();
        healthValue.SetValue(health);
    }

    void UpdateSprites() {
        if (health <= 30 && health > 25) {
            GetBarSpriteRenderer(0).sprite = greenBars[0];
            GetBarSpriteRenderer(1).sprite = greenBars[1];
            GetBarSpriteRenderer(2).sprite = greenBars[2];
            GetBarSpriteRenderer(3).sprite = greenBars[1];
            GetBarSpriteRenderer(4).sprite = greenBars[2];
            GetBarSpriteRenderer(5).sprite = greenBars[3];
        } else if (health <= 25 && health > 20) {
            GetBarSpriteRenderer(0).sprite = yellowBars[0];
            GetBarSpriteRenderer(1).sprite = yellowBars[1];
            GetBarSpriteRenderer(2).sprite = yellowBars[2];
            GetBarSpriteRenderer(3).sprite = yellowBars[1];
            GetBarSpriteRenderer(4).sprite = yellowBars[2];
            GetBarSpriteRenderer(5).sprite = redBars[3];
        } else if (health <= 20 && health > 15) {
            GetBarSpriteRenderer(0).sprite = yellowBars[0];
            GetBarSpriteRenderer(1).sprite = yellowBars[1];
            GetBarSpriteRenderer(2).sprite = yellowBars[2];
            GetBarSpriteRenderer(3).sprite = yellowBars[1];
            GetBarSpriteRenderer(4).sprite = redBars[2];
            GetBarSpriteRenderer(5).sprite = redBars[3];
        } else if (health <= 15 && health > 10) {
            GetBarSpriteRenderer(0).sprite = orangeBars[0];
            GetBarSpriteRenderer(1).sprite = orangeBars[1];
            GetBarSpriteRenderer(2).sprite = orangeBars[1];
            GetBarSpriteRenderer(3).sprite = redBars[2];
            GetBarSpriteRenderer(4).sprite = redBars[2];
            GetBarSpriteRenderer(5).sprite = redBars[3];
        } else if (health <= 10 && health > 5) {
            GetBarSpriteRenderer(0).sprite = orangeBars[0];
            GetBarSpriteRenderer(1).sprite = orangeBars[1];
            GetBarSpriteRenderer(2).sprite = redBars[2];
            GetBarSpriteRenderer(3).sprite = redBars[2];
            GetBarSpriteRenderer(4).sprite = redBars[2];
            GetBarSpriteRenderer(5).sprite = redBars[3];
        } else if (health <= 5 && health > 0) {
            GetBarSpriteRenderer(0).sprite = redBars[0];
            GetBarSpriteRenderer(1).sprite = redBars[2];
            GetBarSpriteRenderer(2).sprite = redBars[2];
            GetBarSpriteRenderer(3).sprite = redBars[2];
            GetBarSpriteRenderer(4).sprite = redBars[2];
            GetBarSpriteRenderer(5).sprite = redBars[3];
        } else {
            GetBarSpriteRenderer(0).sprite = redBars[3];
            GetBarSpriteRenderer(0).flipX = true;
            GetBarSpriteRenderer(1).sprite = redBars[2];
            GetBarSpriteRenderer(2).sprite = redBars[2];
            GetBarSpriteRenderer(3).sprite = redBars[2];
            GetBarSpriteRenderer(4).sprite = redBars[2];
            GetBarSpriteRenderer(5).sprite = redBars[3];
        }
    }

    SpriteRenderer GetBarSpriteRenderer(int index) {
        return bars[index].GetComponent<SpriteRenderer>();
    }
}
