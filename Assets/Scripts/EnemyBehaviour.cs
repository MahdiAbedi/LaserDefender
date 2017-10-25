using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {
    public float life = 50f;
    public float bulletSpeed = 0.5f;
    public GameObject laserBullet;
    public float fireRepeatTime = 5f;
    public int hitScorePoint = 150;
    public AudioClip shootSound;
    public AudioClip dieSound;

    private Score scorePoint;

    void Start()
    {
        scorePoint = GameObject.Find("ScoreTxt").GetComponent<Score>();

    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        LaserBullet bullet = collider.gameObject.GetComponent<LaserBullet>();
        if (bullet)
        {
           
            life -= bullet.GetDamage();
            scorePoint.addScore(hitScorePoint);
            if (life <= 0)
            {
                Destroy(gameObject);
                AudioSource.PlayClipAtPoint(dieSound,transform.position);
            }
            bullet.Hit();
        }
    }

    void Fire()
    {
        GameObject bullet = Instantiate(laserBullet, transform.position+new Vector3(0,-1f,0), Quaternion.identity) as GameObject;
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector3(0, -bulletSpeed);
        AudioSource.PlayClipAtPoint(shootSound, transform.position);
    }

    void Update()
    {
        float probebilty=Time.deltaTime;
        if (Random.value<probebilty)
        {
            Fire();
        }
    }
}
