using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLoader : MonoBehaviour {

	// Use this for initialization
	void Awake () {
        SceneLoader.Instance.Load("Player");
	}
}
