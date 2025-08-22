using System.Collections;
using UnityEngine;

public class FreezeObject : MonoBehaviour, TimeFreeze
{
    private Rigidbody2D rb;
    private Vector2 storedVelocity;
    private float freezeTimer = 5f;
    private bool isFrozen = false;
    MonoBehaviour[] allScripts;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (isFrozen)
        {
            freezeTimer -= Time.deltaTime;
            if (freezeTimer <= 0f)
            {
                Unfreeze();
            }
        }
    }

    public void Freeze(float duration)
    {
        Debug.Log("Frozen mr");
        if (rb == null) return;

        storedVelocity = rb.velocity;
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        isFrozen = true;
        freezeTimer = duration;

        allScripts = GetComponents<MonoBehaviour>();
        foreach (var script in allScripts)
        {
            if (script != this) script.enabled = false;
        }

    }

    public void Unfreeze()
    {
        if (rb == null) return;

        rb.isKinematic = false;
        rb.velocity = storedVelocity;
        isFrozen = false;
        rb.constraints = RigidbodyConstraints2D.None;

        foreach (var script in allScripts)
        {
            if (script != this) script.enabled = true;
        }
    }
}
