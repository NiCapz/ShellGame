using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private GameObject seaShellPrefab;
    [SerializeField] private ShellSpawner shellSpawner;
    [SerializeField] private Animator topWaveAnimator, bottomWaveAnimator;//, cameraAnimator;
    [SerializeField] private CameraAnimation cameraAnimation;

    private bool atBeach = true;
    public Vector2 mouseposition;
    private string startWaveTrigger = "startWave";
    //private string toggleBlanketTrigger = "toggleBlanket";
    private static List<GameObject> clickables = new List<GameObject>();

    public static void AddToClickables(GameObject clickable)
    {
        clickables.Add(clickable);
    }

    public static void RemoveFromClickables(GameObject unclickable)
    {
        clickables.Remove(unclickable);
    }

    void TryClick()
    {
        if (clickables.Count > 0)
        {
            GameObject itemToClickOn = clickables[clickables.Count - 1];
            IClickable clickable = itemToClickOn.GetComponent<IClickable>();
            if (clickable != null)
            {
                clickable.ClickOn();
            }
        }
    }

    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.ScreenToWorldPoint(Input.mousePosition).z;

        Vector2 worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        mouseposition = worldMousePos;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            TryClick();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            topWaveAnimator.SetTrigger(startWaveTrigger);
            bottomWaveAnimator.SetTrigger(startWaveTrigger);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            shellSpawner.DespawnShells();
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            if (atBeach)
            {
                cameraAnimation.EaseUp();
            } else
            {
                cameraAnimation.EaseDown();
            }
            atBeach = !atBeach;
            //cameraAnimator.SetTrigger(toggleBlanketTrigger);
        }
    }
}
