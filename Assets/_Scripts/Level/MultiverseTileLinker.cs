using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "MultiverseTileLinker", menuName = "ScriptableObjects/Tiles/MultiverseTileLinker")]
public class MultiverseTileLinker : ScriptableObject
{
    [System.Serializable]
    public class LinkedTiles
    {
        public List<TileBase> linkedTiles;
    }

    public List<LinkedTiles> tileLinks;
}
