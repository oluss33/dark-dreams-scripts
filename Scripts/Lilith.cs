using Scripts.Dialogues;
using UnityEngine;

namespace Scripts { 
    public class Lilith : MonoBehaviour
    {
        [SerializeField] private float activeDist;
        private Animator anim;
        private Player.Player p;

        private bool performed = false;

        void Start()
        {
            p = Player.Player.main;
            anim = GetComponent<Animator>();
        }
        public void Update()
        {
            if (!performed) {
                if (Vector2.Distance(transform.position, p.transform.position) <= activeDist)
                {
                    // active animation
                    anim.SetTrigger("initial");
                    performed = true;
                }
            }
        }

        public void Hide()
        {
            anim.SetTrigger("hide");
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.purple;
            Gizmos.DrawWireSphere(transform.position, activeDist);
        }
    }
}
