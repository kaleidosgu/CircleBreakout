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

    private BallDefine.BallStateDefine m_futureState;
    private Vector2 m_vecMoveDirection;
    private SpriteRenderer m_sprite;
    private BallDefine.BallStateDefine[] m_randArrayState;
    private Color[] m_randArrayColor;
    private PlayerMovement[] m_arrayPlayer;

    private bool m_bBallMove;
    // Start is called before the first frame update
    void Start()
    {
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
            Vector3 vecDir = transform.position - collision.GetComponent<Transform>().position;
            m_vecMoveDirection = vecDir.normalized * MoveSpeed;
            if( collision.GetComponent<PlayerMovement>().PlayerOwnState == BallDefine.BallStateDefine.BallStateDefine_Blue )
            {
                m_futureState = BallDefine.BallStateDefine.BallStateDefine_Red;
            }
            else if (collision.GetComponent<PlayerMovement>().PlayerOwnState == BallDefine.BallStateDefine.BallStateDefine_Red)
            {
                m_futureState = BallDefine.BallStateDefine.BallStateDefine_Blue;
            }
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
}
