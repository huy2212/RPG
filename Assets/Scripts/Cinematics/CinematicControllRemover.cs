using System.Collections.Generic;
using RPG.Control;
using RPG.Controller;
using RPG.Core;
using UnityEngine;
using UnityEngine.Playables;

public class CinematicControllRemover : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private List<Transform> notWalkableAtStartEnemies;

    void Awake()
    {
        foreach (Transform enemy in notWalkableAtStartEnemies)
        {
            enemy.GetComponent<AIController>().enabled = false;
        }
    }

    void OnEnable()
    {
        GetComponent<PlayableDirector>().played += DisableControl;
        GetComponent<PlayableDirector>().stopped += EnableControl;
    }

    void Disable()
    {
        GetComponent<PlayableDirector>().played -= DisableControl;
        GetComponent<PlayableDirector>().stopped -= EnableControl;
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

        foreach (Transform enemy in notWalkableAtStartEnemies)
        {
            enemy.GetComponent<AIController>().enabled = true;
        }
    }
}