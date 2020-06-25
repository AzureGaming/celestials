using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ThrowBoulderSkill : MonoBehaviour {
    public Animator animator;
    public EarthElementalSkillIndicators skillIndicators;
    public AttackQueueManager queueManager;
    public BoardManager boardManager;
    public GameObject rockPrefab;
    public GameObject rockSpawner;
    Summoner summoner;
    public bool isCasting = false;
    List<Tile> pendingAttacks = new List<Tile>();
    int numberOfTargets = 2;

    private void Awake() {
        summoner = FindObjectOfType<Summoner>();
    }

    public void OnCastAnimationEnd() {
        isCasting = false;
    }

    public IEnumerator CastSkill(AttackQueueManager.AttackCommand command) {
        isCasting = true;
        animator.SetTrigger("Attack1");
        yield return new WaitUntil(() => !isCasting);
        LoadTargets(command.coords);
        yield return StartCoroutine(ThrowBoulders());
        skillIndicators.ClearIndicator();
        //queueManager.RefreshIndicators();
        pendingAttacks.Clear();
        Debug.Log("Done");
    }

    public void QueueSkill() {
        skillIndicators.SetBoulderThrow();
        queueManager.Queue(new AttackQueueManager.AttackCommand(EarthElemental.Moves.ROCKTHROW, CalculateTargets()));
    }

    public int[][] CalculateTargets() {
        List<Tile[]> rows = boardManager.GetRandomRows(numberOfTargets);
        if (rows.Count > numberOfTargets || rows.Count < numberOfTargets) {
            Debug.LogWarning("Rows queued do not match number of targets" + numberOfTargets);
        }
        int[] list1 = new int[] { };
        int[] list2 = new int[] { };
        int[][] coords = new int[][] { list1, list2 };
        int i = 0;
        foreach (Tile[] row in rows) {
            if (Array.Exists(row, (Tile tile) => tile.GetSummon())) {
                foreach (Tile tile in row.Reverse()) {
                    if (tile.GetSummon()) {
                        coords[i] = new int[] { tile.column, tile.row };
                        break;
                    }
                }
            } else {
                // Default
                Debug.Log("Set indicator summoner");
                coords[i] = new int[] { row[0].column, row[0].row };
            }
            i++;
        }

        return coords;
    }

    void LoadTargets(int[][] coords) {
        pendingAttacks = new List<Tile>();
        foreach (int[] coord in coords) {
            pendingAttacks.Add(boardManager.GetTile(coord[0], coord[1]));
        }

        if (pendingAttacks.Count != 2) {
            Debug.LogWarning("Pending attacks is invalid" + pendingAttacks.Count);
        }
    }

    IEnumerator ThrowBoulders() {
        Debug.Log("Throw boulders" + pendingAttacks.Count);
        foreach (Tile tile in pendingAttacks) {
            GameObject rock = Instantiate(rockPrefab, rockSpawner.transform);
            Summon summon = tile.GetSummon();
            if (summon) {
                yield return StartCoroutine(rock.GetComponent<ThrowBoulder>().Attack(summon.transform.position, summon));
            } else {
                yield return StartCoroutine(rock.GetComponent<ThrowBoulder>().Attack(summoner.transform.position, summoner));
            }
            tile.SetAttackIndicator(false);
        }
    }
}
