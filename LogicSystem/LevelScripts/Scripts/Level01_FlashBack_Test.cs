using UnityEngine;
using System.Collections;

public class Level01_FlashBack_Test : LevelLogic
{
    public CutsceneController firstCutscene;

    public override void StartIt()
    {
        base.StartIt();
    }

    public override void RunIt()
    {
        base.RunIt();

        if (step == 1)
        {

        StartLevelSteps:

            #region Level

            #region 0.1 Start first cutscene
            if (levelStep == 0.1f)
            {
                firstCutscene.StartIt();

                SetLevelStep(0.2f);
            }
            #endregion

            #region 0.2 Run first cutscene
            if (levelStep == 0.2f)
            {
                if (firstCutscene.status == CutsceneStatus.Finished)
                {
                    SetLevelStep(0.3f);
                }
            }
            #endregion

            #region 0.3 BlackScreen after first cutscene
            if (levelStep == 0.3f)
            {
                mapLogic.blackScreenFader.StartFadingIn();
                SetLevelStep(1f);
            }
            #endregion

            #endregion

        EndLevelSteps: ;

        }
    }

    //

    public override void LoadCheckPoint(float _levelStep)
    {
        base.LoadCheckPoint(_levelStep);

        #region B
        if (levelStep == 2)
        {
            return;
        }
        #endregion

    }
}
