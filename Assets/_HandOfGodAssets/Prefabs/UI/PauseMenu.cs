using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] Canvas PauseCanvas;
    private bool _isVisible = false;

    private void Start()
    {
        PauseCanvas.enabled = false;
    }
    public void VisibleSwitch()
    {
        if(!_isVisible)
        {
            _isVisible = true;
            PauseCanvas.enabled = true;
        }
        else
        {
            _isVisible = false;
            PauseCanvas.enabled = false;
        }
    }

    public void testBtn()
    {
        Debug.Log("BUTTON PRESSED");
    }
}
