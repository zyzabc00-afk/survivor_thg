using UnityEngine;

public class WeaponOrbit : MonoBehaviour
{
    private float currentAngle;
    private Weapon weapon;

    public void Setup(Weapon data, float startAngle)
    {
        weapon = data;
        currentAngle = startAngle;
    }

    public void UpdateOrbit(Transform center)
    {
        if (weapon == null || center == null) return;

        currentAngle += weapon.speed * Time.deltaTime;

        if (currentAngle >= 360f) currentAngle -= 360f;

        float rad = currentAngle * Mathf.Deg2Rad;
        Vector3 offset = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0) * weapon.radius;
        transform.position = center.position + offset;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            EnemyController target = other.GetComponent<EnemyController>();

            if (target != null)
            {
                float dealDamge = weapon.damage;
                target.ReceiveDamage(dealDamge);
            }
        }
    }
}
