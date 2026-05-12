using Unity.VisualScripting;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] Vector2 moveSpeed;
    Vector2 offset;
    Material material;
    void Start() // Starting by Taking Finding the Sprite of BG
    {
        material = GetComponent<SpriteRenderer>().material;
    }
    void Update() // The movement of BG makes an effect
    {
        offset+=moveSpeed*Time.deltaTime;
        material.mainTextureOffset=offset;
    }

}
