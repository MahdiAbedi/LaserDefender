using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    public GameObject enemyPrefabs;
    public float width = 10f;
    public float height = 5f;
    public float speed = 5f;
    public float enemyCreatorRate = 2.5f;

    private bool moveRight = true;
    private float maxX,minX;
	// Use this for initialization
	void Start () {

        float distanceFromCamera = transform.position.z-Camera.main.transform.position.z;

        Vector3 rightBoundry = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distanceFromCamera));
        Vector3 leftBoundry = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distanceFromCamera));
        maxX = rightBoundry.x;
        minX = leftBoundry.x;

        EnemyCreator();
       
	}

    public void EnemyCreator()
    {
        foreach (Transform childposition in transform)
        {
            Transform freePosition = ReturnFreePosition();
            
            if (freePosition)
            {
                GameObject enemy = Instantiate(enemyPrefabs, freePosition.position, Quaternion.identity) as GameObject;
                enemy.transform.parent = freePosition;
            }
            if (ReturnFreePosition())
            {

                Invoke("EnemyCreator", enemyCreatorRate);
            }
        }
        

    }

	public void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position,new Vector3(width,height) );
    }

	// Update is called once per frame
	void Update () {
        if (moveRight)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        else
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        float enemiesRightBorder = transform.position.x + (0.5f*width);
        float enemiesLeftBorder = transform.position.x - (0.5f * width);
        if (enemiesLeftBorder < minX) moveRight = true;
        if (enemiesRightBorder>maxX) moveRight = false;

        if (IsAllEnemyDead())
        {
            EnemyCreator();
        }
    }

    public bool IsAllEnemyDead()
    {
        foreach (Transform childs in transform)
        {
            //print("the child name is :"+childs.name);
            //print("the child count of position is :" + childs.childCount);
            if (childs.childCount>0)
            {
                return false;
            }
        }
        return true;
    }

    public Transform ReturnFreePosition()
    {
        foreach (Transform childPosition in transform)
        {
            if (childPosition.childCount==0)
            {
                return childPosition;
            }
        }
        return null;
    }
}
