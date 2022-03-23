using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject InGameUI;
    [SerializeField] TextMeshProUGUI ScoreText;
    [SerializeField] GameObject LoseUI;
    [SerializeField] GameObject PauseMenu;
    private bool _isVisible;
    ScoreKeeper _scoreKeeper;

    void Start()
    {
        _scoreKeeper = FindObjectOfType<ScoreKeeper>();
        StartCoroutine(CheckIfWalkManDead());
    }


    private void Update()
    {
        if(ScoreText != null)
        {
            ScoreText.text = ((int)_scoreKeeper.GetTimeScore()).ToString();
        }
    }
    public void VisibleSwitch()
    {
        if (!_isVisible)
        {
            _isVisible = true;
            PauseMenu.SetActive(true);
        }
        else
        {
            _isVisible = false;
            PauseMenu.SetActive(false);
        }
    }
    IEnumerator CheckIfWalkManDead()
    {
        while(true)
        {
            if (_scoreKeeper.IsWalkmanDead())
            {
                InGameUI.SetActive(false);
                LoseUI.SetActive(true);
                break;
            }

            yield return new WaitForEndOfFrame();
        }
    }
}
