using UnityEngine;

public class LaserBullet : MonoBehaviour {

    public float damage = 50f;

    public float GetDamage()
    {
        return damage;
    }

    public void Hit()
    {
        Destroy(gameObject);
    }
}
