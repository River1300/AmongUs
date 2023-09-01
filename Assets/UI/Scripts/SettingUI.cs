using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingUI : MonoBehaviour
{
    [SerializeField]
    private Button MouseControlButton;
    [SerializeField]
    private Button KeyboardMouseControlButton;
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void OnEnable()
    {
        switch(PlayerSetting.controlType)
        {
        case EControlType.Mouse:
            MouseControlButton.image.color = Color.green;
            KeyboardMouseControlButton.image.color = Color.white;
            break;

        case EControlType.KeyboardMouse:
            MouseControlButton.image.color = Color.white;
            KeyboardMouseControlButton.image.color = Color.green;
            break;
        }
    }

    public void SetControlMode(int controlType)
    {
        PlayerSetting.controlType = (EControlType)controlType;
        switch(PlayerSetting.controlType)
        {
        case EControlType.Mouse:
            MouseControlButton.image.color = Color.green;
            KeyboardMouseControlButton.image.color = Color.white;
            break;

        case EControlType.KeyboardMouse:
            MouseControlButton.image.color = Color.white;
            KeyboardMouseControlButton.image.color = Color.green;
            break;
        }
    }

    public void Close()
    {
        StartCoroutine(CloseAfterDelay());
    }

    IEnumerator CloseAfterDelay()
    {
        animator.SetTrigger("Close");

        yield return new WaitForSeconds(0.5f);

        gameObject.SetActive(false);
        animator.ResetTrigger("Close");
    }
}
