using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIsIdle : MonoBehaviour
{
    public static float Timeout { get; set; } // Set the idle timeout (e.g., 20 seconds)
    private static float lastAction;

    public static void ReportAction()
    {
        lastAction = Time.time;
    }

    public static bool IsIdle
    {
        get { return (Time.time - lastAction) > Timeout; }
    }
}
