using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryComponent : MonoBehaviour
{
    [SerializeField] Sprite Icon;

    public void PickedUp()
    {
        gameObject.SetActive(true);
    }

    public void DropDown()
    {
        gameObject.SetActive(false);
    }

    public Sprite GetIcon()
    {
        return Icon;
    }
}
