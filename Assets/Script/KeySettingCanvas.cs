using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class KeySettingCanvas : MonoBehaviour
{
    public GlobalKeySettingComponent KeyComp;


    public KeySettingElement KeyUp1P;
    public KeySettingElement KeyLeft1P;
    public KeySettingElement KeyRight1P;
    public KeySettingElement KeyUp2P;
    public KeySettingElement KeyLeft2P;
    public KeySettingElement KeyRight2P;

    public KeySettingElement KeyDown1P;
    public KeySettingElement KeyDown2P;

    public string NextSceneName;

    public void ConfirmData()
    {
        KeyComp.SetKeyData(KeyLeft1P.CurrentKeyCode, KeyRight1P.CurrentKeyCode, KeyUp1P.CurrentKeyCode,
            KeyLeft2P.CurrentKeyCode, KeyRight2P.CurrentKeyCode, KeyUp2P.CurrentKeyCode,
            KeyDown1P.CurrentKeyCode, KeyDown2P.CurrentKeyCode);
        SceneManager.LoadScene(NextSceneName);
    }
}
