using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Board board;
    public CellManager cellManager;
    public PieceManager pieceManager;
    public Camera mainCamera;

    public Vector3 GetMousePosition()
    {
        var pos = new Vector3(Input.mousePosition.x, Input.mousePosition.y,
            mainCamera.WorldToScreenPoint(transform.position).z);

        //var mousePos = _cam.ScreenToWorldPoint(Input.mousePosition);
        var mousePos = mainCamera.ScreenToWorldPoint(pos);
        mousePos.z = 0;
        return mousePos;

    }

    private void Start()
    {
        board.Generate();
                
        pieceManager.CreatePiece(cellManager.GetCellByXY(0, 0), Factions.White, "wR", PieceManager.PiecesKey.Rook);
        pieceManager.CreatePiece(cellManager.GetCellByXY(1, 0), Factions.White, "wP", PieceManager.PiecesKey.Pawn);
        pieceManager.CreatePiece(cellManager.GetCellByXY(2, 0), Factions.White, "wKn", PieceManager.PiecesKey.Knight);
        pieceManager.CreatePiece(cellManager.GetCellByXY(4, 0), Factions.White, "wK", PieceManager.PiecesKey.King);
        pieceManager.CreatePiece(cellManager.GetCellByXY(5, 0), Factions.White, "wB", PieceManager.PiecesKey.Bishop);
        pieceManager.CreatePiece(cellManager.GetCellByXY(3, 0), Factions.White, "wQ", PieceManager.PiecesKey.Queen);
        
        pieceManager.CreatePiece(cellManager.GetCellByXY(6, 0), Factions.White, "wP", PieceManager.PiecesKey.Pawn);

        pieceManager.CreatePiece(cellManager.GetCellByXY(7, 0), Factions.White, "wR", PieceManager.PiecesKey.Rook);
    }
}
