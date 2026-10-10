using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDetector : MonoBehaviour
{
    public BossArena arena;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")){
            arena.BossStart();
        }
    }
}
