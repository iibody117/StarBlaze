using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] int damage = 10;

    public int GetDamage() // send the Damage taken
    {
        return damage;
    }
    public void Hit(Collider2D shot) // Destroy Object when needed
    {
        if(shot.gameObject.name == "Player") return;
        else if(shot.gameObject.name == "Shield") return;
        else Destroy(gameObject);
    }

    public void SetDamage(int value)
    {
        damage = value;
    }
 
}
