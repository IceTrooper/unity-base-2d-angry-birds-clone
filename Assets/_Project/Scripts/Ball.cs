using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private float dieDelay = 5f;

    public void Die()
    {
        Destroy(gameObject, dieDelay);
    }
}
