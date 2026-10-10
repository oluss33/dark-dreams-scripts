using UnityEngine;

public class Indicator : MonoBehaviour
{

    [SerializeField] private Transform playerHead;
    [SerializeField] private float offsetX;
    [SerializeField] private float offsetY;
    void Update()
    {
        transform.position = playerHead.position + new Vector3(offsetX, offsetY, 0);
    }
}
