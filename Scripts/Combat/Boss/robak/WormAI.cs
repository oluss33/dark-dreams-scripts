using System;
using Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using Combat.Boss;
using Scripts;
using Scripts.CameraExtras;
using Scripts.Combat;
using Scripts.Combat.Enemy;
using Scripts.Management;
using Scripts.Mobs;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;
using Scripts.Enemy;
using Scripts.Combat.Player;

public class WormAI : MonoBehaviour, IBoss
{

    public BossArena arena;

    int previousAttack = 0;
                            // 0 - rolling
                            // 1 - seeking
                            // 2 - rocks
                            // 3 - horizontal
    public Vector2 arenaMiddle;
    public Transform middle;
    public Transform jumpMiddle;
    GameObject player;
    public GameObject positionSetter;
    Animator anim;
    public bool isAttacking;

    public Dictionary<string, Vector2> attackStartPositions;

    public float maxCooldownTime = 5f;
    public float cooldownTime = 0;

    public float shakeIntensity;
    public float shakeTime;

    private Enemy enemy;
    private EnemyHealth health;

    private bool shouldMove = false;

    private Vector2 dir;
    private Vector3 targetPosition;

    public EnemyWeapon weapon;
    
    public GameObject rockPrefab;
    public GameObject groundBreakParticles;
    public Transform head;

    public GameObject[] corners;

    public AnimationCurve animCurve;

    public int phase = 1;
    
    // audioclips
    private AudioSource source;
    private AudioClip phaseClip;
    private AudioClip earthQuakeClip;
    private AudioClip verticalClip;

    private Stun stun;

    private BezierCurve curve;

    private Rigidbody2D rb;

    private Transform pointA, pointB, pointC, pointD;

    private Vector2 startPos;

    private Attack playerAttack;

    private bool indicatorShown = false;

    private bool attackChoosen = false;

    [SerializeField] private float indicatorTime;

    
    private void Start()
    {
        
        arenaMiddle = positionSetter.transform.position;
        player = Player.main.gameObject;
        
        anim = GetComponent<Animator>();
        health = GetComponent<EnemyHealth>();
        enemy = GetComponent<Enemy>();
        stun = GetComponent<Stun>();
        source = GetComponent<AudioSource>();
        rb = positionSetter.GetComponent<Rigidbody2D>();

        phaseClip = Resources.Load<AudioClip>("Sounds/Worm/phase_test");
        earthQuakeClip = Resources.Load<AudioClip>("Sounds/Worm/worm_earthquake");
        verticalClip = Resources.Load<AudioClip>("Sounds/Worm/worm_vertical");

        health.dmg += OnDealDamage;

        stun.onStunActivated += OnStunActivated;

        startPos = positionSetter.transform.position;

        health.onDie += OnDie;

        playerAttack = player.GetComponent<Attack>();

        playerAttack.currentEnemy = GetComponent<Enemy>();
    }

    private void OnDie()
    {
        UpdatePosition();
        anim.SetBool("isDead", true);
    }

    #region Sounds
    public void PhaseSound()
    {
        source.PlayOneShot(phaseClip);
    }

    public void StunSound()
    {
        EarthQuakeSound();
    }

    public void EarthQuakeSound()
    {
        source.PlayOneShot(earthQuakeClip);
    }

    public void VerticalSound()
    {
        source.PlayOneShot(verticalClip);
    }
    #endregion
    
    private void OnStunActivated()
    {
        if (stun.activated) return;
        if(!stun.activated) {
            stun.StunMyself();
        }
        stun.activated = true;
        weapon.enabled = false;

        print("stun");

        Vector3 localPos = player.transform.position;

        positionSetter.transform.position = localPos;

        float sizeX = transform.localScale.x;

        foreach (var param in anim.parameters)
        {
            if (param.type == AnimatorControllerParameterType.Trigger)
            {
                anim.ResetTrigger(param.name);
            }
        }
        print("worm: stun");

        anim.SetBool("stun", true);
        weapon.activated = false; // todo: check
    }

    private void OnDealDamage()
    {
        if (health.currentHp <= (health.maxHp / 2))
        {
            if (phase == 2) return;
            
            Debug.Log("Phase 1 has been finished");
            phase = 2;
        }
        else
        {
            Debug.Log("I'm still in phase 1");
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            ShowRightIndicator();
        }
        if(Input.GetKeyDown(KeyCode.T))
        {
            ShowLeftIndicator();
        }
        
        if (cooldownTime > 0) cooldownTime -= Time.deltaTime;
        if (cooldownTime <= 0) 
        { 
            cooldownTime = 0; 
            if (stun.activated)
            {
                // zabezpieczenie
                anim.SetBool("stun", false);
                stun.activated = false;
            }
        }

        if (curve != null)
        {
            if (!curve.reached) curve.QuadraticCurve();
            else curve = null;
        }
        
        if (stun.activated)
        {
            cooldownTime = maxCooldownTime;
         //   Stun();
            
        }

        if (!isAttacking && cooldownTime == 0 && !stun.activated)
        {
            Attack();
        }
       /* if (allowedVisceral)
        {
            player.GetComponent<Animator>().SetBool("canVisc", true);
        }
        else
        {
            player.GetComponent<Animator>().SetBool("canVisc", false);
        }*/
    }

    void Attack()
    {

        
        if (stun.activated) return;
        if(health.isDead) { anim.Play("static");  return; }
        if(attackChoosen) return;
        isAttacking = true;
        //RollingAttack();
        indicatorShown = false;
        attackChoosen = true;
        curve = null;
        switch (previousAttack)
        {
            case 0:
               if(phase == 1) RollingAttack();
               else HorizontalAttack();
               //Seek();
               Debug.Log("@Worm: Rolling Attack");
               break;
            case 1:
                if (phase == 1) HeadAttack();
                else JumpingAttack();
                Debug.Log("@Worm: Head Attack");
                break;
            case 2:
                if(phase == 1) RocksAttack();
                else TailAttack();
                break;
            case 3:
                if(phase == 1) VerticalAttack();
                else JumpingAttack();

                break;
        }
        
        
        
        
        // set next attack
        int _previous = previousAttack;

        do
        {
            previousAttack = Random.Range(0, 4);
        } while (previousAttack == _previous);
        
        // set cooldown
        cooldownTime = maxCooldownTime;
    }

    private void JumpingAttack()
    {
        print("Jumping Attack");
        Transform playerPos = player.transform;
        pointB = jumpMiddle;
        pointC = playerPos;

        if (player.transform.position.x > middle.position.x)
        {
            pointA = corners[1].transform;
            pointD = corners[2].transform;
            anim.SetTrigger("jump_right");
            if(!indicatorShown) { ShowRightIndicator(); indicatorShown = true; }
        }

        else
        {
            pointA = corners[2].transform;
            pointD = corners[1].transform;
            anim.SetTrigger("jump_left");
            if(!indicatorShown) { ShowLeftIndicator(); indicatorShown = true; }
        }
        positionSetter.transform.position = pointA.position;

        curve = new BezierCurve(positionSetter.transform, pointA, jumpMiddle, playerPos, pointD, animCurve);
        Debug.Log(curve);
    }
    

    private void RocksAttack()
    {
        ResetPosition();

        if (player.transform.position.x > middle.position.x)
        {
            // TOOD: DO SPRAWDZENIA
            anim.SetTrigger("rocks_right");
            if(!indicatorShown) { ShowLeftIndicator(); indicatorShown = true;}
        }
        else
        {
            anim.SetTrigger("rocks_left");
            if(!indicatorShown) { ShowRightIndicator(); indicatorShown = true; }
        }
    }

    public void Rocks()
    {
        int amount = Random.Range(3, 5);

        for (int i = 0; i <= amount; i++)
        {
            Debug.Log("@Worm: Throwing Rocks");
            
            GameObject rock = Instantiate(rockPrefab, head.position, Quaternion.identity);

            Rigidbody2D _rb = rock.GetComponent<Rigidbody2D>();

            float modifier = 0.5f;

            Vector2 direction = player.transform.position - head.position;
            
            float randomY = Random.Range(0.5f, 4.5f);
            float randomX = Random.Range(1.5f, 2.5f);

            Vector2 force = new Vector2(direction.x * randomX, direction.y * randomY) * modifier;

           
            /*
            
            Vector2 force = (Vector2.up * randomY + Vector2.right * randomX) * power;
            
            */
            
            
            _rb.AddForce(force, ForceMode2D.Impulse);
        }
    }
    

    private bool zoomed = false;

    private void RollingAttack()
    {
  //      previousAttack = Random.Range(1, 3);

        var vcm = CinemachineEffects.instance;
        positionSetter.transform.position = middle.position;
  
        float currentSize = arena.camFightSize;
        if (!zoomed) {
            vcm.Zoom(50, .5f, () => vcm.Zoom(currentSize, .15f));
            zoomed = true;
        }
  
        if (player.transform.position.x > middle.position.x)
        {
            anim.SetTrigger("roll_right");
            if(!indicatorShown)
            {
                ShowRightIndicator();
                indicatorShown = true;
            }
        }
        else
        {
            anim.SetTrigger("roll_left");
            if(!indicatorShown) {
                ShowLeftIndicator();
                indicatorShown = true;
            }
        }
    }

    private float speed = 2f;
    
    private void HeadAttack()
    {
        if(cooldownTime > 0) return;
        //previousAttack = Functions.RandomNumberExcept(0, 3, 1);

        Vector3 pos;
        float xModifier = 40;
        
        // check position
        if (player.transform.position.x > middle.position.x)
        {
            anim.SetTrigger("seek_right");
            pos = player.transform.position - new Vector3(xModifier, 0, 0);
            
        }
        else
        {
            anim.SetTrigger("seek_left");
            pos = player.transform.position + new Vector3(xModifier, 0, 0);

        }
        
        if(!indicatorShown) {
            ShowBottomIndicator();
            indicatorShown = true;
        }
        positionSetter.transform.position = pos;
        
/*
        transform.Translate(dir * speed * Time.deltaTime);
        */


    }

    public void SeekingAttack()
    {
        //previousAttack = Random.Range(2, 3);
        //anim.SetTrigger("seeking_attack");
        
        dir = player.transform.position - transform.position;


        float xModifier = 10;
        float yModifier = 10;

        dir.x += xModifier;
        dir.y += yModifier;
        
        //dir.Normalize();
        
        shouldMove = true;

    }

    public void DisableSeek()
    {
        shouldMove = false;
        ResetPosition();
    }

    void VerticalAttack()
    {
        if (cooldownTime > 0) return;
     //   previousAttack = Random.Range(0, 2);
        
        TimeManager.instance.DoSlowMotion(0.4f, 0.4f);

        var position = player.transform.position;
        Instantiate(groundBreakParticles, new Vector2(position.x, position.y - 10), Quaternion.identity);
        
        int rr = Random.Range(0, 2);
        if(rr == 0)
        {
            UpdatePosition();
            anim.SetTrigger("up");
            if(!indicatorShown)
                {
                    ShowAboveIndicator();
                    indicatorShown = true;
                }
        }
        else
        {
            UpdatePosition();
            anim.SetTrigger("down");
            if(!indicatorShown) {
                ShowBottomIndicator();
                indicatorShown = true;
            }
        }
    }

    void HorizontalAttack()
    {
     //   previousAttack = 1;

        var vcm = CinemachineEffects.instance;
        positionSetter.transform.position = middle.position;
        ResetPosition();
        print("horizontal attack");
  
        float currentSize = arena.camFightSize;
        if (!zoomed) {
            vcm.Zoom(50, .5f, () => vcm.Zoom(currentSize, .15f));
            zoomed = true;
        }

        if (player.transform.position.x > middle.position.x)
        {
            anim.SetTrigger("right");
            if(!indicatorShown) { ShowRightIndicator(); indicatorShown = true; }

        }
        else
        {
            anim.SetTrigger("left");
            if(!indicatorShown) { ShowLeftIndicator(); indicatorShown = true; }
        }
    }

    void TailAttack()
    {
        float xModifier = 40;
        Vector3 pos;

        if (player.transform.position.x > middle.position.x)
        {
            if(!indicatorShown) {
                ShowRightIndicator();
                indicatorShown = true;
            }
            anim.SetTrigger("tail_right");
            pos = player.transform.position - new Vector3(xModifier, 0, 0);

        }
        else
        {
            if(!indicatorShown) {
                ShowLeftIndicator();
                indicatorShown = true;
            }
            anim.SetTrigger("tail_left");
            pos = player.transform.position + new Vector3(xModifier, 0, 0);

        }
        positionSetter.transform.position = pos;
     //   previousAttack = 2;
    }

    
    private bool shaked = false;

    public void ShakeCamera()
    {
        if (shaked) return;
        enemy.ShakeCamera();
        shaked = true;
    }
    
    public void ResetAttackingStatus()
    {
        isAttacking = false;
        weapon.activated = false;
    }

    public void UpdatePosition()
    {
        print("UpdatePosition");
        positionSetter.transform.position = player.transform.position;
    }

    public void ResetPosition()
    {
        positionSetter.transform.position = middle.position;
    }

    public void StunActivation()
    {
        /*// TODO: Fix it
        positionSetter.transform.position = Player.main.transform.position;
        anim.SetTrigger("stunActivated");*/
    }

    public void ShowRightIndicator()
    {
        if(cooldownTime > 0) return;
        playerAttack.ShowIndicator(playerAttack.rightIndicator, indicatorTime);
    }

    public void ShowLeftIndicator()
    {
        if(cooldownTime > 0) return;
        playerAttack.ShowIndicator(playerAttack.leftIndicator, indicatorTime);
    }

    public void ShowAboveIndicator()
    {
        if(cooldownTime > 0) return;
        playerAttack.ShowIndicator(playerAttack.aboveIndicator, indicatorTime);

    }

    public void ShowBottomIndicator()
    {
        if(cooldownTime > 0) return;
        playerAttack.ShowIndicator(playerAttack.bottomIndicator, indicatorTime);
    }

    public void ResetAttacked()
    {
        stun.activated = false;
        anim.SetBool("stun", false);
        weapon.activated = false;
        zoomed = false;
        shaked = false;
        stun.shaked = false;
        weapon.performedStun = false;
        indicatorShown = false;
        attackChoosen = false;
    }

    public void ResetBoss()
    {
        if(health.isDead) return;
        anim.Rebind();
        anim.Update(0);
        cooldownTime = 0;

        positionSetter.transform.position = startPos;
        ResetAttacked();
        ResetAttackingStatus();
        phase = 1;

        anim.Play("static");

        EnemyUI.instance.HideEnemy(GetComponent<EnemyHealth>());

        gameObject.SetActive(false);
    }
}
