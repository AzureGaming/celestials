using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelestialRangerController : SummonController {
    public GameObject arrowPrefab;
    public override IEnumerator AttackRoutine(int range, int id) {
        if (CheckWithinRange(range, id)) {
            attackRoutineRunning = true;
            animator.SetTrigger("isAttacking");
            yield return new WaitUntil(() => !attackRoutineRunning);
            yield return StartCoroutine(boss.TakeDamage(entity.attack));
        }
    }

    public override void OnAttackAnimationEnd() {
        StartCoroutine(OnAttackAnimationEndRoutine());
    }

    IEnumerator OnAttackAnimationEndRoutine() {
        GameObject arrow = Instantiate(arrowPrefab, transform);
        ArrowTracking tracking = arrow.GetComponent<ArrowTracking>();
        yield return new WaitUntil(() => tracking.doneMoving);
        Destroy(arrow);
        base.OnAttackAnimationEnd();
    }
}
