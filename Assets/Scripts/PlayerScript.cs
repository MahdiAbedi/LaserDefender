using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {
    public float speed = 5.0f;
    public float bulletSpeed;
    public GameObject laserBullet;
    public float fireRepeatTime = 0.2f;
    public float life = 250f;
    public AudioClip shootSound;

    private float maxX, minX;
	// Use this for initialization
	void Start () {
        //distance between object and camera
        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftPoint = Camera.main.ViewportToWorldPoint(new Vector3(0,0,distance));
        Vector3 rightPoint = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
        maxX = rightPoint.x;
        minX = leftPoint.x;
    }
	void Fire()
    {
        GameObject bullet = Instantiate(laserBullet, transform.position+new Vector3(0,1,0), Quaternion.identity) as GameObject;
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector3(0, bulletSpeed);
        AudioSource.PlayClipAtPoint(shootSound,transform.position);
    }
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            InvokeRepeating("Fire", 0.00001f, fireRepeatTime);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            CancelInvoke("Fire");
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            //transform.position += new Vector3(speed*Time.deltaTime,0,0);
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            //transform.position += new Vector3(-speed*Time.deltaTime, 0, 0);
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        float newX = Mathf.Clamp(transform.position.x,minX,maxX);
        transform.position = new Vector3(newX,transform.position.y,transform.position.z);
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        LaserBullet bullet = collider.gameObject.GetComponent<LaserBullet>();
        if (bullet)
        {
            life -= bullet.GetDamage();
            if (life <= 0)
            {
                Die();  
            }
            bullet.Hit();
        }
    }

    void Die()
    {
        LevelManager manager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        manager.LoadLevel("Win Screen");
        Destroy(gameObject);
    }
}
