using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class BoardManager : MonoBehaviour {
    Tile[][] grid;
    GameObject tilePrefab;
    Board board;
    Tile[] tiles;
    UIManager uiManager;
    int cardOrder = 0;

    private void Awake() {
        uiManager = FindObjectOfType<UIManager>();
        board = GetComponent<Board>();
        tiles = GetComponentsInChildren<Tile>();
        grid = new Tile[3][];
        grid[0] = new Tile[3];
        grid[1] = new Tile[3];
        grid[2] = new Tile[3];
    }

    void Start() {
        int columnLimit = 3;
        int rowLimit = 3;
        int tileCounter = 0;

        for (int row = 0; row < rowLimit; row++) {
            for (int column = 0; column < columnLimit; column++) {
                Tile tile = tiles[tileCounter];
                tile.column = column;
                tile.row = row;
                grid[column][row] = tile;
                tileCounter++;
            }
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

    public IEnumerator MoveSummonFromTile(Summon summon, Tile tile) {
        // Implement: different types of movement
        int startCol = tile.column;
        int endCol = tile.column += 1;

        if (grid.ElementAtOrDefault(endCol) == null) {
            Debug.LogWarning("Column " + endCol + " does not exist in grid.");
            yield break;
        }

        if (grid[endCol].ElementAtOrDefault(tile.row) == null) {
            Debug.Log("Implement: Summon is moved off the board, kill summon");
            yield break;
        }

        yield return StartCoroutine(summon.WalkToTile(grid[endCol][tile.row]));
    }
}
