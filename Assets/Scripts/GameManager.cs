using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] public GameObject player;
    [SerializeField] public CompositeCollider2D wallCollider;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if(instance == null)
            instance = this;
    }
}
