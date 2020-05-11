using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeGolemController : SummonController {
    bool isRewind = false;
    bool skipMove = false;

    public override IEnumerator WalkRoutine(int tiles, int id) {
        Tile tileToMoveTo = boardManager.GetDestination(id, tiles);
        if (tileToMoveTo?.type == TileType.Boss && isRewind) {
            yield return StartCoroutine(DieRoutine());
        } else if (tileToMoveTo?.type == TileType.Boss) {
            skipMove = true;
            yield return StartCoroutine(Rewind());
        } else if (skipMove) {
            yield break;
        } else {
            yield return StartCoroutine(UpdatePositionRoutine(transform.position, tileToMoveTo));
        }
    }

    IEnumerator Rewind() {
        Tile destination = boardManager.GetFirstTileInRowIfValid(GetSummonId());
        if (destination == null) {
            Die();
        } else {
            isRewind = true;
            animator.SetBool("isRewind", true);
            yield return StartCoroutine(UpdatePositionRoutine(transform.position, destination));
            animator.SetBool("isRewind", false);
        }
    }
}
