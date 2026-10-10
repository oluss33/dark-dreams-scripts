using System.Linq;
using Combat.Boss;
using Scripts.Combat.Enemy;
using Scripts.Enemy;
using UnityEngine;

namespace Scripts {

    public interface IResettable
    {
        public abstract void ResetState();
    }

    public class RespawnManager : MonoBehaviour
    {
        public static void ResetWorld()
        {
            print("resetuje swiat!!");
            IBoss[] bosses = FindObjectsByType<MonoBehaviour>().OfType<IBoss>().ToArray();

            Player.Player.main.ResetPlayer();

            foreach(IBoss boss in bosses)
            {
                boss.ResetBoss();
            }
            foreach(AI ai in FindObjectsByType<AI>())
            {
                ai.ResetAI();
            }

            foreach(BossArena arena in FindObjectsByType<BossArena>())
            {
                arena.ResetArena();
            }

            foreach(BossActivator activator in FindObjectsByType<BossActivator>())
            {
                activator.Reset();
            }

            foreach(var resetable in FindObjectsByType<MonoBehaviour>().OfType<IResettable>().ToArray())
            {
                resetable.ResetState();
            }
        }
    }
}