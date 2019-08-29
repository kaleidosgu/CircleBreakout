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
    public KeyCode StickMoveKeyCode;

    public float BigRadius;
    public float SmallRadius;

    public float rotationRadius;
    public float angularSpeed;

    public float InitArc;

    public ScoreComponent scoreCom;

    public Transform PenaltyPoint;

    public BallMovement ballMove;

    public Transform TransParent;

    public string NameOfAnimationStickMove;

    public float RotateZTime;
    public float RotateZSpeed;

    private float m_posX, m_posY, m_rad = 0f;
    private bool m_bPenaltyPlayer;
    private Animator m_animatorStick;

    private int m_hashNameStickMove;

    private bool m_bRotateZ;
    private float m_fRotateZValue;
    private float m_fCurRotateZTime;
    private bool m_bStickDown;
    // Start is called before the first frame update
    void Start()
    {
        m_animatorStick = GetComponent<Animator>();
        rotationRadius = SmallRadius;
        m_rad = InitArc;

        GlobalKeySettingComponent _keycom = FindObjectOfType<GlobalKeySettingComponent>();
        if(_keycom != null)
        {
            if (PlayerOwnState == BallDefine.BallStateDefine.BallStateDefine_Blue)
            {
                LeftKeyCode = _keycom.KeycodeLeft1P;
                RightKeyCode = _keycom.KeycodeRight1P;
                AdjustRadius = _keycom.KeycodeUp1P;
                StickMoveKeyCode = _keycom.KeycodeDown1P;
            }
            else if (PlayerOwnState == BallDefine.BallStateDefine.BallStateDefine_Red)
            {
                LeftKeyCode = _keycom.KeycodeLeft2P;
                RightKeyCode = _keycom.KeycodeRight2P;
                AdjustRadius = _keycom.KeycodeUp2P;
                StickMoveKeyCode = _keycom.KeycodeDown2P;
            }
        }

        _changePos();

        m_hashNameStickMove = Animator.StringToHash(NameOfAnimationStickMove);
    }

    public void Reposition()
    {
        m_rad = InitArc;
        _changePos();

        _resetAngle();
    }

    private void _resetAngle()
    {
        transform.localRotation = Quaternion.identity;
        m_bStickDown = false;
        m_bRotateZ = false;
        m_fRotateZValue = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (scoreCom.GamePause == false)
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
                if (m_bPenaltyPlayer == true)
                {
                    ballMove.ResetBall(PlayerOwnState, transform);
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
            if (Input.GetKey(StickMoveKeyCode) == true)
            {
                //m_animatorStick.Play("stickMove");
                //m_animatorStick.enabled = true;
                if(m_bRotateZ == false && m_bStickDown == false)
                {
                    m_bRotateZ = true;
                    m_bStickDown = true;
                    m_fCurRotateZTime = 0;
                }
            }
            if( Input.GetKeyUp(StickMoveKeyCode) == true )
            {
                _resetAngle();
            }

            if(m_bRotateZ == true)
            {
                if (m_bStickDown == true)
                {
                    m_fCurRotateZTime += Time.deltaTime;
                    if (m_fCurRotateZTime >= RotateZTime)
                    {
                        m_bRotateZ = false;
                        m_fRotateZValue = 0;
                    }
                    else
                    {
                        m_fRotateZValue += (Time.deltaTime * RotateZSpeed);
                        Vector3 vec = new Vector3(0, 0, m_fRotateZValue);
                        transform.Rotate(vec, Space.Self);
                    }
                }
            }

            //if(m_animatorStick.enabled == true)
            //{
            //    if (m_animatorStick.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !m_animatorStick.IsInTransition(0))
            //    {
            //        if (m_animatorStick.GetCurrentAnimatorStateInfo(0).shortNameHash == m_hashNameStickMove)
            //        {
            //            m_animatorStick.enabled = false;
            //        }
            //    }
            //}

        
        }
        else
        {
            if (Input.GetKey(LeftKeyCode) || Input.GetKey(RightKeyCode))
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

        TransParent.position = new Vector3(m_posX, m_posY, TransParent.position.z);

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

        if (m_rad >= 360f)
        {
            m_rad = 0f;
        }
    }
    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.red;
        //Gizmos.DrawLine(transform.parent.position, TransCenter.position);
    }
}
