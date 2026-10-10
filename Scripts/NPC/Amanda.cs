using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.NPC
{
    public class Amanda : MonoBehaviour
    {
        Animator anim;
        [SerializeField] public SpriteRenderer spriteRenderer;
        [SerializeField] public GameObject amanda_boxoff;

        public bool withoutCardBoard = false;
        
        void Start()
        {
            anim = GetComponent<Animator>();
            amanda_boxoff.SetActive(false);
        }

        public void Test()
        {
            TakeOffBox(GameObject.FindObjectOfType<Amanda>());
        }

        public void TakeOffBox(Amanda amanda)
        {
            Debug.Log("robie cos bardzo waznego xd");
            amanda.spriteRenderer.enabled = false;
            amanda.amanda_boxoff.SetActive(true);
            amanda.withoutCardBoard = true;
        }
    }
}
