using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

[DisallowMultipleComponent]
public class Damagable : MonoBehaviour, IDamagable
{
    public float MaxHealth => maxHealth;
    [SerializeField] private float maxHealth = 10f;
    public float Health => health;
    [SerializeField, EditableProgressBar("Health", "maxHealth", EColor.Red)] private float health = 10f;

    [InfoBox("If you disable this option the object won't be receiving a damage on collision, but the damage still can be received by calling TakeDamage().")]
    [Space, SerializeField] private bool applyDamageOnCollision = true;
    [SerializeField, Tag, EnableIf("applyDamageOnCollision")] private string excludeFromCollisionDamage;
    [SerializeField] private bool destroyOnDie = true;

    [Space]
    public UnityEvent<float> OnHit;
    public UnityEvent<float> OnDie;

    [Space]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private DamagedSprite[] damagedSprites;

    private int damagedSpriteId = 0;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!applyDamageOnCollision || (excludeFromCollisionDamage != "" && collision.gameObject.CompareTag(excludeFromCollisionDamage))) return;
        TakeDamage(collision.relativeVelocity.magnitude);
    }

    public float TakeDamage(float damageAmount)
    {
        if(health <= 0f) return 0f;

        health -= damageAmount;
        ApplyDamagedSprite();

        if(health <= 0f)
        {
            damageAmount += health;
            health = 0f;
            OnDie.Invoke(damageAmount);

            if(destroyOnDie) Destroy(gameObject);
            return damageAmount;
        }

        OnHit.Invoke(damageAmount);
        return damageAmount;
    }
    
    private void ApplyDamagedSprite()
    {
        while(damagedSpriteId < damagedSprites.Length && health <= damagedSprites[damagedSpriteId].healthThreshold)
        {
            spriteRenderer.sprite = damagedSprites[damagedSpriteId].sprite;
            damagedSpriteId++;
        }
    }

    private void Reset()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    [System.Serializable]
    public struct DamagedSprite
    {
        public Sprite sprite;
        public float healthThreshold;
    }
}
