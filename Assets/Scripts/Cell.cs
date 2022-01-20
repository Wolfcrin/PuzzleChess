using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    //Posicion en el tablero
    private Vector2Int _boardPosition    = Vector2Int.zero;

    //Collision
    private RectTransform _rectTransform = null;

    //La Pieza que esta dentro de esta celda
    //public BasePiece currentPiece        = null;

    //Imagen de sombreado cuando esta marcada la celda
    public Image outlineImage;

    public Vector2Int BoardPosition { get => _boardPosition; set => _boardPosition = value; }

    public void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    public void Init(Vector2Int boardPosition)
    {
        BoardPosition  = boardPosition;
    }    

    //public void RemovePiece()
    //{
    //    if (currentPiece != null)
    //        currentPiece.Kill();
    //}


}
