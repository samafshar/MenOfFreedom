using UnityEngine;
using System.Collections;

public class Compass : MonoBehaviour
{

    public Texture2D texture = null;
    public float texture_X_Coef;
    public float texture_Y_Coef;
    public float texture_W_Coef;
    public float texture_H_Coef;

    public Texture2D backTexture = null;
    public Texture2D topTexture = null;
    public float backTexture_X_Coef;
    public float backTexture_Y_Coef;
    public float backTexture_W_Coef;
    public float backTexture_H_Coef;

    public float PrimRot;
    public int m = 1;
    public float Destin;

    public float Rot;
    public float RotRem;
    public float RotBackUp;
    public float RotBackUpRem;

    public float Velo;
    public float VeloBackUp;
    public float Accel;
    public float DeltaRot;
    public float DeltaRotBackUp;
    public int k = 1;
    public float delta;
    public float deltaBackUp;
    public float teta;

    public int speedMax;

    public float playerDeltaRot;
    public float playerDeltaRotBackUp;
    public float fk = 0;
    public int half = 0;
    public int halfPrim = 0;

    public float playerRotRem;
    public float playerRotBackUpRem;


    public float playerRot;
    public float playerRotBackUp;
    public float playerPrimRot;



    public float x;
    public float y;
    public float z;
    public float w;

    public int n;
    public int nBackUp;
    public int nc;
    public int mov;

    bool shouldShowTextures = true;

    // Use this for initialization
    void Start()
    {
        Velo = 0;
        Accel = 0;
        Rot = PrimRot;
        playerPrimRot = transform.eulerAngles.y;
        n = 0;
        nc = 0;
        speedMax = 500;
    }

    // Update is called once per frame
    void Update()
    {

        x = transform.eulerAngles.x;
        y = transform.eulerAngles.y;
        z = transform.eulerAngles.z;

        playerRotBackUp = playerRot;
        playerRot = transform.eulerAngles.y + (n * 360);

        playerRotBackUpRem = playerRotBackUp % 360;
        if (playerRotBackUpRem < 0)
        {
            playerRotBackUpRem += 360;
        }

        playerRotRem = playerRot % 360;
        if (playerRotRem < 0)
        {
            playerRotRem += 360;
        }

        nBackUp = n;
        if (playerRotBackUpRem > 320 && playerRotRem < 40)
        {
            n += 1;
        }

        if (playerRotBackUpRem < 40 && playerRotRem > 320)
        {
            n -= 1;
        }
        playerDeltaRot = playerRot - playerRotBackUp;

        Destin = PrimRot - (playerRot - (playerPrimRot));

        RotBackUp = Rot;
        Rot = ((Velo * Time.deltaTime) + RotBackUp);

        DeltaRotBackUp = DeltaRot;
        DeltaRot = ((Destin) - Rot);

        DeltaRot = DeltaRot % 360;

        if (DeltaRot > 180)
        {
            DeltaRot -= 360;
        }
        else if (DeltaRot < -180)
        {
            DeltaRot += 360;
        }

        //if (n > nBackUp || n < nBackUp)
        //{
        //    DeltaRot = DeltaRotBackUp;
        //}

        Accel = (k * DeltaRot);

        VeloBackUp = Velo;
        Velo += (Accel * Time.deltaTime);
        Velo = (Velo * m) / (1 + m);
        if (Velo > speedMax)
        {
            Velo = speedMax;
        }
        if (Velo < -speedMax)
        {
            Velo = -speedMax;
        }
        if (Mathf.Abs(Velo) < 0.02)
        {
            Velo = 0;
        }
        if (n > nBackUp || n < nBackUp)
        {
            Velo = VeloBackUp;
        }


        deltaBackUp = delta;
        delta += (Rot - RotBackUp);

    }

    void OnGUI()
    {
        GUI.depth = -700;

        if (shouldShowTextures)
        {
            GUI.DrawTexture(new Rect(Screen.height * backTexture_X_Coef, Screen.height * backTexture_Y_Coef, Screen.height * backTexture_W_Coef, Screen.height * backTexture_H_Coef), backTexture);

            float l = Screen.height * texture_X_Coef;
            float t = Screen.height * texture_Y_Coef;
            float w = Screen.height * texture_W_Coef;
            float h = Screen.height * texture_H_Coef;

            GUIUtility.RotateAroundPivot(delta, new Vector2(l + w / 2, t + h / 2));

            GUI.DrawTexture(new Rect(l, t, w, h), texture);

            GUIUtility.RotateAroundPivot(-delta, new Vector2(l + w / 2, t + h / 2));

            GUI.DrawTexture(new Rect(Screen.height * backTexture_X_Coef, Screen.height * backTexture_Y_Coef, Screen.height * backTexture_W_Coef, Screen.height * backTexture_H_Coef), topTexture);
        }
    }

    public void SetShouldShowTextures(bool _val)
    {
        shouldShowTextures = _val;
    }

}
