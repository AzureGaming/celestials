using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBoulder : MonoBehaviour {
    Vector3 startLoc;
    Summoner summoner;
    float duration = 1f;

    private void Awake() {
        summoner = FindObjectOfType<Summoner>();
    }

    public IEnumerator Attack(Vector3 location, Summon summon) {
        yield return StartCoroutine(MoveRoutine(transform.position, location));
        summon.Die();
    }

    public IEnumerator Attack(Vector3 location, Summoner summoner) {
        yield return StartCoroutine(MoveRoutine(transform.position, location));
        summoner.TakeDamage(2);
    }

    IEnumerator MoveRoutine(Vector3 start, Vector3 destination) {
        for (float t = 0; t < duration; t += Time.deltaTime) {
            transform.position = Vector3.Lerp(start, destination, Mathf.Min(1, t / duration));
            yield return null;
        }
        Destroy(gameObject);
    }
}
