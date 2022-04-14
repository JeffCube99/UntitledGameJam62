using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapManager : MonoBehaviour
{
    [SerializeField] private GameObject character;
    [SerializeField] private float switchDelay;
    [SerializeField] private List<Tilemap> tilemaps;
    [SerializeField] private MultiverseTileLinker multiverseTileLinker;
    private Dictionary<TileBase, List<TileBase>> multiverseTileMap;
    private bool isSwapping;
    private List<bool> tilemapsDone;

    private void Start()
    {
        multiverseTileMap = new Dictionary<TileBase, List<TileBase>>();
        foreach (MultiverseTileLinker.LinkedTiles linkedTiles in multiverseTileLinker.tileLinks)
        {
            foreach (TileBase tile in linkedTiles.linkedTiles)
            {
                multiverseTileMap[tile] = linkedTiles.linkedTiles;
            }
        }

        foreach (Tilemap tilemap in tilemaps)
        {
            tilemap.CompressBounds();
        }
    }

    void ReplaceTile(Vector3Int tilePosition, int universeIndex, Tilemap tilemap)
    {
        TileBase originalTile = tilemap.GetTile(tilePosition);
        if (originalTile != null)
        {
            tilemap.SetTile(tilePosition, multiverseTileMap[originalTile][universeIndex]);
        }
    }

    public void SwapTilesStartingAtCharacterPosition(int universeIndex)
    {
        foreach(Tilemap tilemap in tilemaps)
        {
            Vector3Int startPosition = tilemap.WorldToCell(character.transform.position);
            StartCoroutine(SwapTiles(startPosition, universeIndex, tilemap));
        }
    }

    IEnumerator SwapTiles(Vector3Int startPosition, int universeIndex, Tilemap tilemap)
    {
        BoundsInt tilemapBounds = tilemap.cellBounds;
        List<Vector3Int> perimeterPositions;

        int maxX = Mathf.Max(Mathf.Abs(startPosition.x - tilemapBounds.x), Mathf.Abs(startPosition.x - tilemapBounds.xMax));
        int maxY = Mathf.Max(Mathf.Abs(startPosition.y - tilemapBounds.y), Mathf.Abs(startPosition.y - tilemapBounds.yMax));
        int maxSize = Mathf.Max(maxX, maxY);

        for (int size = 0; size < maxSize+1; size++)
        {
            perimeterPositions = GetPerimeterPositions(startPosition, size, tilemapBounds);
            foreach (Vector3Int position in perimeterPositions)
            {
                ReplaceTile(position, universeIndex, tilemap);
            }
            yield return new WaitForSeconds(switchDelay);
        }
    }

    public List<Vector3Int> GetPerimeterPositions(Vector3Int center, int size, BoundsInt tilemapBounds)
    {
        List<Vector3Int> perimeterPositions = new List<Vector3Int>();
        if (size == 0)
        {
            for (int z = tilemapBounds.z; z < tilemapBounds.zMax + 1; z++)
            {
                perimeterPositions.Add(new Vector3Int(center.x, center.y, z));
            }
            return perimeterPositions;
        }

        Vector3Int targetPosition = center;
        int xMin = center.x - size;
        int xMax = center.x + size;
        int yMin = center.y - size;
        int yMax = center.y + size;
        for (int z = tilemapBounds.z; z < tilemapBounds.zMax + 1; z++)
        {
            for (int x = xMin; x < xMax + 1; x++)
            {
                targetPosition = new Vector3Int(x, yMin, z);
                if (tilemapBounds.Contains(targetPosition))
                    perimeterPositions.Add(targetPosition);
                targetPosition = new Vector3Int(x, yMax, z);
                if (tilemapBounds.Contains(targetPosition))
                    perimeterPositions.Add(targetPosition);
            }
            for (int y = yMin + 1; y < yMax; y++)
            {
                targetPosition = new Vector3Int(xMin, y, z);
                if (tilemapBounds.Contains(targetPosition))
                    perimeterPositions.Add(targetPosition);
                targetPosition = new Vector3Int(xMax, y, z);
                if (tilemapBounds.Contains(targetPosition))
                    perimeterPositions.Add(targetPosition);
            }
        }
        return perimeterPositions;
    }

    public (List<Vector3Int>, List<Vector3Int>) GetAdjacentTilePositions(List<Vector3Int> tilePositions, List<Vector3Int> excludedPositions, BoundsInt tilemapBounds)
    {
        List<Vector3Int> adjacentPositions = new List<Vector3Int>();
        foreach (Vector3Int position in tilePositions)
        {
            for (int x = position.x - 1; x < position.x + 2; x++)
            {
                for (int y = position.y - 1; y < position.y + 2; y++)
                {
                    for (int z = tilemapBounds.z; z < tilemapBounds.zMax + 1; z++)
                    {
                        Vector3Int targetPosition = new Vector3Int(x, y, z);

                        if (!excludedPositions.Contains(targetPosition) &&
                            tilemapBounds.x <= x && x <= tilemapBounds.xMax &&
                            tilemapBounds.y <= y && y <= tilemapBounds.yMax)
                        {
                            adjacentPositions.Add(targetPosition);
                            excludedPositions.Add(targetPosition);
                        }
                    }
                }
            }
        }
        return (adjacentPositions, excludedPositions);
    }
}
