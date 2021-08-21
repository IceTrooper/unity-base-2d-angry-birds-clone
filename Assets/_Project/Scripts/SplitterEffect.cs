using UnityEngine;

public class SplitterEffect : MonoBehaviour
{
    private bool isUsed;

    private float offset = 2f;

    public void Activate()
    {
        if(isUsed == true) return;

        var ball = GetComponent<Ball>();
        var rb2D = GetComponent<Rigidbody2D>();

        var upperGO = Instantiate(gameObject, transform.position + Vector3.up * offset, transform.rotation, transform.parent);
        var lowerGO = Instantiate(gameObject, transform.position + Vector3.down * offset, transform.rotation, transform.parent);

        ball.OnDie.AddListener(() => {
            Destroy(upperGO);
            Destroy(lowerGO);
        });

        var upperRb2D = upperGO.GetComponent<Rigidbody2D>();
        var lowerRb2D = lowerGO.GetComponent<Rigidbody2D>();

        upperRb2D.velocity = rb2D.velocity;
        lowerRb2D.velocity = rb2D.velocity;

        isUsed = true;
    }
}
