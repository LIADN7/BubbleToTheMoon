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
    private GameManager manager;

    private float speedX = 3f; // Constant horizontal speed
    [SerializeField] private float maxSpeedY = 5f; // Maximum vertical speed
    [SerializeField] private float speedStepY = 1f; // Increment/Decrement step for vertical speed
    
    private float hitAddForceY = -1f;
    private float currentSpeedY = 0f; // Current vertical speed
    private bool isHit = false; // Tracks if the player was hit
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
    }

    /// <summary>
    /// Applies a downward force when the player is hit.
    /// </summary>
    private void ApplyHitForce()
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
    private void HandleMovement()
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
    private void HandleSpeedChange()
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



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            OnPlayerHit();
        }
    }

    private void OnPlayerHit()
    {
        Debug.Log("Player hit by an enemy!");
        currentSpeedY = hitAddForceY; // Reset vertical speed
        ApplyHitForce();
    }

    private void OnPlayerWin()
    {
        Debug.Log("Player won the game!");
        // Handle player win logic here
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("MainGame");
        }
    }

    private void OnPlayerLoss()
    {
        Debug.Log("Player lost the game!");
        // Handle player loss logic here
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("MainGame");
        }
    }


}
