using UnityEngine;
using System.Collections;

public class GunItem : MonoBehaviour
{
    public enum GunNameEnum {LeeEnfeild,Remigton ,MP18,Vickers,Mauser,Winchester,SpringField_sniper};
    public GunNameEnum GunName;
    public bool ManualSetBulletCount = false;
    public int BulletCount;
   
    private int MaxBullet=100;
    
	
	void Start () 
    {
        CreateGunItem(ManualSetBulletCount,BulletCount,GunName);       
	}


    public void CreateGunItem(bool ManualSet,int bulletCount,GunNameEnum Name) 
    {
        
       
        GunName=Name;


        if (ManualSet == false)
        {
            BulletCount = CalculateBulletCount();
        }
        else
        {
            BulletCount = bulletCount;
        }
        
        if (BulletCount > MaxBullet)
        {
            BulletCount = MaxBullet;
        }
        if (BulletCount <= 0)
        {
            BulletCount=CalculateBulletCount();
        }

        
	}
    public GunItemInfo GetGunItemInfo()
    {
        GunItemInfo CurrentGunItemInfo = new GunItemInfo();
        CurrentGunItemInfo.BulletCount = BulletCount;
        CurrentGunItemInfo.Name = GunName;
        return CurrentGunItemInfo;
    }
    int CalculateBulletCount()
    {
       int bulletCount = Random.Range((int)(MaxBullet * .3), (int)(MaxBullet * .5));
        return bulletCount;
        
    }
    void OnTriggerEnter()
    {
        GunItemInfo currentgunInfo = GetGunItemInfo();
        //SendMessage("goft",currentgunInfo);
    }


}
