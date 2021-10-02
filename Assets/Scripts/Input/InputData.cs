using UnityEngine;

static public class InputData
{
    static public float Horizontal
    {
        get
        {
            float result = 0;
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
                result -= 1;
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
                result += 1;
            return result;
        }
    }

    static public float Vertical
    {
        get
        {
            float result = 0;
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.DownArrow))
                result -= 1;
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.UpArrow))
                result += 1;
            return result;
        }
    }

    static public bool JumpDown
    {
        get
        {
            return Input.GetKeyDown(KeyCode.Space) ||
                   Input.GetKeyDown(KeyCode.UpArrow) ||
                   Input.GetKeyDown(KeyCode.W);
        }
    }
}
