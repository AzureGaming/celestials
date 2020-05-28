using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockingCrystal : MonoBehaviour {
    Animator animator;
    bool isBreaking = false;
    bool isSpawning = true;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    public IEnumerator Break() {
        animator.SetTrigger("IsBroken");
        isBreaking = true;
        yield return new WaitUntil(() => !isBreaking);
        Destroy(gameObject);
    }

    public void OnBreakAnimationEventEnd() {
        isBreaking = false;
    }

    public void OnSpawnAnimationEventEnd() {
        isSpawning = false;
    }

    public bool GetIsSpawning() {
        return isSpawning;
    }
}
