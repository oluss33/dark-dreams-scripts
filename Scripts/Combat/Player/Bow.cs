using Unity.Mathematics;
using UnityEngine;

public class Bow : MonoBehaviour
{

    Vector2 direction;

    [SerializeField] private GameObject arrow;
    [SerializeField] private float launchForce;
    [SerializeField] private Transform shotPoint;

    public GameObject point;

    void Start()
    {
        
    }

    void Update()
    {
       /* if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }*/
    }

    private void Shoot()
    {
        GameObject _arrow = Instantiate(arrow, shotPoint.position, shotPoint.rotation);
        _arrow.GetComponent<Rigidbody2D>().linearVelocity = transform.right * launchForce;
    }
}
