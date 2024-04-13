using UnityEngine;

public class BulletTrap : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Bullet bullet))
        {
            bullet.ReachTarget();
        }
    }
}
