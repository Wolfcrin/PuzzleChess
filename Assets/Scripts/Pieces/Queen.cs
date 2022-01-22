using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : BasePiece
{
    public override void Init(Cell newCell, Factions newFaction, Sprite newImage)
    {
        base.Init(newCell, newFaction, newImage);

        steps = new Vector3Int(7, 7, 7);   //newFaction == Factions.White ? new Vector3Int(7, 7,7) : new Vector3Int(0, -1, -1);

        //GetComponent<SpriteRenderer>().sprite = newImage;
    }
}
