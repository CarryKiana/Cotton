using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemName itemName;

    public void ItemClicked() {
        InventoryManager._instance.AddItem(itemName);
        this.gameObject.SetActive(false);
    }
}
