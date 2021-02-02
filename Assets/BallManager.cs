using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;

public class BallManager : MonoBehaviour
{
    // Used to track time between times for spawning balls
    private float timeTaken = 0;
    public GameObject enemy;
    public GameObject score;
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
                    score.GetComponent<TMP_Text>().text = (int.Parse(score.GetComponent<TMP_Text>().text) + 1).ToString(); 
                }else{
                    score.GetComponent<TMP_Text>().text = (int.Parse(score.GetComponent<TMP_Text>().text) - 1).ToString(); 
                    Delete(enemyCollider.gameObject);
                }
            }
        }
        //Spawn balls
        if(timeTaken > 0.25f){
            timeTaken = 0f;
            System.Random rand = new System.Random();
            SpawnEnemy(float.Parse((360 * rand.NextDouble()).ToString()), (ColorType) Enum.GetValues(typeof(ColorType)).GetValue(rand.Next(0, Enum.GetValues(typeof(ColorType)).Length)));
        }
        timeTaken += Time.deltaTime;
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
    public void SpawnEnemy(float angle, ColorType type){
        GameObject tempEnemy = Instantiate(enemy);
        angle  += 90;
        angle *= Mathf.Deg2Rad;
        tempEnemy.transform.position = new Vector3(Mathf.Cos(angle) * 4, Mathf.Sin(angle) * 8, 10);
        tempEnemy.GetComponent<EnemyController>().color = type;
        enemies.Add(tempEnemy);
    }
}
