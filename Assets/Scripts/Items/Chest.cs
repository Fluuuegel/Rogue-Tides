using Unity.Mathematics;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    public bool isOpened { get; private set; }

    public string chestID { get; private set; }

    public GameObject itemPrefab;
    public Sprite openedSprite;
    void Start()
    {
        chestID ??= GlobalHelper.GenerateUniqueID(gameObject);
        // Register the chest with the save controller
    }

    // Implementation of IInteractable interface
    public void Interact()
    {
        if (!IsInteractable()) return;
        OpenChest();
    }

    public bool IsInteractable()
    {
        return !isOpened;
    }

    private void OpenChest() {
        SetOpened(true);

        // if (itemPrefab != null)
        // {   
        //     GameObject droppedItem = Instantiate(itemPrefab, transform.position + Vector3.down, Quaternion.identity);
        //     droppedItem.GetComponent<BounceEffect>().StartBounce();
        // }
    }
    public void SetOpened(bool opened)
    {
        if (isOpened = opened)
        {
            // Change the sprite to the opened one
            GetComponent<SpriteRenderer>().sprite = openedSprite;
        }
    }

}
