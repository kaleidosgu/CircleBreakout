using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLookAtCenter : MonoBehaviour
{
    public Transform TransCenter;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = TransCenter.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.x, -direction.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    //void Update()
    //{
    //    Vector3 vecDir = (TransCenter.position - transform.position).normalized;
    //    transform.up = vecDir;
    //}
}
