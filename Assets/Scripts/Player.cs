using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using static GameManager;

public class Player : MonoBehaviour
{

    [SerializeField] protected KeyCode upKey;
    [SerializeField] protected KeyCode downKey;
    [SerializeField] protected KeyCode rightKey;
    [SerializeField] protected KeyCode leftKey;

    [SerializeField] protected SpriteRenderer playerSpriteRenderer;

    [SerializeField] protected SpriteRenderer bubbleSpriteRenderer;

    [SerializeField] protected CircleCollider2D bubbleCollider;
    [SerializeField] public string PlayerName;

    protected GameManager manager;
    protected UICanvasController canvasController;

    protected float speedX = 3f; // Constant horizontal speed
    [SerializeField] protected float maxSpeedY = 5f; // Maximum vertical speed
    [SerializeField] protected float speedStepY = 1f; // Increment/Decrement step for vertical speed

    protected float hitAddForceY = -1f;
    protected float currentSpeedY = 0f; // Current vertical speed
    protected int currentLevelY = 0; // Current vertical speed
    protected bool isHit = false; // Tracks if the player was hit
    [SerializeField] protected bool isTriggered = false; // Trigger state to control movement
    void Start()
    {
        this.manager = GameManager.inst;
        canvasController = FindFirstObjectByType<UICanvasController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.manager.CurrentState == GameState.Play)
        {
            HandleMovement();
            HandleSpeedOnKey();
            if (IsOutOfScreen())
            {
                Destroy(this.gameObject);
            }
            if (CheckForWin())
            {
                manager.ChangeState(GameState.Endgame);
                canvasController.ShowWinner(PlayerName);
            }
        }
    }
    private bool IsOutOfScreen()
    {
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
        return screenPoint.x < -0.2f || screenPoint.x > 1.2f || screenPoint.y < -0.3f || screenPoint.y > 1.7f;
    }
    private bool CheckForWin()
    {
        int playersPlaying = FindObjectsByType<Player>(FindObjectsInactive.Exclude, FindObjectsSortMode.None).Length;
        return playersPlaying == 1;

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
        float y = currentSpeedY + 0.3f; // Vertical speed based on up/down key presses



        // Apply movement
        Vector3 movement = new Vector3(x * speedX, y, 0) * Time.deltaTime;
        transform.position += movement;
    }

    /// <summary>
    /// Adjusts vertical speed when up or down keys are pressed.
    /// </summary>
    protected virtual void HandleSpeedOnKey()
    {
        if (!isHit)
        {

            // Increase vertical speed when upKey is pressed
            if (Input.GetKeyDown(upKey) && currentSpeedY < maxSpeedY)
            {
                //currentLevelY++;
                //currentSpeedY += speedStepY;
                HandleUpAndDownChange(1);
            }
            if (Input.GetKeyDown(upKey) && currentSpeedY >= maxSpeedY)
            {
                OnPlayerHit();
            }

            // Decrease vertical speed when downKey is pressed
            if (Input.GetKeyDown(downKey) && currentSpeedY > 0)
            {
                //currentLevelY--;
                //currentSpeedY -= speedStepY;
                HandleUpAndDownChange(-1);
            }
        }
    }

    protected virtual void HandleUpAndDownChange(int direction)
    {
        currentLevelY += direction;
        currentSpeedY += (speedStepY * direction);

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
        currentLevelY = -1;
        ApplyHitForce();
    }



}