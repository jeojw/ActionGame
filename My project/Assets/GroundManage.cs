using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GroundManage : MonoBehaviour
{
    Tilemap Tile;
    HashSet<Vector3Int> CrackedList;
    // Start is called before the first frame update
    void Start()
    {
        Tile = GetComponent<Tilemap>();
        CrackedList = new HashSet<Vector3Int>();
        foreach (Vector3Int pos in Tile.cellBounds.allPositionsWithin)
        {
            CrackedList.Add(pos);
        }
    }

    // Update is called once per frame

    public void Reset()
    {
        this.gameObject.SetActive(true);
        Tile.animationFrameRate = 0f;
    }
    void Update()
    {
        foreach (Vector3Int pos in CrackedList)
        {
            RaycastHit2D hit = Physics2D.BoxCast(new Vector2(pos.x, pos.y), new Vector2(0.5f, 5.5f), 0f, Vector2.up, 5f, LayerMask.GetMask("Player"));
            if (hit.collider != null)
            {
                Tile.animationFrameRate = 1f;
                if (Tile.GetAnimationFrameCount(pos) >= 3)
                    this.gameObject.SetActive(false);
            }
            else
                Tile.animationFrameRate = 0f;
        }
    }
}
