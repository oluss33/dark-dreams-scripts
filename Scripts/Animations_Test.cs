using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animations_Test : MonoBehaviour
{
    
    
    
    [SerializeField] private Animator anim;
    public void DisableFighting()
    {
        anim.SetBool("isFighting", false);
    }
}
