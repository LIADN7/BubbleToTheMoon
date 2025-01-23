using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] protected KeyCode rightKey;
    [SerializeField] protected KeyCode leftKey;
    [SerializeField] protected KeyCode upKey;
    [SerializeField] protected KeyCode downKey;
    [SerializeField] protected SpriteRenderer playerSprite;

    private float speedY= 0.5f;
    private float speedX=3;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetKey(rightKey)?1: Input.GetKey(leftKey)?-1:0; // +1 if right arrow is pushed, -1 if left arrow is pushed, 0 otherwise

       float y = Input.GetKey(upKey) ? 1 : Input.GetKey(downKey) ? -1 : 0;     // +1 if up arrow is pushed, -1 if down arrow is pushed, 0 otherwise
        if (x < 0) { this.playerSprite.flipX = true; }
        if (x > 0) { this.playerSprite.flipX = false; }
        Vector3 movementYVector = new Vector3(0, y, 0) * speedY * Time.deltaTime;
        Vector3 movementXVector = new Vector3(x, 0, 0) * speedX * Time.deltaTime;
        transform.position += movementYVector + movementXVector;

    }




    private void OnTriggerEnter2D(Collider2D other)
    {

    }



    private void OnPlayerHit()
    {

    }

    private void OnPlayerWin()
    {

    }

    private void OnPlayerLoss()
    {

    }
}
