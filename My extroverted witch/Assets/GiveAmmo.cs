using UnityEngine;

public class GiveAmmo : MonoBehaviour
{
    public ElementType elementType;
    public int ammoGiven;

    public enum ElementType
    {
        Ice,
        Fire,
        Lightning
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            if (elementType == ElementType.Ice)
            {
                
                var iceUser = collision.GetComponentInChildren<IceBallStrategy>();
                if (iceUser != null)
                {
                   
                    iceUser.AddAmmo(ammoGiven);
                    Destroy(gameObject);
                }
            }
            else if (elementType == ElementType.Fire)
            {
                var fireUser = collision.GetComponentInChildren<IAmmoUser>();
                if (fireUser != null)
                {
                    fireUser.AddAmmo(ammoGiven);
                    Debug.Log($"Fire ammo given: {ammoGiven}");
                }
            }
            else if (elementType == ElementType.Lightning)
            {
                var lightningUser = collision.GetComponentInChildren<IAmmoUser>();
                if (lightningUser != null)
                {
                    lightningUser.AddAmmo(ammoGiven);
                    Debug.Log($"Lightning ammo given: {ammoGiven}");
                }
            }
        }

         
      }
    }
