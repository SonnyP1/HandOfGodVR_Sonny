using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] Image ButtonImage;
    Sprite _emptySprite;
    InventoryComponent _item;

    private void Start()
    {
        if(ButtonImage == null)
        {
            ButtonImage = GetComponent<Image>();
            if(ButtonImage == null)
            {
                ButtonImage = GetComponent<Image>();
            }

            if(ButtonImage == null)
            {
                Debug.Log($"InventorySlot: {gameObject.name} cannot find an ButtonImage");
            }
            else
            {
                _emptySprite = ButtonImage.sprite;
            }
        }
    }

    public bool IsSlotEmpty()
    {
        return _item == null;
    }

    public void StoreItem(InventoryComponent newItem)
    {
        _item = newItem;
        ButtonImage.sprite = _item.GetIcon();
        _item.DropDown();
    }

    public InventoryComponent GrabItem()
    {
        _item.PickedUp();
        ButtonImage.sprite = _emptySprite;
        InventoryComponent returnItem = _item;
        _item = null;
        return returnItem;
    }
}
