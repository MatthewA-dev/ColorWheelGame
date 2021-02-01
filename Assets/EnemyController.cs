using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// Controls enemy behavior and appereance
public class EnemyController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject targetGameObject;
    private CircleCollider2D coll;
    private SpriteRenderer render;
    public ColorType color;
    public EnemyController(ColorType type){
        color = type;
    }
    void Start()
    {
        if(!Enum.IsDefined(typeof(ColorType), color)){
            color = ColorType.COLOR1;
        }
        render = GetComponent<SpriteRenderer>();
        coll = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Color colorval;
        ColorManager.colors.TryGetValue(color, out colorval);
        if(colorval != render.color){
            render.color = colorval;
        }
        transform.position = Vector3.MoveTowards(transform.position,new Vector3(targetGameObject.transform.position.x,targetGameObject.transform.position.x,transform.position.z), 2.5f * Time.deltaTime);
    }
}
public enum ColorType{
    COLOR1,
    COLOR2,
    COLOR3
}