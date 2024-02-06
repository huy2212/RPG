using System;
using RPG.Controller;
using RPG.Core;
using RPG.Movement;
using UnityEngine;
using UnityEngine.Playables;

public class CinematicControllRemover : MonoBehaviour
{
    private GameObject player;

    void OnEnable()
    {
        GetComponent<PlayableDirector>().played += DisableControl;
        GetComponent<PlayableDirector>().stopped += EnableControl;
    }

    void Disable()
    {
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void DisableControl(PlayableDirector director)
    {
        player.GetComponent<ActionScheduler>().CancelCurrentAction();
        player.GetComponent<PlayerController>().enabled = false;
    }


    private void EnableControl(PlayableDirector director)
    {
        player.GetComponent<PlayerController>().enabled = true;
    }
}