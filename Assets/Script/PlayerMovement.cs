using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public BallDefine.BallStateDefine PlayerOwnState;
    public Transform TransCenter;
    public KeyCode LeftKeyCode;
    public KeyCode RightKeyCode;
    public KeyCode AdjustRadius;

    public float BigRadius;
    public float SmallRadius;

    public float rotationRadius;
    public float angularSpeed;

    public float InitArc;

    public ScoreComponent scoreCom;

    public Transform PenaltyPoint;

    //public Transform BallParent;

    public BallMovement ballMove;

    private float m_posX, m_posY, m_rad = 0f;
    private bool m_bPenaltyPlayer;
    // Start is called before the first frame update
    void Start()
    {
        rotationRadius = SmallRadius;
        m_rad = InitArc;
        _changePos();
    }

    public void Reposition()
    {
        m_rad = InitArc;
        _changePos();
    }

    // Update is called once per frame
    void Update()
    {
        if(scoreCom.GamePause == false)
        {
            if (Input.GetKey(LeftKeyCode) == true)
            {
                _moveParameter(false);
            }
            else if (Input.GetKey(RightKeyCode) == true)
            {
                _moveParameter(true);
            }
            else if (Input.GetKeyUp(AdjustRadius) == true)
            {
                if(m_bPenaltyPlayer == true)
                {
                    ballMove.ResetBall(PlayerOwnState,transform);
                    m_bPenaltyPlayer = false;
                }
                else
                {
                    if (rotationRadius == BigRadius)
                    {
                        rotationRadius = SmallRadius;
                        _changePos();
                    }
                    else
                    {
                        rotationRadius = BigRadius;
                        _changePos();
                    }
                }
            }
        }
        else
        {
            if( Input.GetKey(LeftKeyCode) || Input.GetKey(RightKeyCode))
            {
                //角色确认
                scoreCom.PlayerConfirm(PlayerOwnState);
            }
        }
    }

    public void SetPenaltyPlayer()
    {
        m_bPenaltyPlayer = true;

        ballMove.transform.SetParent(PenaltyPoint);
        ballMove.transform.localPosition = new Vector3(0, 0, 0);
    }
    private void _changePos()
    {
        m_posX = TransCenter.position.x + Mathf.Cos(m_rad) * rotationRadius;
        m_posY = TransCenter.position.y + Mathf.Sin(m_rad) * rotationRadius;

        transform.position = new Vector3(m_posX, m_posY, transform.position.z);

    }

    private void _moveParameter(bool bRight)
    {
        _changePos();
        float fAddValue = Time.deltaTime * angularSpeed;
        if( bRight == true )
        {
            fAddValue = fAddValue * -1;
        }
        else
        {

        }
        m_rad = m_rad + fAddValue;
        //Debug.Log(string.Format("m_angle = {0}", m_rad) );

        if (m_rad >= 360f)
        {
            m_rad = 0f;
        }
    }
}
