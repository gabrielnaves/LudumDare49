using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputProcessor : MonoBehaviour
{
    void Update()
    {
        InputData.Horizontal = GetHorizontalAxis();
        InputData.Vertical = GetVerticalAxis();
        InputData.JumpDown = GetJumpDown();
    }

    float GetHorizontalAxis()
    {
        float result = 0;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            result -= 1;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            result += 1;
        return result;
    }

    float GetVerticalAxis()
    {
        float result = 0;
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.DownArrow))
            result -= 1;
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.UpArrow))
            result += 1;
        return result;
    }

    bool GetJumpDown()
    {
        return Input.GetKeyDown(KeyCode.Space) ||
               Input.GetKeyDown(KeyCode.UpArrow) ||
               Input.GetKeyDown(KeyCode.W);
    }
}
