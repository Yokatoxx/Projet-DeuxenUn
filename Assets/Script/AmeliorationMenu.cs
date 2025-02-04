using UnityEngine;

public class AmeliorationMenu : MonoBehaviour
{
    public Timer timer;
    public GameObject spawner;
    public PLayerControl player;
    public Shoot hand;


    public void SpeedBonus()
    {
        player.speed += 1;
        ResetWave();
    }

    public void ShootingRateBonus()
    {
        hand.shootCooldown += 0.1f;
        ResetWave();
    }

    public void HealingBonus()
    {
        ResetWave();
    }

    private void ResetWave()
    {
        timer.currentTime = 15;
        timer.UpdateTimerText();
        StartCoroutine(timer.DecrementTimer());
        spawner.SetActive(true);
        gameObject.SetActive(false);
        
    }

}
