using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    [SerializeField] private Transform t;
    [SerializeField] private float rotationSpeed = 5f;

    private Vector2 previousNormal = Vector2.up;
    
    void Update()
    {
        var hit = Physics2D.Raycast(t.position, Vector2.down, 10, LayerMasks.instance.groundMask);
        if (hit.transform != null)
        {
            // Wygładzanie normalnej
            Vector2 smoothNormal = Vector2.Lerp(previousNormal, hit.normal, 0.2f);
            previousNormal = smoothNormal;

            Quaternion targetRot = Quaternion.FromToRotation(Vector3.up, smoothNormal);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, 360 * Time.fixedDeltaTime);
        }
    }
}
