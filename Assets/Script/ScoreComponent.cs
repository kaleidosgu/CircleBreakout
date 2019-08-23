using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreComponent : MonoBehaviour
{
    public Text TxtScore;
    public Text TxtBlueConfirm;
    public Text TxtRedConfirm;
    public Text TxtScoreNotify;
    public Text TxtRules;
    public int ScoreOf1P;
    public int ScoreOf2P;
    public bool GamePause;

    public BallMovement ballMove;

    public KeyCode BlueReady;
    public KeyCode RedReady;

    private bool m_bBlueConfirm;
    private bool m_bRedConfirm;

    private BallDefine.BallStateDefine m_nextState;
    // Start is called before the first frame update
    void Start()
    {
        TxtScore.text = string.Format("{0}:{1}", ScoreOf1P, ScoreOf2P);
    }


    public void ScoredByBlue()
    {
        ScoreOf1P++;
        TxtScore.text = string.Format("{0}:{1}",ScoreOf1P, ScoreOf2P);
        TxtScoreNotify.text = "蓝色得分";
        _readyMod();
        m_nextState = BallDefine.BallStateDefine.BallStateDefine_Red;
    }
    public void ScoredByRed()
    {
        ScoreOf2P++;
        TxtScore.text = string.Format("{0}:{1}", ScoreOf1P, ScoreOf2P);
        TxtScoreNotify.text = "红色得分";
        _readyMod();
        m_nextState = BallDefine.BallStateDefine.BallStateDefine_Blue;
    }
    private void _readyMod()
    {
        GamePause = true;
        m_bBlueConfirm = false;
        m_bRedConfirm = false;
        TxtBlueConfirm.text = string.Format("蓝色待准备");
        TxtRedConfirm.text = string.Format("红色待准备");
    }
    public void PlayerConfirm(BallDefine.BallStateDefine rState)
    {
        if( rState == BallDefine.BallStateDefine.BallStateDefine_Blue)
        {
            m_bBlueConfirm = true;
            TxtBlueConfirm.text = string.Format("蓝色完毕");
        }
        else if (rState == BallDefine.BallStateDefine.BallStateDefine_Red)
        {
            m_bRedConfirm = true;
            TxtRedConfirm.text = string.Format("红色完毕");
        }
        if( m_bBlueConfirm == true && m_bRedConfirm == true )
        {
            m_bBlueConfirm = false;
            m_bRedConfirm = false;
            ballMove.SetPlayerPenalty(m_nextState);
            TxtBlueConfirm.text = string.Format("");
            TxtRedConfirm.text = string.Format("");
            TxtScoreNotify.text = "";
            GamePause = false;
        }
    }

    private void Update()
    {
        if( Input.GetKeyUp(BlueReady) || Input.GetKeyUp(RedReady))
        {
            TxtRules.gameObject.SetActive(false);
        }
    }
}
