using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public enum ColorType { blank = 0, yellow = 1, red = 2, blue = 3, black = 4 };
    public static Dictionary<ColorType, Color> BrickColorDict = new()
    {  { ColorType.yellow , new Color(1, 0.92f, 0.016f, 1) }, { ColorType.red , Color.red },
        { ColorType.blue , Color.blue }, { ColorType.black , Color.black } };
    public static string playerTag = "Player";
    public static string botTag = "Bot";
    public enum StageIdx { first = 0, second = 1, third = 2 };
    public enum PoolType { bricks = 0, carriedBricks = 1 };

    public static Dictionary<StageIdx, Stage> stageDict = new();

    public static string animMove = "move";
    public static string animIdle = "idle";
    public static string animJump = "jump";

    public enum LevelIdx { level1 = 0, level2 = 1, level3 = 2 }
}
