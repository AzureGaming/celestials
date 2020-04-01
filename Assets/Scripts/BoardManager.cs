using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour {
    Tile[][] grid;
    GameObject tilePrefab;
    Board board;
    Tile[] tiles;

    private void Awake() {
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
                grid[column][row] = tiles[tileCounter];
                tileCounter++;
            }
        }
    }

    public IEnumerator InsertSummon(int column, int row, Card card) {
        Tile tile = grid[2][2];
        tile.Summon(card);
        Debug.Log("Inserted " + card + " at " + column + row);
        yield break;
    }

    public void DetectTileState() {
        foreach (Tile tile in tiles) {
            tile.UpdateStatus();
        }
    }
}
