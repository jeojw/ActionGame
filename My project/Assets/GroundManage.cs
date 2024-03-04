using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GroundManage : MonoBehaviour
{
    Tilemap Tile;
    List<Vector3Int> CrackedList;
    // Start is called before the first frame update
    void Start()
    {
        Tile = GetComponent<Tilemap>();
        CrackedList = new List<Vector3Int>();
        foreach (Vector3Int pos in Tile.cellBounds.allPositionsWithin)
        {
            CrackedList.Add(pos);
        }
    }

    //void OnDrawGizmos()
    //{

    //    RaycastHit2D hit = Physics2D.BoxCast(Pos_1.position, new Vector2(2.5f, 5.5f), 0f, Vector2.up, 0.5f, LayerMask.GetMask("Player"));

    //    Gizmos.color = Color.red;
    //    if (hit.collider != null)
    //    {
    //        Gizmos.DrawWireCube(Pos_1.position + Vector3.up * 0.5f, new Vector3(2.5f, 5.5f));
    //    }
    //}

    // Update is called once per frame

    public void Reset()
    {
        this.gameObject.SetActive(true);
        Tile.animationFrameRate = 0f;
    }
    void Update()
    {
        for (int i = 0; i < CrackedList.Count; i++)
        {
            RaycastHit2D hit = Physics2D.BoxCast(new Vector2(CrackedList[i].x, CrackedList[i].y), new Vector2(2.5f, 5.5f), 0f, Vector2.up, 5f, LayerMask.GetMask("Player"));
            if (hit.collider != null)
            {
                Tile.animationFrameRate = 1.5f;
                if (Tile.GetAnimationFrameCount(CrackedList[i]) >= 3)
                    this.gameObject.SetActive(false);
            }
            else
                Tile.animationFrameRate = 0f;
        }
        

        
    }
}
