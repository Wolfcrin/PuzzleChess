using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CellState{
    None,
    Friendly,
    Enemy,
    Empty,
    OutOfBounds
}

public class Cell : MonoBehaviour
{
    //Posicion en el tablero
    private Vector2Int _boardPosition    = Vector2Int.zero;

    //Collision
    private RectTransform _rectTransform = null;

    //Image
    private SpriteRenderer _sprite = null;

    //La Pieza que esta dentro de esta celda
    public BasePiece currentPiece        = null;

    //Imagen de sombreado cuando esta marcada la celda
    public SpriteRenderer outlineImage;

    public Vector2Int BoardPosition { get => _boardPosition; set => _boardPosition = value; }

    public void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _sprite = GetComponent<SpriteRenderer>();
    }

    public void Init(Vector2Int boardPosition)
    {
        BoardPosition  = boardPosition;
    }  
    
    public void ChangeColor(Color newColor)
    {
        _sprite.color = newColor;
    }

    public void ShowOutline()
    {
        outlineImage.enabled = true;
    }

    public void HideOutline()
    {
        outlineImage.enabled = false;
    }

    //public void RemovePiece()
    //{
    //    if (currentPiece != null)
    //        currentPiece.Kill();
    //}


}
