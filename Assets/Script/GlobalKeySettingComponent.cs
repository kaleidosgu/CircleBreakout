using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GlobalKeySettingComponent : MonoBehaviour
{
    public KeyCode KeycodeLeft1P;
    public KeyCode KeycodeRight1P;
    public KeyCode KeycodeUp1P;
    public KeyCode KeycodeDown1P;


    public KeyCode KeycodeLeft2P;
    public KeyCode KeycodeRight2P;
    public KeyCode KeycodeUp2P;
    public KeyCode KeycodeDown2P;

    public string NextSceneName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void SetKeyData(KeyCode Left1P, KeyCode Right1P, KeyCode Up1P, KeyCode Left2P, KeyCode Right2P, KeyCode Up2P, KeyCode Down1P, KeyCode Down2P)
    {
        KeycodeLeft1P = Left1P;
        KeycodeRight1P = Right1P;
        KeycodeUp1P = Up1P;
        KeycodeLeft2P = Left2P;
        KeycodeRight2P = Right2P;
        KeycodeUp2P = Up2P;

        KeycodeDown1P = Down1P;
        KeycodeDown2P = Down2P;
    }
}
