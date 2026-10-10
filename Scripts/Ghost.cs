using System.Collections;
using System.Collections.Generic;
using Scripts.Player.Movement;
using UnityEngine;

namespace Scripts {
    public class Ghost : MonoBehaviour
    {
        public GhostType ghostType;
        Player.Player player;

        void Start() {
            player = Player.Player.main;
        }

        void OnMouseDown() {
            PickupGhost();
        }

        void PickupGhost() {
            if(ghostType == GhostType.SPEED) {
                PlayerController.movements[2].speed += 2;
                PlayerController.movements[0].speed += 2;
            }
            if(ghostType == GhostType.ATTACK) {
             //   player.punchDmg += 10;
            }
            //if(ghostType == GhostType.ATTACKSPEED) {
            //    QTE.QTE.Instance.value += 0.2f;
            //}
            Destroy(gameObject);
        }
    }

    public enum GhostType { SPEED, ATTACK, ATTACKSPEED }
}
