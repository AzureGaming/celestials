using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeGolemController : SummonController {
    bool isRewind = false;
    bool skipMove = false;
    Tile rewindDestination = null;

    public override IEnumerator WalkRoutine(int tiles, int id) {
        if (skipMove) {
            skipMove = !skipMove;
            yield break;
        }

        Tile tileToMoveTo = boardManager.GetDestination(id, tiles);
        if (tileToMoveTo?.type == TileType.Boss && isRewind) {
            yield return StartCoroutine(DieRoutine());
        } else if (tileToMoveTo?.type == TileType.Boss) {
            skipMove = true;
            Rewind();
        } else {
            yield return StartCoroutine(UpdatePositionRoutine(transform.position, tileToMoveTo));
        }
    }

    public void OnRewindAnimationEventEnd() {
        StartCoroutine(EndRewindAnimationRoutine());
    }

    void Rewind() {
        rewindDestination = boardManager.GetFirstTileInRowIfValid(GetSummonId());
        if (rewindDestination == null) {
            Die();
        } else {
            isRewind = true;
            animator.SetBool("isRewind", true);
            movementRoutineRunning = true; // board manager will proceed too early otherwise
        }
    }

    IEnumerator EndRewindAnimationRoutine() {
        movementSpeed = 1f;
        yield return StartCoroutine(UpdatePositionRoutine(transform.position, rewindDestination));
        movementSpeed = 2f;
        rewindDestination = null;
        animator.SetBool("isRewind", false);
    }
}
