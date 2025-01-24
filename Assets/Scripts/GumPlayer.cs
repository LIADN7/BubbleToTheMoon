using UnityEngine;

public class GumPlayer : Player
{

    public SoundsNames gumExplosion = SoundsNames.GumExplosion;
    public SoundsNames blowinGumUP = SoundsNames.BlowingGumUp;
    public SoundsNames blowinGumDown = SoundsNames.BlowingGumDown;

    protected override void OnPlayerHit()
    {
        base.OnPlayerHit();
        SoundManager.inst.Play(gumExplosion);
    }

    protected override void HandleUpAndDownChange(int direction)
    {
        base.HandleUpAndDownChange(direction);
        Debug.LogWarning($"yyyy: {currentLevelY - 1}");
        if (direction == 1)
        {
            SoundManager.inst.PlayOneShot(blowinGumUP, currentLevelY-1);
        }
        else
        {
            SoundManager.inst.PlayOneShot(blowinGumDown, currentLevelY - 1);
        }
/*        currentLevelY += direction;
        currentSpeedY += speedStepY * direction;*/

    }



}
