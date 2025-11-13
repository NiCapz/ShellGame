using DigitalRuby.Tween;
using UnityEngine;

public class CameraAnimation : MonoBehaviour
{

    private Vector3 startingPos = new Vector3(0, 0, -10);
    private Vector3 endPos = new Vector3(0, 9.41f, -10);
    private bool atBlanket;

    public void EaseUp()
    {
        TweenFactory.Tween(null, startingPos, endPos, 1.5f, TweenScaleFunctions.CubicEaseInOut,  t => transform.position = t.CurrentValue, t => Debug.Log("eased up"));
    }
    public void EaseDown()
    {
        TweenFactory.Tween(null, endPos, startingPos, 1.5f, TweenScaleFunctions.CubicEaseInOut,  t => transform.position = t.CurrentValue, t => Debug.Log("eased down"));
    }
}
