using UnityEngine;
using System.Collections;

public class GameGeneralInfo : MonoBehaviour
{
    public static GameGeneralInfo Instance;

    public LayerMask SoldierRaycastLayer;

    public LayerMask SoldierViewRaycastLayer;

    public LayerMask SoldierCampModeViewRaycastLayer;

    public LayerMask HitLayer;

    public LayerMask ExplosionLayer;

    public LayerMask UnderFootSurfaceLayer;

    public LayerMask OnlyGroundLayer;

    public LogicJob_Anim_Info[] logicJob_Anim_Infos;

    //public GameObject[] Soldiers_Hindi_Springfield;
    //public GameObject[] Soldiers_Hindi_MP18;
    //public GameObject[] Soldiers_English_Springfield;
    //public GameObject[] Soldiers_English_MP18;
    //public GameObject[] Soldiers_Ally;

    void Awake()
    {
        Instance = this;
    }

    public LogicJob_Anim_Info Get_LogicJob_Anim_Info_ByType(LogicJob_Anim_Type _animType)
    {
        foreach (LogicJob_Anim_Info inf in logicJob_Anim_Infos)
        {
            if (inf.animType == _animType)
                return inf;
        }

        return null;
    }
}
