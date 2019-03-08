using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuInput : MonoBehaviour
{
    public EventSystem EventSystem;
    public GameObject SelectedButton;

    private bool _buttonSelected;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Vertical") > 0f && !_buttonSelected)
        {
            EventSystem.SetSelectedGameObject(SelectedButton);
            _buttonSelected = true;
        }
    }

    private void OnDisable()
    {
        _buttonSelected = false;
    }
}
