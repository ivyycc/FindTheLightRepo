using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{

    [field: Header("UI SFX")]
    [field: SerializeField] public EventReference hover { get; private set; }
    [field: SerializeField] public EventReference select { get; private set; }
    [field: SerializeField] public EventReference play { get; private set; }
    [field: SerializeField] public EventReference quit { get; private set; }

    [field: Header("Other SFX")]
    [field: SerializeField] public EventReference buttonPress { get; private set; }
    [field: SerializeField] public EventReference doorOpen { get; private set; }
    [field: SerializeField] public EventReference movingPlatform { get; private set; }

    [field: Header("Player SFX")]
    //[field: SerializeField] public EventReference playerWalking { get; private set; }
    [field: SerializeField] public EventReference playerJump { get; private set; }
    [field: SerializeField] public EventReference playerdies { get; private set; }
    [field: SerializeField] public EventReference playerfalls { get; private set; }

    [field: Header("Music")]
    [field: SerializeField] public EventReference Music { get; private set; }

    [field: Header("Ambience")]
    [field: SerializeField] public EventReference ambience { get; private set; }

    public static FMODEvents instance { get; private set; }

    public void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one FMOD Events in the scene");
        }
        instance = this;
    }
}
