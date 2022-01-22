using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class BasePiece : MonoBehaviour //MonoBehaviour 
{
    protected GameManager _gameManager   = null;
    private PieceManager _pieceManager = null;

    //Estados
    public bool isMyFirstMove = true;
    private bool _isDragging = false;

    //Movimiento
    public Vector3Int steps = Vector3Int.zero;

    //Faccion a la que pertenece la pieza
    public Factions faction = 0;

    //Images
    //public Color32 imageColor = Color.clear;
    private SpriteRenderer _spriteRendered = null;

    //collision
    protected RectTransform rectTransform = null;

    //cell parent
    protected Cell originalCellParent = null; //celda original para volver en caso de un movimiento invalido
    protected Cell currentCellParent  = null; //celda actual donde esta
    protected Cell targetCell = null; //celda donde se quiere posicionar

    private void Awake()
    {
        _gameManager    = FindObjectOfType<GameManager>();
        _pieceManager   = FindObjectOfType<PieceManager>();
        _spriteRendered = GetComponent<SpriteRenderer>();
        rectTransform = GetComponent<RectTransform>(); 
    }

    public virtual void Init(
        Cell   newCell,
        Factions  newFaction,
        //Color  newColor,
        Sprite newImage
    )
    {
       faction = newFaction;
        //ChangeColor(newColor);
       ChangeImage(newImage);
       

       Place(newCell);

    }

    public void ChangeImage(Sprite newSprite)
    {
        _spriteRendered.sprite = newSprite;
        _spriteRendered.sortingOrder = 1;
    }

    public void ChangeColor(Color32 newColor)
    {
        _spriteRendered.color = newColor;

    }


    // Segun Unity
    // Reset is called when the user hits the Reset button in the Inspector's
    // context menu or when adding the component the first time.
    // This function is only called in editor mode.
    // Reset is most commonly used to give good default values in the Inspector.
    public void Reset()
    {
        Kill();

        isMyFirstMove = true;

        Place(originalCellParent);
    }

    //position 
    //POsicionar la primera vez
    public virtual void Place(Cell newCell)
    {
        // Cell donde me muevo
        currentCellParent  = newCell;
        originalCellParent = newCell;
        currentCellParent.currentPiece = this;

        // Mover
        transform.localPosition = newCell.transform.localPosition;
        gameObject.SetActive(true);
    }

    //movimiento Normal
    protected virtual void Move()
    {
        // Marcamos que ya movimos una vez por lo menos
        isMyFirstMove = false;

        // Si a celda tiene una pieza la removemos (comemos)
        targetCell.RemovePiece();

        // Clear current
        currentCellParent.currentPiece = null;

        // Intercambiamos las celdas
        currentCellParent = targetCell;
        currentCellParent.currentPiece = this;

        // Movemos en el escenario
        transform.position = currentCellParent.transform.localPosition;

        //movimos correctamente no necesitamos mas la targetCell
        targetCell = null;
    }

    public virtual void Kill()
    {
        //Limpiamos la celda actual
        currentCellParent.currentPiece = null;

        //removemos la pieza
        // TODO: crear un lugar donde almacenarlas
        gameObject.SetActive(false);
    }

    //Muestra el path de la celda
    //cada pieza debera setear como lo hace
    // normalmente todas usan esta sin sobrecargarla, porque mueven normal
    protected virtual void CheckPath()
    {
        // Horizontal
        _gameManager.cellManager.CreateCellPath(currentCellParent.BoardPosition, faction,  1, 0, steps.x);
        _gameManager.cellManager.CreateCellPath(currentCellParent.BoardPosition, faction, -1, 0, steps.x);

        // Vertical
        _gameManager.cellManager.CreateCellPath(currentCellParent.BoardPosition, faction, 0, 1, steps.y);
        _gameManager.cellManager.CreateCellPath(currentCellParent.BoardPosition, faction, 0, -1, steps.y);

        // Diagonal Arriba
        _gameManager.cellManager.CreateCellPath(currentCellParent.BoardPosition, faction, 1, 1, steps.z);
        _gameManager.cellManager.CreateCellPath(currentCellParent.BoardPosition, faction, -1, 1, steps.z);

        // Diagonal Abajo
        _gameManager.cellManager.CreateCellPath(currentCellParent.BoardPosition, faction, -1, -1, steps.z);
        _gameManager.cellManager.CreateCellPath(currentCellParent.BoardPosition, faction, 1, -1, steps.z);


    }

    #region Eventos Normales entre Piezas

    private void OnMouseDown()
    {
        if(!_isDragging)
            CheckPath();
    }

    private void OnMouseDrag()
    {
        _isDragging = true;

        transform.localPosition = _gameManager.GetMousePosition();

        // Vamos verificando las celdas por las que pasa el mouse
        foreach (Cell cell in _gameManager.cellManager.lightedCells)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(cell.RectTransform, Input.mousePosition,_gameManager.mainCamera))
            {
                // Si la celda es valida la guardamos y terminamos el bucle
                targetCell = cell;
                break;
            }

            // si el mouse esta sobre una celda que no esta sombreada marcamos que no tenemos
            // un objetivo valido.
            targetCell = null;
        }
    }

    private void OnMouseUp()
    {
        _isDragging = false;

        // Ocultamos las celdas
        _gameManager.cellManager.HideCells();

        // La celda seleccionada no es correcta(No es valida) 
        // devolvemos la pieza a su posicion inicial
        if (!targetCell)
        {
            transform.localPosition = currentCellParent.gameObject.transform.localPosition;
            return;
        }

        // Mover a la nueva celda
        Move();

        

        // Final del turno tenemos que invertir 
        // para que el otro jugador pueda hacer un movimiento.
        //mPieceManager.SwitchSides(mColor);
    }


    #endregion

}