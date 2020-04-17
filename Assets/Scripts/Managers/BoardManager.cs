using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BoardManager : MonoBehaviour {
    Tile[][] grid;
    GameObject tilePrefab;
    Board board;
    Tile[] tiles;
    UIManager uiManager;
    int cardOrder = 0;
    int stageLimit = 3;
    int rowLimit = 3;
    int attackCounter = 0;
    int attacksToWaitFor = 0;

    private void Awake() {
        uiManager = FindObjectOfType<UIManager>();
        board = GetComponent<Board>();
        tiles = GetComponentsInChildren<Tile>();
        grid = new Tile[4][];
        grid[0] = new Tile[3];
        grid[1] = new Tile[3];
        grid[2] = new Tile[3];
        // boss cells
        grid[3] = new Tile[3];
    }

    void Start() {
        int tileCounter = 0;
        for (int row = 0; row < rowLimit; row++) {
            for (int column = 0; column < stageLimit; column++) {
                Tile tile = tiles[tileCounter];
                tile.column = column;
                tile.row = row;
                tile.tag = "[Tile] Summon";
                grid[column][row] = tile;
                tileCounter++;
            }
        }

        for (int i = 0; i < 3; i++) {
            grid[3][i] = null;
        }
    }

    public void IncrementCardOrder(int value) {
        cardOrder += value;
    }

    public int GetCardOrder() {
        return cardOrder;
    }

    public void DetectTileState() {
        foreach (Tile tile in tiles) {
            tile.SetSelectState();
        }
    }

    public void HandleSummoned() {
        foreach (Tile tile in tiles) {
            tile.SetNeutralState();
        }
        uiManager.SetLocationSelectionPrompt(false);
    }

    public IEnumerator ResolveMovePhase() {
        for (int col = 0; col < stageLimit; col++) {
            yield return StartCoroutine(MoveSummonsInColumn(col));
        }
    }

    public IEnumerator ResolvePowerAbilityPhase() {
        yield break;
    }

    public IEnumerator ResolveAttackPhase() {
        for (int stage = stageLimit - 1; stage > 0; stage--) {
            Tile[] tiles = grid[stage];
            System.Array.Sort(tiles, (tileX, tileY) => {
                Summon summonX = tileX.GetComponentInChildren<Summon>();
                Summon summonY = tileY.GetComponentInChildren<Summon>();
                int x, y;
                x = summonX ? summonX.getOrder() : -1;
                y = summonY ? summonY.getOrder() : -1;
                return x - y;
            });
            foreach (Tile tile in tiles) {
                Summon summon = tile.GetComponentInChildren<Summon>();
                if (summon) {
                    yield return StartCoroutine(summon.Attack());
                }
            }
        }
        yield return new WaitUntil(() => CheckAttacksAreDone());
        yield break;
    }

    public IEnumerator MoveSummonFromTile(Summon summon, Tile tile) {
        // Implement: different types of movement
        int startCol = tile.column;
        int endCol = tile.column += 1;

        if (grid.ElementAtOrDefault(endCol) == null) {
            Debug.LogWarning("Column " + endCol + " does not exist in grid.");
            yield break;
        }

        if (grid[endCol].ElementAtOrDefault(tile.row) == null) {
            StartCoroutine(summon.Die());
            yield break;
        }

        if (!grid[endCol][tile.row].CompareTag("[Tile] Summon")) {
            Debug.Log("Cell exists but is not a valid tile");
            yield break;
        }

        yield return StartCoroutine(summon.WalkToTile(grid[endCol][tile.row]));
    }

    public bool CheckCanHitBoss(Tile currentPos, int range) {
        return CheckWithinRange(range, currentPos.column, currentPos.row);
    }

    public void IncrementAttackCounter() {
        attackCounter += 1;
    }

    public void IncrementAttacksToWaitFor() {
        attacksToWaitFor += 1;
    }

    bool CheckAttacksAreDone() {
        if (attackCounter >= attacksToWaitFor) {
            attackCounter = 0;
            attacksToWaitFor = 0;
            return true;
        }

        return false;
    }

    IEnumerator MoveSummonsInColumn(int column) {
        foreach (Tile tile in grid[column]) {
            Summon summon = tile.GetComponentInChildren<Summon>();
            if (summon) {
                yield return InitCoroutine(summon.Move());
            }

        }
        yield return null;
    }

    IEnumerator InitCoroutine(IEnumerator coroutine) {
        StartCoroutine(coroutine);
        yield break;
    }

    bool CheckWithinRange(int range, int colIndex, int rowIndex) {
        int targetColIndex = colIndex + range;
        if (targetColIndex < grid.Length && rowIndex < grid.Length) {
            if (grid[targetColIndex][rowIndex] == null) {
                return true;
            }
        } else {
            Debug.LogWarning("Range check is invalid: Range - " + range + " colIndex: " + colIndex + " rowIndex: " + rowIndex);
        }
        return false;
    }
}
