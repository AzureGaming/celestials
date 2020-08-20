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
            yield return StartCoroutine(DieRoutine(false, false));
        } else if (tileToMoveTo?.type == TileType.Boss) {
            skipMove = true;
           yield return StartCoroutine(Rewind());
        } else if (tileToMoveTo?.GetSummon() != null) {
            yield break;
        } else if (tileToMoveTo?.GetComponentInChildren<BlockingCrystal>()) {
            yield break;
        } else {
            walkAudio.loop = true;
            walkAudio.Play();
            yield return StartCoroutine(UpdatePositionRoutine(transform.position, tileToMoveTo));
            walkAudio.Stop();
        }
    }

    public void OnRewindAnimationEventEnd() {
        StartCoroutine(EndRewindAnimationRoutine());
    }

    IEnumerator Rewind() {
        rewindDestination = boardManager.GetFirstTileInRowIfValid(GetId());
        if (rewindDestination == null) {
            yield return StartCoroutine(Die(false, false));
        } else {
            powerAudio.Play();
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
