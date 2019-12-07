using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connector : MonoBehaviour
{
    public Selectable musicSelectable;
    public Selectable titleSelectable;    

    LineRenderer lineRenderer;

    public void CreateConnector(Selectable music, Selectable title)
    {
        lineRenderer = gameObject.GetComponent<LineRenderer>();

        UpdateConnector(music, title);
    }

    public void UpdateConnector(Selectable music, Selectable title)
    {
        titleSelectable = title;
        musicSelectable = music;

        musicSelectable.connectedSelectable = titleSelectable;
        titleSelectable.connectedSelectable = musicSelectable;

        var titlePos = titleSelectable.transform.position;
        var musicPos = musicSelectable.transform.position;

        lineRenderer.SetPosition(0, new Vector3(titlePos.x, titlePos.y, titlePos.z - 0.01f));
        lineRenderer.SetPosition(1, new Vector3(musicPos.x, musicPos.y, musicPos.z - 0.01f));
    }

    public void DestroyConnector()
    {
        musicSelectable.connectedSelectable = null;
        titleSelectable.connectedSelectable = null;

        Destroy(gameObject);
    }
}
