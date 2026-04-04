using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Act[] Acts;

    int currentAct = 0;
    int points = 0;

    public Image endingImage;
    public TMP_Text endingText;

    public Sprite winSprite;
    public Sprite loseSprite;

    void Start()
    {
        // выключаем все акты
        for (int i = 0; i < Acts.Length; i++)
        {
            Acts[i].gameObject.SetActive(false);
        }

        // запускаем первый
        SetupAct(currentAct);
    }

    void SetupAct(int index)
    {
        Act act = Acts[index];
        act.gameObject.SetActive(true);

        if (act.rightButton != null)
        {
            act.rightButton.onClick.RemoveAllListeners();
            act.rightButton.onClick.AddListener(() =>
            {
                points++;
                GoToAct(act.nextRightAct);
            });
        }

        if (act.wrongButton != null)
        {
            act.wrongButton.onClick.RemoveAllListeners();
            act.wrongButton.onClick.AddListener(() =>
            {
                points--;
                GoToAct(act.nextWrongAct);
            });
        }

        if (act.simpleButton != null)
        {
            act.simpleButton.onClick.RemoveAllListeners();
            act.simpleButton.onClick.AddListener(() =>
            {
                GoToAct(act.nextSimpleAct);
            });
        }
    }

    void GoToAct(int nextIndex)
    {
        Acts[currentAct].gameObject.SetActive(false);

        if (nextIndex < 0 || nextIndex + 1 >= Acts.Length)
        {
            SetupAct(currentAct+1);
            EndGame();
            return;
        }

        currentAct = nextIndex;
        SetupAct(currentAct);
    }

    void NextAct()
    {
        Acts[currentAct].gameObject.SetActive(false);
        currentAct++;

        SetupAct(currentAct);
    }

    void EndGame()
    {
        Debug.Log("Игра окончена. Очки: " + points);

        if (points < 0)
        {
            Debug.Log("Плохая концовка");
        }
        else
        {
            Debug.Log("Хорошая концовка");
        }

        ApplyEnding();
    }

    void ApplyEnding()
    {
        if (points > 0)
        {
            endingImage.sprite = winSprite;
            endingText.text = "You Won";
        }
        else
        {
            endingImage.sprite = loseSprite;
            endingText.text = "You Lose";
        }
    }

}