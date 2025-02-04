using UnityEngine;

public class Shadow : MonoBehaviour
{
    public SpriteRenderer player;
    public SpriteRenderer shadow;


    private void Update()
    {
        shadow.flipX = player.flipX;
    }
}
