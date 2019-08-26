using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class KeySettingElement : MonoBehaviour
{
    public KeyCode CurrentKeyCode;
    public Text KeyCodeName;

    public KeyCode DefaultKeycode;

    private bool m_bWaiting;
    // Start is called before the first frame update
    void Start()
    {
        KeyCodeName.text = DefaultKeycode.ToString();
        CurrentKeyCode = DefaultKeycode;
    }

    public void BtnClick()
    {
        m_bWaiting = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(m_bWaiting == true)
        {
            foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(kcode))
                {
                    Debug.Log("KeyCode down: " + kcode);
                    m_bWaiting = false;
                    KeyCodeName.text = kcode.ToString();
                    CurrentKeyCode = kcode;
                }
            }
        }
    }
}
