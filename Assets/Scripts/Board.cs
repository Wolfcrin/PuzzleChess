using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public GameObject cellPrefabs;
    public Vector2Int boardSize = new Vector2Int(8, 8);

    public GameObject piece; 
    public Cell[,] _cells;

    private void Awake()
    {
        _cells = new Cell[boardSize.x, boardSize.y];
    }

    //Crear el boar 
    //TODO Generar con la posiciones de las piezas segun un array
    public void Generate()
    {
        for (int y = 0; y < boardSize.y; y++)
        {
            for (int x = 0; x < boardSize.x; x++)
            {
                // Create the cell
                //GameObject newCell = Instantiate(cellPrefabs, transform);
                GameObject newCell = Instantiate(cellPrefabs);
                newCell.transform.position = new Vector2(gameObject.transform.position.x + x, gameObject.transform.position.y + y);
                newCell.name = $"Cell {x} {y}";
                newCell.transform.SetParent(null);

                // Setup
                _cells[x, y] = newCell.GetComponent<Cell>();
                _cells[x, y].Init(new Vector2Int(x, y));
            }
        }
        for (int x = 0; x < 8; x += 2)
        {
            for (int y = 0; y < 8; y++)
            {
                // Offset for every other line
                int offset = (y % 2 != 0) ? 0 : 1;
                int finalX = x + offset;

                // Color
                _cells[finalX, y].GetComponent<SpriteRenderer>().color = new Color32(230, 220, 187, 255);
            }
        }



        // GameObject.FindObjectOfType<CellManager>().CreateCellPath(new Vector2Int(1, 1),0,1,0,7);
        // GameObject.FindObjectOfType<CellManager>().CreateCellPath(new Vector2Int(1, 1),0,-1,0,7);

        // GameObject.FindObjectOfType<CellManager>().CreateCellPath(new Vector2Int(1, 1),0, 0, 1, 7);
        // GameObject.FindObjectOfType<CellManager>().CreateCellPath(new Vector2Int(1, 1),0, 0, -1, 7);

        //GameObject.FindObjectOfType<CellManager>().CreateCellPath(new Vector2Int(1, 1), 1, 1, 7);
        //GameObject.FindObjectOfType<CellManager>().CreateCellPath(new Vector2Int(1, 1), -1,1, 7);

        //GameObject.FindObjectOfType<CellManager>().CreateCellPath(new Vector2Int(1, 1), -1,-1, 7);
        //GameObject.FindObjectOfType<CellManager>().CreateCellPath(new Vector2Int(1, 1), 1, -1, 7);


    }
}
