using UnityEngine;

public class ShieldActivation : MonoBehaviour
{
    private Animator animator;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] EdgeCollider2D edgeCollider2D;
    [SerializeField] bool shieldIsActive;

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        edgeCollider2D = GetComponent<EdgeCollider2D>();

        spriteRenderer.enabled = false;
        edgeCollider2D.enabled = false;
    }

    public void PlayShieldIn()
    {
        if(!shieldIsActive){
        animator.SetTrigger("ActiveShield");
        shieldIsActive = true;
        }
    }
    public void PlayerShieldOut()
    {
        animator.SetTrigger("DeactiveShield");
    }
    public void SetShieldIsActive(bool value)
    {
        shieldIsActive = value;
    }



}
