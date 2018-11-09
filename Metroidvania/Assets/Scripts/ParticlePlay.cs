using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePlay : MonoBehaviour {

    public ParticleSystem ps;

	void Start () 
    {
        ps = GetComponent<ParticleSystem>();
        StartCoroutine(playPart());
	}

    IEnumerator playPart()
    {
        yield return new WaitForSeconds(.1f);
        ps.Play();
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }
}
