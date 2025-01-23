using UnityEngine;
using TMPro;
using System.Collections;

public class CountdownTimer : MonoBehaviour
{
    TextMeshProUGUI countdownText;
    [SerializeField] float countdownTime = 3f;

    private void Awake()
    {
        countdownText = GetComponent<TextMeshProUGUI>();
    }
    public void PlayCoundown()
    {

        StartCoroutine(Countdown());
    }

    private IEnumerator Countdown()
    {
        float currentTime = countdownTime;

        while (currentTime > 0)
        {
            countdownText.text = Mathf.Ceil(currentTime).ToString();

            yield return new WaitForSeconds(1f);

            currentTime--;
        }

        countdownText.text = "GO!";

        yield return new WaitForSeconds(1f);

        countdownText.text = "";
    }
}