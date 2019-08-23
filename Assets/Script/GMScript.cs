using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GMScript : MonoBehaviour
{
    public KeyCode KeyCodeResetBall;
    public BallMovement TransBall;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if( Input.GetKeyUp(KeyCodeResetBall) == true )
        {
            //TransBall.ResetBall();
        }
    }
}
