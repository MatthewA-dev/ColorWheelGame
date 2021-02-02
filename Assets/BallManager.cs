using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BallManager : MonoBehaviour
{
    public GameObject enemy;
    private CircleCollider2D coll;
    public List<GameObject> enemies = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<CircleCollider2D>();
        enemy.GetComponent<EnemyController>().targetGameObject = gameObject;
    }

    // Counts number of objects in array before it hits a null
    int CountArray(Array arr){
        int count = 0;
        foreach (var item in arr)
        {
            if(item != null){
                count++;
            }else{
                break;
            }
        }
        return count;
    }
    public void Delete(GameObject game) {
        enemies.Remove(game);
        Destroy(game);
    }
    void Update()
    {
        // Handle Collision
        Collider2D[] colls = new Collider2D[enemies.Count];
        int colliderCount = coll.OverlapCollider(new ContactFilter2D().NoFilter(), colls);
        for (int i = 0; i < colliderCount; i++){
            Collider2D enemyCollider = colls[i];
            if(enemies.Contains(enemyCollider.gameObject)){
                ColorType type = closestPoint(enemyCollider.gameObject.transform.position);
                if(type == enemyCollider.gameObject.GetComponent<EnemyController>().color){
                    Delete(enemyCollider.gameObject);
                }else{
                    Delete(enemyCollider.gameObject);
                }
            }
        }
    }
    // Get closest point from a linerenderer, Defaults to returning COLOR1
    public ColorType closestPoint(Vector3 point){
        Vector3 closePoint = new Vector3(99999,99999,99999);
        ColorType color = ColorType.COLOR1;
        foreach (Curve curve in CurveRenderer.lines){
            for (int i = 0; i < curve.lineRenderer.positionCount; i += 2){
                Vector3 pos = curve.lineRenderer.transform.position + curve.lineRenderer.GetPosition(i);
                if(Vector3.Distance(point,pos) < Vector3.Distance(point,closePoint)){
                    closePoint = pos;
                    color = curve.colorType;
                }
            }
        }
        Debug.Log(color);
        return color;
    }
    public GameObject CheckPoint(Vector3 point){
        foreach (GameObject enemy in enemies)
        {
            if(enemy.GetComponent<Collider2D>().bounds.Contains(point)){
                return enemy;
            }
        }
        return null;
    }
    public void SpawnEnemy(Vector2 pos){
        GameObject tempEnemy = Instantiate(enemy);
        tempEnemy.transform.position = pos;
        enemies.Add(tempEnemy);
    }
}
