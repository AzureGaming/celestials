using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour {
    Tile[][] grid;
    GameObject tilePrefab;
    Board board;
    Tile[] tiles;
    UIManager uiManager;

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
}
