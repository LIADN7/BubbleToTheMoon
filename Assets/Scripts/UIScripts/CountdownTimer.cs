using UnityEngine;
using TMPro;
using System.Collections;
using static GameManager;

public class CountdownTimer : MonoBehaviour
{
    TextMeshProUGUI countdownText;
    [SerializeField] float countdownTime = 3f;
    private GameManager manager;

    private void Awake()
    {

        this.manager = GameManager.inst;
        countdownText = GetComponent<TextMeshProUGUI>();
    }
    private void Start()
    {
    }

    public void PlayCoundown()
    {
        SoundManager.inst.Play(SoundsNames.StartGameButton);
        StartCoroutine(Countdown());
    }

    private IEnumerator Countdown()
    {
        SoundManager.inst.Stop(SoundsNames.MainMenuMusic);
        SoundManager.inst.Play(SoundsNames.CountToGame);
        float currentTime = countdownTime;
        while (currentTime > 0)
        {
            countdownText.text = Mathf.Ceil(currentTime).ToString();

            yield return new WaitForSeconds(1f);

            currentTime--;
        }

        countdownText.text = "GO!";

        yield return new WaitForSeconds(1f);
        SoundManager.inst.Play(SoundsNames.MainGameMusic);
        manager.ChangeState(GameState.Play);
        countdownText.text = "";
        countdownTime = 3;

    }


}