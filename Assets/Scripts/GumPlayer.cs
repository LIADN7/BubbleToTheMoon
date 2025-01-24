using UnityEngine;

public class GumPlayer : Player
{

    public SoundsNames gumExplosion = SoundsNames.GumExplosion;
    public SoundsNames blowinGumUP = SoundsNames.BlowinGumUP;
    public SoundsNames blowinGumDown = SoundsNames.BlowinGumDown;

    protected override void OnPlayerHit()
    {
        Debug.Log("Player hit by an enemy!");
        SoundManager.inst.Play(gumExplosion);
        this.currentSpeedY = hitAddForceY; // Reset vertical speed
        ApplyHitForce();
    }
}
