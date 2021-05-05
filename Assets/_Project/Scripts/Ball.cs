using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Ball : MonoBehaviour
{
    [SerializeField] private float dieDelay = 5f;

    public UnityEvent OnActivate, OnDie;

    private bool isShot;
    private bool hasTouched;

    private void OnCollisionEnter2D(Collision2D collision)
    {
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
