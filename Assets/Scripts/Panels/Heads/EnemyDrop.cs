using UnityEngine;

public class EnemyDrop : MonoBehaviour
{
    public GameObject dropPrefab; // HeadDrop

    public void Drop()
    {
        if (dropPrefab != null)
        {
            // Появление чуть ниже врага
            Vector3 dropPosition = transform.position + Vector3.down * 0.5f;
            Instantiate(dropPrefab, dropPosition, Quaternion.identity);
        }
    }
}