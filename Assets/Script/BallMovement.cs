using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public BallDefine.BallStateDefine StateOfBall;
    public float ballSpeed;

    public Color COLOR_Blue;
    public Color COLOR_Red;

    public ScoreComponent scoreCom;

    public PlayerMovement blueMove;
    public PlayerMovement redMove;

    public float MoveSpeed;

    public Transform TransParent;

    public Transform TransCenter;

    public float detectDistance;

    public float ballPositionRadius;

    public float GizmosLength;

    private BallDefine.BallStateDefine m_futureState;
    private Vector2 m_vecMoveDirection;
    private SpriteRenderer m_sprite;
    private BallDefine.BallStateDefine[] m_randArrayState;
    private Color[] m_randArrayColor;
    private PlayerMovement[] m_arrayPlayer;

    private bool m_bBallMove;
    private Collider2D m_selfCollider;
    private Vector2 m_vecSelf;
    private Vector3 m_vecTouchPos;
    private Vector3 m_vecReflectPos;

    private Vector3 m_vecInNormal;
    // Start is called before the first frame update
    void Start()
    {
        m_vecSelf = new Vector2();
        m_selfCollider = GetComponent<Collider2D>();
        m_randArrayState = new BallDefine.BallStateDefine[2];
        m_randArrayState[0] = BallDefine.BallStateDefine.BallStateDefine_Blue;
        m_randArrayState[1] = BallDefine.BallStateDefine.BallStateDefine_Red;

        m_randArrayColor = new Color[2];
        m_randArrayColor[0] = COLOR_Blue;
        m_randArrayColor[1] = COLOR_Red;
        m_vecMoveDirection = Vector3.right;
        StateOfBall = BallDefine.BallStateDefine.BallStateDefine_None;
        m_sprite = GetComponent<SpriteRenderer>();

        m_arrayPlayer = new PlayerMovement[2];
        m_arrayPlayer[0] = blueMove;
        m_arrayPlayer[1] = redMove;

        RandPlayerPenalty();

        m_vecTouchPos = new Vector3();
        m_vecReflectPos = new Vector3();
        m_vecInNormal = new Vector3();
    }
    public void RandPlayerPenalty()
    {
        int nRes = Random.Range(0, 2);
        SetPlayerPenalty((BallDefine.BallStateDefine)nRes + 1);
    }
    public void SetPlayerPenalty(BallDefine.BallStateDefine rState)
    {
        m_arrayPlayer[(int)rState - 1].SetPenaltyPlayer();
        m_bBallMove = false;
        m_sprite.color = Color.white;
        blueMove.Reposition();
        redMove.Reposition();
    }

    // Update is called once per frame
    void Update()
    {
        if(scoreCom.GamePause == false)
        {
            if(m_bBallMove == true)
            {
                transform.Translate(m_vecMoveDirection * Time.deltaTime * ballSpeed);
            }
        }
    }

    public void ResetBall(BallDefine.BallStateDefine rPlayer,Transform transPlayer)
    {
        //transform.position = new Vector3();
        transform.SetParent(null);
        transform.localRotation = new Quaternion();

        m_vecMoveDirection = TransCenter.position - transform.position ;
        m_vecMoveDirection = m_vecMoveDirection.normalized * MoveSpeed;

        if(rPlayer == BallDefine.BallStateDefine.BallStateDefine_Red)
        {
            m_futureState = BallDefine.BallStateDefine.BallStateDefine_Blue;
        }
        else if (rPlayer == BallDefine.BallStateDefine.BallStateDefine_Blue)
        {
            m_futureState = BallDefine.BallStateDefine.BallStateDefine_Red;
        }
        scoreCom.GamePause = false;
        m_bBallMove = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if( collision.tag == BallDefine.TagOfPlayer)
        {
            //Vector3 vecDir = transform.position - collision.GetComponent<Transform>().position;
            //m_vecMoveDirection = vecDir.normalized * MoveSpeed;

            Vector2 vecTouchPoint = new Vector2();
            bool bRes = GetCollisionPoint(out vecTouchPoint, BallDefine.TagOfPlayer);
            if( bRes == false )
            {
                Debug.Assert(false);
            }
            m_vecSelf.Set(transform.position.x, transform.position.y);
            Vector3 vecDir = m_vecSelf - vecTouchPoint;
            //m_vecMoveDirection = vecDir.normalized * MoveSpeed;
            //m_vecMoveDirection = vecDir.normalized * MoveSpeed;
            if ( collision.GetComponent<PlayerMovement>().PlayerOwnState == BallDefine.BallStateDefine.BallStateDefine_Blue )
            {
                m_futureState = BallDefine.BallStateDefine.BallStateDefine_Red;
            }
            else if (collision.GetComponent<PlayerMovement>().PlayerOwnState == BallDefine.BallStateDefine.BallStateDefine_Red)
            {
                m_futureState = BallDefine.BallStateDefine.BallStateDefine_Blue;
            }
            m_vecTouchPos.Set(vecTouchPoint.x, vecTouchPoint.y, 0);
            Vector3 vecNormal = TransCenter.position - collision.GetComponent<Transform>().position;
            m_vecInNormal = vecNormal.normalized;
            m_vecReflectPos = Vector3.Reflect(m_vecTouchPos, m_vecInNormal);

            m_vecMoveDirection = m_vecReflectPos.normalized * MoveSpeed;
//            Debug.Log(string.Format("Touch[{0}] Reflect[{1}]", m_vecTouchPos, m_vecReflectPos));
        }
        else if( collision.tag == BallDefine.TagOfChangeArea )
        {
            if(m_futureState == BallDefine.BallStateDefine.BallStateDefine_Blue)
            {
                _changeBallColor(BallDefine.BallStateDefine.BallStateDefine_Blue);
            }
            else if (m_futureState == BallDefine.BallStateDefine.BallStateDefine_Red)
            {
                _changeBallColor(BallDefine.BallStateDefine.BallStateDefine_Red);
            }
        }
    }

    private void _changeBallColor(BallDefine.BallStateDefine rState)
    {
        StateOfBall = rState;
        if(rState == BallDefine.BallStateDefine.BallStateDefine_Blue)
        {
            m_sprite.color = COLOR_Blue;
        }
        else if (rState == BallDefine.BallStateDefine.BallStateDefine_Red)
        {
            m_sprite.color = COLOR_Red;
        }
        else
        {
            m_sprite.color = Color.white;
        }
    }


    public bool GetCollisionPoint(out Vector2 vecPoint, string tagName)
    {
        bool bRes = false;
        vecPoint.x = 0;
        vecPoint.y = 0;
        bRes = _CheckHit(out vecPoint, tagName,transform.right);
        if( bRes == false)
        {
            bRes = _CheckHit(out vecPoint, tagName, -transform.right);
        }
        if (bRes == false)
        {
            vecPoint.x = transform.position.x;
            vecPoint.y = transform.position.y;
            bRes = true;
        }
        return bRes;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, ballPositionRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(m_vecTouchPos, ballPositionRadius);

        Gizmos.color = Color.red;
        Vector3 vecToInDir = -m_vecTouchPos * GizmosLength;
        Gizmos.DrawLine(m_vecTouchPos, vecToInDir);

        Vector3 vecNormalExpa = m_vecInNormal * GizmosLength;
        Gizmos.color = Color.blue;
        Vector3 vecInNormal = new Vector3(vecNormalExpa.x + m_vecTouchPos.x, vecNormalExpa.y + m_vecTouchPos.y, 0);
        Gizmos.DrawLine(m_vecTouchPos, vecInNormal);

        Vector3 vecReflectExpa = m_vecReflectPos * GizmosLength;
        Gizmos.color = Color.green;
        Vector3 vecReflect = new Vector3(vecReflectExpa.x + m_vecTouchPos.x, vecReflectExpa.y + m_vecTouchPos.y, 0);
        Gizmos.DrawLine(m_vecTouchPos, vecReflect);
    }

    private bool _CheckHit(out Vector2 vecPoint,string tagName,Vector2 vecDir)
    {
        vecPoint.x = 0;
        vecPoint.y = 0;
        bool bRes = false;
        RaycastHit2D[] hit2dArray = Physics2D.RaycastAll(transform.position, vecDir, detectDistance);

        foreach (RaycastHit2D hit2d in hit2dArray)
        {
            if (hit2d.collider != null && m_selfCollider != null)
            {
                if (hit2d.collider != m_selfCollider)
                {
                    if (tagName == hit2d.collider.gameObject.tag)
                    {
                        vecPoint.x = hit2d.point.x;
                        vecPoint.y = hit2d.point.y;
                        bRes = true;
                        break;
                    }
                }
                else
                {
                    //Debug.Log("same collide");
                }
            }
        }
        return bRes;
    }
}
