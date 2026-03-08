using System.Collections;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private float delay;
    private float radius;
    private int damage;

    public void Initialize(float explosionDelay, float explosionRadius, int explosionDamage)
    {
        delay = explosionDelay;
        radius = explosionRadius;
        damage = explosionDamage;

        StartCoroutine(ExplosionRoutine());
    }

    private IEnumerator ExplosionRoutine()
    {
        yield return new WaitForSeconds(delay);

        // Наносим урон игроку и всем врагам в радиусе
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius);
        foreach (Collider2D hit in hits)
        {
            if (hit.TryGetComponent<Player>(out Player player))
            {
                player.TakeDamage(transform, damage);
            }
            else if (hit.TryGetComponent<EnemyEntity>(out EnemyEntity enemy))
            {
                enemy.TakeDamage(damage);
            }
        }

        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}