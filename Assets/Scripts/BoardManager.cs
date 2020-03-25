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
        int column = 0;
        int row = 0;
        int columnLimit = 3;
        int rowLimit = 3;

        foreach (Tile tile in tiles) {
            grid[column][row] = tile;
            row++;
            column++;
            if (row > rowLimit) {
                row = 0;
            }
            if (column > columnLimit) {
                column = 0;
            }
        }
    }

    public IEnumerator InsertSummon(int column, int row, Card card) {
        Tile tile = grid[column][row];
        tile.Summon(card);
        Debug.Log("Inserted " + card + " at " + column + row);
        yield break;
    }
}
