using UnityEngine;

public class DestructableObject : MonoBehaviour
{
    [SerializeField] private float destructablePowerImpact = 10f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.relativeVelocity.magnitude);
        if(collision.relativeVelocity.magnitude >= destructablePowerImpact)
        {
            Destroy(gameObject);
        }
    }
}
