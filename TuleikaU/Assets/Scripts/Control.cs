using UnityEngine;

public class Control
{
    #region StandardKeys

    public static KeyCode Player1RotateLeftKey = KeyCode.A;
    public static KeyCode Player1RotateRightKey = KeyCode.D;
    public static KeyCode Player1BlowKey = KeyCode.W;
    public static KeyCode Player1FlipperKey = KeyCode.S;

    public static KeyCode Player2RotateLeftKey = KeyCode.LeftArrow;
    public static KeyCode Player2RotateRightKey = KeyCode.RightArrow;
    public static KeyCode Player2BlowKey = KeyCode.UpArrow;
    public static KeyCode Player2FlipperKey = KeyCode.DownArrow;

    #endregion

    #region AlternativeKeys

    public static KeyCode Player1UpKey = KeyCode.W;
    public static KeyCode Player1DownKey = KeyCode.S;
    public static KeyCode Player1LeftKey = KeyCode.A;
    public static KeyCode Player1RightKey = KeyCode.D;
    public static KeyCode Player1AltBlowKey = KeyCode.E;
    public static KeyCode Player1AltFlipperKey = KeyCode.Q;

    public static KeyCode Player2UpKey = KeyCode.UpArrow;
    public static KeyCode Player2DownKey = KeyCode.DownArrow;
    public static KeyCode Player2LeftKey = KeyCode.LeftArrow;
    public static KeyCode Player2RightKey = KeyCode.RightArrow;
    public static KeyCode Player2AltBlowKey = KeyCode.RightShift;
    public static KeyCode Player2AltFlipperKey = KeyCode.RightControl;

    #endregion

    public static bool IsAlternative = false;

    public KeyCode Up;
    public KeyCode Down;
    public KeyCode Left;
    public KeyCode Right;
    public KeyCode Blow;
    public KeyCode Flipper;

    public Control(PlayerType playerType)
    {
        if (playerType == PlayerType.PlayerOne)
        {
            Up = Player1UpKey;
            Down = Player1DownKey;
            Left = IsAlternative ? Player1RotateLeftKey : Player1LeftKey;
            Right = IsAlternative ? Player1RotateRightKey : Player1RightKey;
            Blow = IsAlternative ? Player1AltBlowKey : Player1BlowKey;
            Flipper = IsAlternative ? Player1AltFlipperKey : Player1FlipperKey;
        }
        else
        {
            Up = Player2UpKey;
            Down = Player2DownKey;
            Left = IsAlternative ? Player2RotateLeftKey : Player2LeftKey;
            Right = IsAlternative ? Player2RotateRightKey : Player2RightKey;
            Blow = IsAlternative ? Player2AltBlowKey : Player2BlowKey;
            Flipper = IsAlternative ? Player2AltFlipperKey : Player2FlipperKey;
        }
    }
}
