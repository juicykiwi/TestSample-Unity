using UnityEngine;
using System.Collections;

public class GameHelper
{
    public static Vector3 RoundVector3(Vector3 pos)
    {
        return new Vector3(
            Mathf.Round(pos.x), 
            Mathf.Round(pos.y),
            Mathf.Round(pos.z));
    }

    public static Vector2 RoundVector2(float x, float y)
    {
        return new Vector2(
            Mathf.Round(x),
            Mathf.Round(y));
    }
}
