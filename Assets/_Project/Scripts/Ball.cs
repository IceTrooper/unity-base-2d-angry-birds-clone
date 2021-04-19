using UnityEngine;

public class Ball : MonoBehaviour
{
    public void Die()
    {
        Destroy(gameObject, 3f);
    }
}
