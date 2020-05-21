using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderDrop : MonoBehaviour {
    Summoner summoner;
    Vector3 startPos;
    GameManager gameManager;
    float t = 0f;
    float speed = 1f;

    private void Awake() {
        summoner = FindObjectOfType<Summoner>();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Start() {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update() {
        transform.position = Vector3.Lerp(startPos, summoner.transform.position, t);
        t += speed * Time.deltaTime;
        if (transform.position == summoner.transform.position) {
            gameManager.SetWaitForCompletion(false);
            Destroy(gameObject);
        }
    }
}
