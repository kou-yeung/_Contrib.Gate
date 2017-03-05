using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandaloneServer : MonoBehaviour
{
    void Awake()
    {
        GameState.CreateInstance();
    }
    void Start ()
    {
	}
	
	void Update ()
    {
        GameState.Instance.Update();
    }
}
