using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_FallFloor : MonoBehaviour
{
    public float timeBeforeDrop;
    public float returnTime;
    public float time;
    public float distance;
    private bool touched = false;

    void OnCollisionEnter2D(Collision2D collider)
    {
        if(collider.gameObject.tag == "Player")
            if (touched == false)
            {
                Debug.Log(touched);
                StartCoroutine(collapse());
                Debug.Log(touched);
            }
    }

    public IEnumerator collapse()
    {
        float elapsedTime = 0;
        Vector2 currentPosition = transform.position;
        Vector2 NewPosition = new Vector2(transform.position.x, transform.position.y - distance);

        yield return new WaitForSeconds(timeBeforeDrop);
        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;
            transform.position = Vector2.Lerp(currentPosition, NewPosition, (elapsedTime / time));
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(returnTime);
        elapsedTime = 0;
        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;
            transform.position = Vector2.Lerp(NewPosition, currentPosition, (elapsedTime / time));
            yield return new WaitForEndOfFrame();
        }
    }
}
