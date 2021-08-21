using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Ball : MonoBehaviour
{
    [SerializeField] private float dieDelay = 5f;

    [SerializeField] private UnityEvent OnActivate;
    public UnityEvent<float> OnHit;
    public UnityEvent OnDie;

    [SerializeField] private Scoreable scoreable;
    //private int survivePoints = 10000;
    private int hitPointsMultiplier = 1;

    private bool isShot;
    private bool hasTouched;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var collisionPower = collision.relativeVelocity.magnitude;
        OnHit.Invoke(collisionPower);

        if(collisionPower >= 1f)
        {
            var damageable = collision.gameObject.GetComponent<IDamagable>();
            if(damageable != null)
            {
                scoreable.GetPoints(Mathf.RoundToInt(hitPointsMultiplier * 10 * (int)collisionPower));
            }
        }

        if(isShot)
        {
            hasTouched = true;
        }
    }

    public void Shot()
    {
        isShot = true;
        Die();
    }

    public void Activate()
    {
        if(hasTouched) return;

        OnActivate.Invoke();
    }

    public void Die()
    {
        StartCoroutine(DieCoroutine());
    }

    private IEnumerator DieCoroutine()
    {
        yield return new WaitForSeconds(dieDelay);
        OnDie.Invoke();
        Destroy(gameObject);
    }
}
