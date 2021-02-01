using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InputManager : MonoBehaviour
{
    // Start is called before the first frame update
    private CurveRenderer curve;
    void Start()
    {
        curve = GetComponent<CurveRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.LeftArrow)){
            transform.eulerAngles += new Vector3(0,0,150f * Time.deltaTime);
            curve.Rotate(150f * Time.deltaTime);
        }else if(Input.GetKey(KeyCode.RightArrow)){
            transform.eulerAngles += new Vector3(0,0,-150f * Time.deltaTime);
            curve.Rotate(-150f * Time.deltaTime);
        }
        if(Input.GetKeyDown(KeyCode.Space)){
            GetComponent<BallManager>().SpawnEnemy();
        }
    }
}
