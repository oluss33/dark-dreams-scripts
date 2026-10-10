using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerResetter : MonoBehaviour
{
    public void ResetTrigger(string triggerName)
    {
        GetComponent<Animator>().ResetTrigger(triggerName);
    }

    public void PlayerReset()
    {
        GetComponent<Animator>().ResetTrigger("Parry");
        GetComponent<Animator>().ResetTrigger("Attack");
        //  GetComponent<Animator>().ResetTrigger("dodger");
        //  GetComponent<Animator>().ResetTrigger("dodgel");
        GetComponent<Animator>().ResetTrigger("TakeHit");
    }
}
