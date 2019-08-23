using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDefine 
{
    public enum BallStateDefine
    {
        BallStateDefine_None = 0,
        BallStateDefine_Blue,
        BallStateDefine_Red
    }

    public static string TagOfPlayer = "Player";
    public static string TagOfChangeArea = "ChangeArea";
    public static string TagOfWall = "Wall";
}
