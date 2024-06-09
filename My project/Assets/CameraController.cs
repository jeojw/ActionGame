using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject Target;
    private Vector3 TargetPos;
    private Vector3 UpdatePos;
    private Vector3 AdjustPos;

    private float CameraYHalfSize;
    private float CameraXHalfSize;

    public float offsetX;
    public float offsetY;
    public float offsetZ;
    
    public float MinCameraX;
    public float MaxCameraX;
    public float MinCameraY;
    public float MaxCameraY;

    // Start is called before the first frame update
    void Start()
    {
        AdjustPos = new Vector3(0, 0, -10);
        CameraYHalfSize = Camera.main.orthographicSize;
        CameraXHalfSize = CameraYHalfSize * Camera.main.aspect;
    }

    void LimitCameraArea()
    {
        TargetPos = Target.transform.position + new Vector3(0, 5, 0);

        transform.position = Vector3.Lerp(transform.position, TargetPos + AdjustPos, Time.deltaTime * Target.GetComponent<PlayerControl>().speed);

        float clampX = Mathf.Clamp(transform.position.x, MinCameraX, MaxCameraX);
        float clampY = Mathf.Clamp(transform.position.y, MinCameraY, MaxCameraY);

        transform.position = new Vector3(clampX, clampY, -10);
    }
    void LateUpdate()
    {
        LimitCameraArea();
    }


}
