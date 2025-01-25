using System.Xml.Linq;
using UnityEngine;

public class GumPlayer : Player
{

    private SoundsNames gumExplosion = SoundsNames.GumExplosion;
    private SoundsNames blowinGumUP = SoundsNames.BlowingGumUp;
    private SoundsNames blowinGumDown = SoundsNames.BlowingGumDown;
    private string playerName = "Girl";
    private string gumName = "GirlBubble_";

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
        //ChangeSprite(this.playerSprite, gumName + "" + (currentLevelY));
/*        currentLevelY += direction;
        currentSpeedY += speedStepY * direction;*/

    }

    private void ChangeSprite(SpriteRenderer sprite,string name)
    {
        // Load the sprite from the Resources folder
        Sprite newSprite = Resources.Load<Sprite>($"Sprites/{name}");

        if (newSprite != null)
        {
            // Assign the loaded sprite to the SpriteRenderer
            sprite.sprite = newSprite;
        }
        else
        {
            Debug.LogWarning($"Sprite with name '{name}' not found in Resources/Sprites!");
        }
    }

}
