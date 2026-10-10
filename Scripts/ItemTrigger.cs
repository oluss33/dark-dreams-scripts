using System;
using Scripts;
using Scripts.Gameplay;
using Scripts.Inventory;
using Scripts.Player;
using UnityEngine;

public class ItemTrigger : IInteractable, IResettable
{
    public ItemDataHolder data;
    public int amount = 1;
    [SerializeField] private bool destroyAfterUse = true;
    private bool used = false;
    [SerializeField] private bool showAfterReset = false;

    public override void Interact()
    {
        if(used) return;
        Player.main.inventory.entityInventory.AddItem(data.data, amount); 
        if(!GameManager.Instance.openedInventory) { 
            Hints.instance.ShowHint(gameObject, "Inventory", "Press M to open your inventory", 2); 
            GameManager.Instance.openedInventory = true;
        }
        used = true;
        if(destroyAfterUse) Destroy(gameObject);
        else GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Interactable>().enabled = false; 
        GetComponent<Interactable>().used = true; 
        KeyHints.instance.HideInteractHint();
        this.enabled = false;
    }

    public void ResetState()
    {
        if(!showAfterReset) return;
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<Interactable>().enabled = true; 
        GetComponent<Interactable>().used = false; 
    }
}
