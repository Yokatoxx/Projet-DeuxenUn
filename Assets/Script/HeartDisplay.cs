using UnityEngine;
using UnityEngine.UI;

public class HeartDisplay : MonoBehaviour
{
    public Sprite emptyHeart;
    public Sprite fullHeart;
    public Image[] hearts;
    public PLayerControl player;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < hearts.Length ; i++)
        {
            if(i < player.health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            if(i < 10)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }
}
