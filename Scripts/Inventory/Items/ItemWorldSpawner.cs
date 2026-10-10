using System;
using Unity.Mathematics;
using UnityEngine;

namespace Scripts.Inventory
{
    public class ItemWorldSpawner : MonoBehaviour
    {
        public static ItemWorldSpawner instance { get; private set; }

        private void Awake()
        {
            instance = this;
        }

        public GameObject itemPrefab;
        
        public void SpawnItem(Item item, Vector3 position)
        {
            
            Debug.Log("spawning new item");
            
            GameObject newItem = Instantiate(itemPrefab, new Vector3(position.x, position.y, 7), quaternion.identity);
            
            ItemWorld itemWorld = newItem.GetComponent<ItemWorld>();

            ItemDataHolder ih = new ItemDataHolder();
            ih.data = item.data;

            itemWorld.data = ih;
            itemWorld.amount = item.amount;

           // itemWorld.GetComponent<SpriteRenderer>().sprite = item.data.icon;
       
            
            Setup(itemWorld.data.data, itemWorld.GetComponent<BoxCollider2D>(), itemWorld.gameObject);
        }
        
       public void Setup(ItemData data, BoxCollider2D col, GameObject obj)
        {
          //  Debug.Log("ustawiam");
            obj.GetComponent<SpriteRenderer>().sprite = data.icon;
          //  Debug.Log(data.name);
            switch (data.name)
            {
                case "Coin":
                    col.size = new Vector2(1, 1);
                    obj.transform.localScale = new Vector3(3.5f, 3.5f, 0);
                    break;
                case "Torch":
                    col.size = new Vector2(1, 4.5f);
                    obj.transform.localScale = new Vector3(3.3f, 2f, 0);
                    break;
                case "Sword":
                    col.size = new Vector2(1, 5.8f);
                    obj.transform.localScale = new Vector3(2.1f, 1.98f, 0);
                    break;
            }
        }
    }
}