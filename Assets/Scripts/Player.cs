using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using static GameManager;

public class Player : MonoBehaviour
{
    [SerializeField] protected KeyCode rightKey;
    [SerializeField] protected KeyCode leftKey;
    [SerializeField] protected KeyCode upKey;
    [SerializeField] protected KeyCode downKey;
    [SerializeField] protected SpriteRenderer playerSprite;
    protected GameManager manager;

    protected float speedX = 3f; // Constant horizontal speed
    [SerializeField] protected float maxSpeedY = 5f; // Maximum vertical speed
    [SerializeField] protected float speedStepY = 1f; // Increment/Decrement step for vertical speed

    protected float hitAddForceY = -1f;
    protected float currentSpeedY = 0f; // Current vertical speed
    protected bool isHit = false; // Tracks if the player was hit
    [SerializeField] protected bool isTriggered = false; // Trigger state to control movement
    void Start()
    {
        this.manager = GameManager.inst;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.manager.CurrentState== GameState.Play)
        {
            HandleMovement();
            HandleSpeedChange();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnPlayerLoss();
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            OnPlayerWin();
        }

    }

    /// <summary>
    /// Applies a downward force when the player is hit.
    /// </summary>
    protected virtual void ApplyHitForce()
    {
        StartCoroutine(HitForceCountDown(1F));
    }
    // Set power up on the player
    IEnumerator HitForceCountDown(float timerPowerUp)
    {

            isHit = true;
                yield return new WaitForSeconds(timerPowerUp);
        isHit = false;


    }

    /// <summary>
    /// Handles player movement based on input.
    /// </summary>
    protected virtual void HandleMovement()
    {
        // Constant horizontal movement
        float x = Input.GetKey(rightKey) ? 1 : Input.GetKey(leftKey) ? -1 : 0;
        float y = currentSpeedY; // Vertical speed based on up/down key presses

        // Flip the sprite based on horizontal direction
        if (x < 0) playerSprite.flipX = true;
        if (x > 0) playerSprite.flipX = false;

        // Apply movement
        Vector3 movement = new Vector3(x * speedX, y, 0) * Time.deltaTime;
        transform.position += movement;
    }

    /// <summary>
    /// Adjusts vertical speed when up or down keys are pressed.
    /// </summary>
    protected virtual void HandleSpeedChange()
    {
        if (!isHit)
        {

            // Increase vertical speed when upKey is pressed
            if (Input.GetKeyDown(upKey) && currentSpeedY < maxSpeedY)
            {
                currentSpeedY += speedStepY;
            }

            // Decrease vertical speed when downKey is pressed
            if (Input.GetKeyDown(downKey) && currentSpeedY > 0)
            {
                currentSpeedY -= speedStepY;
            }
        }
    }



    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            OnPlayerHit();
        }
    }

    protected virtual void OnPlayerHit()
    {
        Debug.Log("Player hit by an enemy!");
        currentSpeedY = hitAddForceY; // Reset vertical speed
        ApplyHitForce();
    }

    protected virtual void OnPlayerWin()
    {
        Debug.Log("Player won the game!");
        // Handle player win logic here

            SceneManager.LoadScene("MainGame");
        
    }

    protected virtual void OnPlayerLoss()
    {
        Debug.Log("Player lost the game!");
        // Handle player loss logic here

            SceneManager.LoadScene("MainGame");
        
    }


}
