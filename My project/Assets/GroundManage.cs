using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GroundManage : MonoBehaviour
{
    Vector3Int gridPos;
    Tilemap Tile;
    public Grid grid;
    public Transform Pos_1;
    public Transform Pos_2;
    // Start is called before the first frame update
    void Start()
    {
        Tile = GetComponent<Tilemap>();
    }

    Vector3Int WorldToCell(Vector3 _Vector)
    {
        return new Vector3Int(0, 0, 0);
    }

    void OnDrawGizmos()
    {

        RaycastHit2D hit = Physics2D.BoxCast(Pos_1.position, new Vector2(2.5f, 5.5f), 0f, Vector2.up, 0.5f, LayerMask.GetMask("Player"));

        Gizmos.color = Color.red;
        if (hit.collider != null)
        {
            Gizmos.DrawWireCube(Pos_1.position + Vector3.up * 0.5f, new Vector3(2.5f, 5.5f));
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 world = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        gridPos = grid.WorldToCell(world);

        RaycastHit2D hit = Physics2D.BoxCast(Pos_1.position, new Vector2(2.5f, 5.5f), 0f, Vector2.up, 0.5f, LayerMask.GetMask("Player"));
        RaycastHit2D hit2 = Physics2D.BoxCast(Pos_2.position, new Vector2(2.5f, 5.5f), 0f, Vector2.up, 0.5f, LayerMask.GetMask("Player"));
        if (hit.collider != null)
        {
            Tile.animationFrameRate = 1.5f;
            if (Tile.GetAnimationFrameCount(new Vector3Int(230, 5, 0)) >= 3 ||
                Tile.GetAnimationFrameCount(new Vector3Int(233, 5, 0)) >= 3)
                this.gameObject.SetActive(false);

        }
        else
            Tile.animationFrameRate = 0f;

        Debug.Log(gridPos);
    }
}
