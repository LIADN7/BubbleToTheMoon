using System.Xml.Linq;
using System.Collections;
using UnityEngine;

public class GumPlayer : Player
{

    private SoundsNames gumExplosion = SoundsNames.GumExplosion;
    private SoundsNames blowinGumUP = SoundsNames.BlowingGumUp;
    private SoundsNames blowinGumDown = SoundsNames.BlowingGumDown;
    [SerializeField] private Sprite[] playerSprites;
    [SerializeField] private Sprite[] gumSprites;
    [SerializeField] protected SpriteRenderer bubbleExpSpriteRenderer;
    [SerializeField] protected Animator bubbleExpAnimator;

    private float radiusOffset=0.1f;
    private float radiusStart=0.4f;

    protected override void OnPlayerHit()
    {
        if(currentLevelY>=0)
            TriggerBubblePop();
        base.OnPlayerHit();
        SoundManager.inst.Play(gumExplosion);
        ChangeSprite(gumSprites.Length+1);
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
        ChangeSprite(currentLevelY);
/*        currentLevelY += direction;
        currentSpeedY += speedStepY * direction;*/

    }

    private void ChangeSprite(int i)
    {
        // Load the sprite from the Resources folder
        int iPlayer = 1;
        bool bubbleEnable = true;
        if (i < 0|| i >= gumSprites.Length)
        {
            iPlayer = 0;
            i = 0;
            bubbleEnable = false;
        }
/*        else if (i >= gumSprites.Length) {
            i = gumSprites.Length-1;
        }*/
            this.playerSpriteRenderer.sprite = playerSprites[iPlayer];
            this.bubbleSpriteRenderer.sprite = gumSprites[i];
            this.bubbleSpriteRenderer.enabled = bubbleEnable;

            this.bubbleCollider.radius= GetNewRadius(i);
            this.bubbleCollider.offset = new Vector2(0f, GetNewRadius(i*2));

    }

    private float GetNewRadius(int i)
    {
        return radiusOffset * i + radiusStart;
    }



    private void TriggerBubblePop()
    {
        if (bubbleExpAnimator != null && bubbleExpSpriteRenderer != null)
        {
            bubbleExpAnimator.SetBool("Pop", true);
            bubbleExpAnimator.Play("idle");

        }
        else
        {
            Debug.LogWarning("Animator or SpriteRenderer is not assigned!");
        }
    }



}
