using UnityEngine;
using System.Collections;

public class HUDGun : MonoBehaviour
{ 

    public HUDGunTexture[] GunsTexture;

    public float ShowTimeDuration;
    //public float FadeTimeDuration;
    public Texture2D[] NumberTexture;
    public Vector2 DevidedScreenGunTexturePos;
    public Vector2 DevidedScreenGunTextureSize;
    public Vector2 DevidedScreenBulletTextureSize;
    public Vector2 DividedScreenBulletTexturePos;
    public Vector2 DividedScreenBulletCountPos;
    public Vector2 DividedScreenBulletCountSize;
    public float DivScrBullInterval;
    public float DivScrBullCountInterval;
    public float FadingInGunsVelocity;
    public float FadingOutGunsVelocity;
    public HUDGunInfo coft;
    public GUISkin HudSkin;
   
    private Vector2 bulletCountLablePos;
    private Vector2 lableSize;
    private float bulletInterval;
    private float bulletCountInterval;
    private HUDGunInfo currentGun;
    private HUDGunTexture currentGunTexture;
    private float showTime;
    private float fadeTime;
    private Vector2 gunTexturePos;
    private Vector2 bulletTexturePos;
    private Vector2 massageTexturePos;
    private Vector2 gunTexureSize;
    
    private Vector2 bulletSize;
    private Vector2 bulletCountSize;
    private Texture2D currentMassageTexture;
    private Texture2D[] currentNumber=new Texture2D[3];
    private Texture2D emptyTexture;
    private bool showingTexture;
    private string bulletCountStr;
    private char[] bulletCountChar;
    private float gunsHudAlfa=0;
    private float textureMassageHudAlfa = 0;
    private bool increasingAlfa = false;
    private bool decreasing = false;
    private Color ourColor;
    
    void Awake()
    {
      
        
         ourColor = GUI.color;
    }
    void Update()
    {      
    }
   
    public void SetGunforHUD(HUDGunInfo Gun)
    {
        currentGun = Gun;
        showTime = ShowTimeDuration;
        
        increasingAlfa = true;
        decreasing = false;
        for (int i = 1; i <= GunsTexture.Length;i++ )
        {
            if (Gun.Name == GunsTexture[i - 1].Name)
            {
                currentGunTexture = GunsTexture[i - 1];
                break;
            }
        }
        bulletCountStr = currentGun.TotalBulletCount.ToString();
       
        bulletCountChar = bulletCountStr.ToCharArray();
        if (bulletCountChar.Length == 3)
        {
            WhichNumberTexture(bulletCountChar, 0);
            WhichNumberTexture(bulletCountChar, 1);
            WhichNumberTexture(bulletCountChar, 2);
        }
        if (bulletCountChar.Length == 2)
        {
            WhichNumberTexture(bulletCountChar, 0);
            WhichNumberTexture(bulletCountChar, 1);
            currentNumber[2] = emptyTexture;
        }
        if (bulletCountChar.Length == 1)
        {
            WhichNumberTexture(bulletCountChar, 0);
            currentNumber[1] = emptyTexture;
            currentNumber[2] = emptyTexture;
        }
       
    }
   
  
	
    void OnGUI() 
    {
        setPosAndSize();
        GUI.skin = HudSkin;
        Color currentColor = ourColor;
        Color textMassageColor = ourColor;
        currentColor.a = gunsHudAlfa;
        textMassageColor.a = textureMassageHudAlfa;
        GUI.color = currentColor;

        if (increasingAlfa || decreasing || showTime > 0)
        {
            if (increasingAlfa)
            {
                gunsHudAlfa += Time.deltaTime * FadingInGunsVelocity;
                if (gunsHudAlfa > 1)
                {
                    increasingAlfa = false;
                    fadeTime = ShowTimeDuration - showTime;
                }

            }
            if (showTime <1)
            {
                decreasing = true;
            }
            if (decreasing)
            {
                gunsHudAlfa -= Time.deltaTime * FadingOutGunsVelocity;
              
                if (gunsHudAlfa <= 0)
                {
                    decreasing = false;
                }

            }


            {
                GUI.DrawTexture(new Rect(gunTexturePos.x, gunTexturePos.y, gunTexureSize.x, gunTexureSize.y), currentGunTexture.GunTexture);
                for (int i = 0; i < (currentGun.MagazineBulletCount - currentGun.MagazineEmptyBulletCount); i++)
                {
                    GUI.DrawTexture(new Rect(bulletTexturePos.x - ((i) ) * bulletInterval, bulletTexturePos.y, bulletSize.x, bulletSize.y), currentGunTexture.BulletTexture);
                }
                for (int i = 0; i < currentGun.MagazineEmptyBulletCount; i++)
                {
                    GUI.DrawTexture(new Rect(bulletTexturePos.x - (i+currentGun.MagazineBulletCount-currentGun.MagazineEmptyBulletCount) * bulletInterval, bulletTexturePos.y, bulletSize.x, bulletSize.y), currentGunTexture.EmptyBulletTexture);
                }
                for (int i = 0; i <= 2; i++)
                {
                    GUI.DrawTexture(new Rect(bulletCountLablePos.x+i*bulletCountInterval, bulletCountLablePos.y, bulletCountSize.x, bulletCountSize.y), currentNumber[i]);
                }
                showTime -= Time.deltaTime;
            }

        }
       
    }
    void setPosAndSize()
    {
        gunTexturePos = CalculatePosition(DevidedScreenGunTexturePos);
        bulletTexturePos = CalculatePosition(DividedScreenBulletTexturePos);
        bulletCountLablePos = CalculatePosition(DividedScreenBulletCountPos);
        bulletSize = CalculateSize(DevidedScreenBulletTextureSize);
        gunTexureSize = CalculateSize(DevidedScreenGunTextureSize);
        bulletInterval =  Screen.width / DivScrBullInterval;
        bulletCountInterval = Screen.width / DivScrBullCountInterval;
        bulletCountSize = CalculateSize(DividedScreenBulletCountSize);
    }
    Vector2 CalculatePosition(Vector2 divide)
    {
        Vector2 position;
        position.x = Screen.width - Screen.width / divide.x;
        position.y = Screen.height - Screen.height / divide.y;
        return (position);
    }
    Vector2 CalculateSize(Vector2 divide)
    {
        Vector2 size;
        size.x = Screen.width / divide.x;
        size.y = Screen.height / divide.y;
        return (size);
    }
    void WhichNumberTexture(char[] ourChar,int numberState)
    {
        string numberStr;
        char[] numberCharArray;
       
        char numberChar;
        for (int i = 0; i <= 9; i++)
        {
            numberStr = i.ToString();
           
            numberCharArray=numberStr.ToCharArray();
           
            
            numberChar = numberCharArray[0];
           
            if (ourChar[numberState] == numberChar)
            {
               
                currentNumber[numberState] = NumberTexture[i];

            }
        }
       
        
    }
   
}
