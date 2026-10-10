using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class CoinGenerator : MonoBehaviour
    {
        [SerializeField] int chance = 0;
        [SerializeField] Inventory.Item item;

        private void Start()
        {
            if(Random.Range(1, 100) <= chance + Player.Player.main.lucky)
            {
                //Inventory.ItemWorld.SpawnItemInWorld(transform.position, item);
            }
        }
    }
}
