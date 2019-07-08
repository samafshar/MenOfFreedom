using UnityEngine;
using System.Collections;

public class DecalManager : MonoBehaviour
{
    private static DecalManager instance;

    public int MaxMarks;
    public ArrayList Marks;
    public ArrayList PushDistances;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public static float Add(GameObject go)
    {
        if (instance == null)
        {            
            GameObject aux = new GameObject("DecalManager");
            instance = aux.AddComponent<DecalManager>();
            instance.Marks = new ArrayList();
            instance.PushDistances = new ArrayList();
            instance.MaxMarks = 50;
        }

        GameObject auxGo;
        Transform auxT;
        Transform currentT = go.transform;
        Vector3 currentPos = currentT.position;
        float radius = (currentT.localScale.x * currentT.localScale.x * 0.25f) + 
                        (currentT.localScale.y * currentT.localScale.y * 0.25f) +
                            (currentT.localScale.z * currentT.localScale.z * 0.25f);
        
        //I change The Radius Here is first numbers:
        //radius = Mathf.Sqrt(radius);
        //float realRadius = radius * 2;
        //radius *= 0.2f;

        radius = Mathf.Sqrt(radius);
        float realRadius = radius * 8f;
        radius *= 3f;

        float distance;

        if (instance.Marks.Count == instance.MaxMarks)
        {
            auxGo = instance.Marks[0] as GameObject;
            Destroy(auxGo);
            instance.Marks.RemoveAt(0);
            instance.PushDistances.RemoveAt(0);
        }

        float pushdistance = 0.0001f;
        int lenght = instance.Marks.Count;
        //int sideMarks = 0;

        //GameObject sphereTest = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        //sphereTest.transform.position = currentPos;
        //sphereTest.transform.localScale = new Vector3(radius * 4f, radius * 8f, radius * 8f);
            
        
        for (int i = 0; i < lenght; i++)
        {
            auxGo = instance.Marks[i] as GameObject;

            if (auxGo != null)
            {
                auxT = auxGo.transform;
                distance = (auxT.position - currentPos).magnitude;
                
                if (distance < radius)
                {
                    Destroy(auxGo);
                    instance.Marks.RemoveAt(i);
                    instance.PushDistances.RemoveAt(i);
                    i--;
                    lenght--;
                }
                else if (distance < realRadius)
                {
                    float cDist = (float)instance.PushDistances[i];
                    //if (cDist >= pushdistance)
                    //    pushdistance = cDist * 1.3f;
                    pushdistance = Mathf.Max(pushdistance, cDist);
                }
            }
            else
            {
                instance.Marks.RemoveAt(i);
                instance.PushDistances.RemoveAt(i);
                i--;
                lenght--;
            }
        }

        pushdistance += 0.0035f;

        instance.Marks.Add(go);
        instance.PushDistances.Add(pushdistance);

        //DebugTest.Instance.testString = pushdistance.ToString();
        return pushdistance;
    }

    public static void ClearMarks()
    {
        GameObject go;
        if (instance.Marks.Count > 0)
        {
            for (int i = 0; i < instance.Marks.Count; i++)
            {
                go = instance.Marks[i] as GameObject;
                Destroy(go);
            }
            instance.Marks.Clear();
        }
    }
}
