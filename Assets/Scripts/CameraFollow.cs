using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed = 0.125f;

    public Vector3 offset;

    float xMin, xMax, zMin, zMax;

    LevelManager levelManager;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target == null) return;

        levelManager = GameObject.FindGameObjectWithTag("System").transform.Find("LevelManager").GetComponent<LevelManager>();

        xMin = levelManager.xMin;
        xMax = levelManager.xMax;
        zMin = levelManager.zMin;
        zMax = levelManager.zMax;

        Vector3 delta = target.position;

        if (delta.x > xMax) delta.x = xMax;

        if (delta.x < xMin) delta.x = xMin;

        if (delta.z > zMax) delta.z = zMax;

        if (delta.z < zMin) delta.z = zMin;

        Vector3 desiredPosition = new Vector3(delta.x, delta.y, delta.z) + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
