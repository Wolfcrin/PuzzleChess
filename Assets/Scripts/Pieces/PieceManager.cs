using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


//Esta clase es la encargada de la creacion de las piezas
//Y de contener los tipos de piezas
// efectos raros 
// Promover el peon
// Intercambiar que faccion esta jugando luego de cada movimiento
public enum Factions
{
    White,
    Black
}

public class PieceManager : MonoBehaviour
{
    public GameObject piecePrefab;

    //la lista de piezas
    // Todo que se guarde por faccion
    //private List<BasePiece> _whitePieces = null;
    //private List<BasePiece> _blackPieces = null;
    //private List<BasePiece> _promotedPieces = null;
    

    private Dictionary<String, String> PiecesImages = new Dictionary<String, String>()
    {
        {"bP", "chess_0" },  // peon negro
        {"bR", "chess_3" },  // torre negro
        {"bKn","chess_1" },  // caballero negro
        {"bB", "chess_2" },  // alfil negro
        {"bK", "chess_5" },  // rey negro
        {"bQ", "chess_4" },  // reina negro
        {"wP", "chess_6" },  // peon blanco
        {"wR", "chess_9" },  // torre blanco
        {"wKn","chess_7" },  // caballero blanco
        {"wB", "chess_8" },  // alfil blanco
        {"wK", "chess_11" }, // rey blanco
        {"wQ", "chess_10" }  // reina blanco
    };



    public enum PiecesKey { 
        Pawn,
        Rook,
        Knight,
        Bishop,
        King,
        Queen
    }


    private Dictionary<PiecesKey, Type> PiecesList = new Dictionary<PiecesKey, Type>()
    {
        {PiecesKey.Pawn, typeof(Pawn) },       // peon
        {PiecesKey.Rook, typeof(Rook) },       // torre
        {PiecesKey.Knight, typeof(Knight) },   // caballero
        {PiecesKey.Bishop, typeof(Bishop) },   // alfil
        {PiecesKey.King, typeof(King) },       // rey
        {PiecesKey.Queen, typeof(Queen ) }     // reina
    };

    public BasePiece CreatePiece(
        Cell cell,
        Factions faction,
        //Color color,
        string imageKey,
        PiecesKey pieceTypeKey)
    {
        // Creamos una nueva pieza
        GameObject newPieceObject = Instantiate(piecePrefab);
        //newPieceObject.transform.SetParent(transform);
        newPieceObject.transform.SetParent(null);

        // Seteamos la escala y la rotacion
        newPieceObject.transform.localScale = new Vector3(1, 1, 1);
        newPieceObject.transform.localRotation = Quaternion.identity;

        // Le damos el comportamiento a la nueva pieza

        string pieceImageName = PiecesImages[imageKey];
        Sprite image = Resources.LoadAll<Sprite>("chess").ToList().Find(s => s.name == pieceImageName);

        BasePiece newPiece = (BasePiece)newPieceObject.AddComponent(PiecesList[pieceTypeKey]);  
        newPiece.Init(
            cell, 
            faction,
            //color, 
            image);

        newPiece.enabled = true;

        return newPiece;
    }

    //Activa las piezas para poder usarlas
    //private void SetInteractive(List<BasePiece> allPieces, bool value)
    //{
    //    foreach (BasePiece piece in allPieces)
    //        piece.enabled = value;
    //}

    public void PromotePiece(Pawn pawn, Cell cell, Factions faction)
    {
        // Asesinamos al Peon :(
        pawn.Kill();
        
        //obtenemos la reina correspondiente
        string imagenKey = faction == Factions.White ? "wQ" : "bQ"; 

        // Creamos la nueva reina
        BasePiece promotedPiece = CreatePiece(cell,faction,imagenKey,PiecesKey.Queen);

        // Posicionamos la pieza
        promotedPiece.Place(cell);

        // Agregar la pieza promovida a una lista
        //TODO:       
        //mPromotedPieces.Add(promotedPiece);
    }
}
