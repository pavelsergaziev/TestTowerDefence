using UnityEngine;

public interface IDamageableObjectsParent 
{
    void DealDamage(GameObject damageReceiver, int damageAmount);
}