using UnityEngine;
using System.Collections;

public enum ItemType
{
    Gun,
    Bullet,
    Grenade,
}

public class Item : MonoBehaviour
{
    public bool IsActive = true;
    public ItemType itemType;
    public PlayerGunName gunName;
    public int ammoCount;

    ItemRootObject parentItemRootObj;

    public void UpdateAmmoCount(int _newAmmoCount)
    {
        int newAmmoCount = _newAmmoCount;
        ammoCount = newAmmoCount;
        if (ammoCount == 0)
        {
            SetFinished();
        }
    }

    public GameObject GetRootGameObject()
    {
        return transform.parent.gameObject;
    }

    void Start()
    {
        parentItemRootObj = GetRootGameObject().GetComponent<ItemRootObject>();
    }

    void SetFinished()
    {
        IsActive = false;
        parentItemRootObj.SetFinished();
    }
}
