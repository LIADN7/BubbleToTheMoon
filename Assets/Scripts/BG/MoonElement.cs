using UnityEngine;

public class MoonElement : BGElement
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Won!");
            
            canvasController.ShowWinner(other.gameObject.GetComponent<Player>().PlayerName);
            manager.ChangeState(GameManager.GameState.Endgame);
        }
    }

        public void MoonAnimSound()
        {
            SoundManager.inst.Play(SoundsNames.MoonAnimation);
        }
}
