using UnityEngine;

public class MoonElement : BGElement
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Won!");
            manager.ChangeState(GameManager.GameState.Endgame);
            canvasController.ShowWinner(other.gameObject.GetComponent<Player>().PlayerName); ///
        }
    }
}
