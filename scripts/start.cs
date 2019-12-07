using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class start : MonoBehaviour {

    public UISprite board;

    public void OnPlaybuttonClick()
    {
        board.gameObject.SetActive(false);

        Application.LoadLevel(1);

    }
}
