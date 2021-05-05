using UnityEngine;
using UnityEngine.Events;

public class DestructableObject : MonoBehaviour
{
    [SerializeField] private float destructablePowerImpact = 10f;

    public UnityEvent OnDestruction;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.relativeVelocity.magnitude);
        if(collision.relativeVelocity.magnitude >= destructablePowerImpact)
        {
            OnDestruction.Invoke();
            Destroy(gameObject);
        }
    }
}
