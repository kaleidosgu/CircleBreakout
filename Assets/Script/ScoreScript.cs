using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    public ScoreComponent ScoreComp;
    // Start is called before the first frame update
    private BallMovement m_ballMove;
    void Start()
    {
        m_ballMove = GetComponent<BallMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if( collision.tag == BallDefine.TagOfWall )
        {
            if(m_ballMove.StateOfBall == BallDefine.BallStateDefine.BallStateDefine_Blue )
            {
                //2p getscore
                ScoreComp.ScoredByRed();
            }
            else if (m_ballMove.StateOfBall == BallDefine.BallStateDefine.BallStateDefine_Red)
            {
                //1p getscore
                ScoreComp.ScoredByBlue();
            }
            else
            {
                m_ballMove.RandPlayerPenalty();
            }
        }
    }
}
