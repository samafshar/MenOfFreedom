using UnityEngine;
using System.Collections;

public class GunRotation : MonoBehaviour
{
    public float amount = 0.17f;
    public float maxAmount = 0.24f;
    public float aimedAmount = 0.04f;
    public float aimedMaxAmount = 0.06f;
    public float smooth = 4.2f;

    float maxAmountInAFrame = 0.025f;

    float minDeltaTimeToDecreaseAmount = 0.0333f;
    float maxDeltaTimeToDecreaseAmount = 0.1f;

    float finalKamkonCoef = 0.7f;

    PlayerGun playerGun;

    private Vector3 def;

    bool firstTick = true;

    void Start()
    {
        //def = transform.localPosition;

        playerGun = GetComponent<PlayerGun>();
    }

    void Update()
    {
        if (firstTick)
        {
            def = transform.localPosition;
            firstTick = false;
        }

        float am = amount;
        float maxAm = maxAmount;

        float newSmooth = smooth;

        if (playerGun!= null && playerGun.isAimed)
        {
            am = aimedAmount;
            maxAm = aimedMaxAmount;
        }

        float factorX = -CustomInputManager.GetMouseAxisX() * am; //-Input.GetAxis("Mouse X") * am;
        factorX = Mathf.Clamp(factorX, -maxAmountInAFrame, maxAmountInAFrame);

        float factorY = -CustomInputManager.GetMouseAxisY() * am;  //-Input.GetAxis("Mouse Y") * am;
        factorY = Mathf.Clamp(factorY, -maxAmountInAFrame, maxAmountInAFrame);

        //print(Time.deltaTime);

        float deltaTime = Time.deltaTime;

        if (deltaTime > minDeltaTimeToDecreaseAmount)
        {
            float clampedDT = Mathf.Clamp(deltaTime, minDeltaTimeToDecreaseAmount, maxDeltaTimeToDecreaseAmount);

            float decreasementCoef = clampedDT / maxDeltaTimeToDecreaseAmount;

            factorX *= (1.33f - decreasementCoef);
            factorY *= (1.33f - decreasementCoef);

            newSmooth *= (1.5f - decreasementCoef);
        }


        //print(factorX);

        if (factorX > maxAm)
            factorX = maxAm;

        if (factorX < -maxAm)
            factorX = -maxAm;

        if (factorY > maxAm)
            factorY = maxAm;

        if (factorY < -maxAm)
            factorY = -maxAm;

        Vector3 final = new Vector3(def.x + factorX * finalKamkonCoef, def.y + factorY * finalKamkonCoef, def.z);
        transform.localPosition = Vector3.Lerp(transform.localPosition, final, Time.deltaTime * newSmooth);
    }
}
