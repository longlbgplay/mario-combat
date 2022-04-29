using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class marioDead : MonoBehaviour
{
    Vector2 deadLocation;
    private void Update()
    {
        StartCoroutine(deadAnim());
    }
    IEnumerator deadAnim()
    {
        while (true)
        {
            float bump = 15f;
            transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y + bump * Time.deltaTime);
            if (transform.localPosition.y >= deadLocation.y + 60f) break;
            yield return null;
            transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y - bump * Time.deltaTime);
            if (transform.localPosition.y <= -10f)
            {
                deadLocation = transform.localPosition;
                GameObject marioDead = (GameObject)Instantiate(Resources.Load("Prefab/characters_13"));
                marioDead.transform.localPosition = deadLocation;
                Destroy(gameObject);
                FindObjectOfType<checkPoint>().restart();
                break;
            }
            yield return null;
        }
    }
}
