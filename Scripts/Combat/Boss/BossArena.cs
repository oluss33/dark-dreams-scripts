using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Cinemachine;
using Combat.Boss;
using Scripts.Combat.Enemy;
using UnityEngine;
using Scripts.Player;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;
using Scripts.CameraExtras;
using Scripts;

public class BossArena : MonoBehaviour
{
    public GameObject detector;
    public GameObject barrier;
    public GameObject boss;
    //public Vector3 bossSpawnPosition;

    public CinemachineVirtualCamera cam;
    public float camFightSize;

    private Slider s;
    
    private void Start()
    {
        detector.SetActive(true);
        barrier.SetActive(false);
        boss.SetActive(false);
    }

    public void ResetArena()
    {
        BossStop();
        detector.SetActive(true);

        foreach (var _barrier in barrier.GetComponentsInChildren<ArenaBarrier>())
        {
            _barrier.Reset();
        }
        barrier.SetActive(false);
        boss.SetActive(false); // zabezpieczenie
    }

    public void ActivateBarrier()
    {
        barrier.SetActive(true);
    }

    public void BossStart()
    {
        print("boss start kurwa");
        detector.SetActive(false);
        boss.SetActive(true);

        IBoss iBoss = boss.GetComponent<IBoss>();
        
        iBoss.PhaseSound();

        Player.main.StartFight();
        if (EnemyUI.instance != null)
        {

            s = EnemyUI.instance.ShowEnemy(boss.GetComponent<EnemyHealth>());
        }

        CinemachineEffects.instance.startSize = camFightSize; // TOOD: Reset it after fight!!!!
        cam.m_Lens.OrthographicSize = camFightSize;

    }

    public void BossStop()
    {
        Debug.Log("wywoluje BossStop");
        Player.main.StopFight();
        barrier.SetActive(false);
        cam.LookAt = Player.main.transform;
        cam.Follow = Player.main.transform;
        cam.m_Lens.OrthographicSize = CinemachineEffects.instance.startSize;
    }
}
