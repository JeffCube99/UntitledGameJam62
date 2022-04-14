using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapManager : MonoBehaviour
{
    [SerializeField] private GameObject character;
    [SerializeField] private float switchDelay;
    [SerializeField] private List<Tilemap> tilemaps;
    [SerializeField] private List<GameObjectRuntimeSet> universeRuntimeSets;
    [SerializeField] private MultiverseTileLinker multiverseTileLinker;
    private Dictionary<TileBase, List<TileBase>> multiverseTileMap;
    public int currentUniverseIndex;

    private void Start()
    {
        Setup();
        SwapAllTiles(currentUniverseIndex);
    }

    public void Setup()
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

    private Dictionary<int, List<GameObject>> GetSizeToGameObjects(List<GameObject> gameObjects, Vector3Int startPosition, int maxSize)
    {
        Tilemap tilemap = tilemaps[0];
        Dictionary<int, List<GameObject>> sizeToGameObjects = new Dictionary<int, List<GameObject>>();
        foreach (GameObject gameObject in gameObjects)
        {
            Vector3Int gameObjectPosition = tilemap.WorldToCell(gameObject.transform.position);
            Vector3Int diff = gameObjectPosition - startPosition;
            int size = Mathf.Max(Mathf.Abs(diff.x), Mathf.Abs(diff.y));
            if (size > maxSize)
            {
                size = maxSize;
            }
            if (sizeToGameObjects.ContainsKey(size))
            {
                sizeToGameObjects[size].Add(gameObject);
            }
            else
            {
                sizeToGameObjects[size] = new List<GameObject>();
                sizeToGameObjects[size].Add(gameObject);
            }
        }
        return sizeToGameObjects;
    }

    void ReplaceTile(Vector3Int tilePosition, int universeIndex, Tilemap tilemap)
    {
        TileBase originalTile = tilemap.GetTile(tilePosition);
        if (originalTile != null)
        {
            tilemap.SetTile(tilePosition, multiverseTileMap[originalTile][universeIndex]);
        }
    }

    public void SwapAllTiles(int universeIndex)
    {
        currentUniverseIndex = universeIndex;
        foreach (Tilemap tilemap in tilemaps)
        {
            foreach (var position in tilemap.cellBounds.allPositionsWithin)
            {
                ReplaceTile(position, universeIndex, tilemap);
            }
        }
        for (int i = 0; i < universeRuntimeSets.Count; i++)
        {
            foreach (GameObject gameObject in universeRuntimeSets[i].items)
            {
                if (i == universeIndex)
                {
                    gameObject.SetActive(true);
                }
                else
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }

    public void SwapTilesStartingAtCharacterPosition(int universeIndex)
    {
        StartCoroutine(SwapTiles(character.transform.position, universeIndex));
    }

    IEnumerator SwapTiles(Vector3 characterPosition, int universeIndex)
    {
        int previousUniverseIndex = currentUniverseIndex;
        currentUniverseIndex = universeIndex;
        if (previousUniverseIndex == currentUniverseIndex)
        {
            yield break;
        }

        List<BoundsInt> tilemapBounds = new List<BoundsInt>();
        List<int> maxSizes = new List<int>();
        List<Vector3Int> startPositions = new List<Vector3Int>();
        foreach (Tilemap tilemap in tilemaps)
        {
            BoundsInt bounds = tilemap.cellBounds;
            tilemapBounds.Add(bounds);
            Vector3Int startPosition = tilemap.WorldToCell(characterPosition);
            startPositions.Add(startPosition);

            int maxX = Mathf.Max(Mathf.Abs(startPosition.x - bounds.x), Mathf.Abs(startPosition.x - bounds.xMax));
            int maxY = Mathf.Max(Mathf.Abs(startPosition.y - bounds.y), Mathf.Abs(startPosition.y - bounds.yMax));
            maxSizes.Add(Mathf.Max(maxX, maxY));
        }

        int maxSize = Mathf.Max(maxSizes.ToArray());

        Dictionary<int, List<GameObject>> sizeToRemoveGameObjects = GetSizeToGameObjects(universeRuntimeSets[previousUniverseIndex].items, startPositions[0], maxSize);
        Dictionary<int, List<GameObject>> sizeToLoadGameObjects = GetSizeToGameObjects(universeRuntimeSets[currentUniverseIndex].items, startPositions[0], maxSize);
        List<Vector3Int> perimeterPositions;
        for (int size = 0; size < maxSize + 1; size++)
        {
            for (int tilemapIndex = 0; tilemapIndex < tilemaps.Count; tilemapIndex++)
            {
                if (size <= maxSizes[tilemapIndex])
                {
                    perimeterPositions = GetPerimeterPositions(startPositions[tilemapIndex], size, tilemapBounds[tilemapIndex]);
                    foreach (Vector3Int position in perimeterPositions)
                    {
                        ReplaceTile(position, universeIndex, tilemaps[tilemapIndex]);
                    }
                }
            }
            if (sizeToRemoveGameObjects.ContainsKey(size))
            {
                foreach (GameObject gameObject in sizeToRemoveGameObjects[size])
                {
                    gameObject.SetActive(false);
                }
            }
            if (sizeToLoadGameObjects.ContainsKey(size))
            {
                foreach (GameObject gameObject in sizeToLoadGameObjects[size])
                {
                    gameObject.SetActive(true);
                }
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
