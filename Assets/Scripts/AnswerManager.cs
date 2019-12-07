using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AnswerManager : MonoBehaviour
{
    public GameObject emptyConnector;
    List<Connector> connectors = new List<Connector>();
    public void SubmitAnswer(Selectable music, Selectable title)
    {
        var musicConnector = CheckForMusic(music);
        var titleConnector = CheckForTitle(title);

        if (musicConnector == null)
        {
            if (titleConnector == null)
            {
                var newConnector = Instantiate(emptyConnector, transform).GetComponent<Connector>();
                newConnector.CreateConnector(music, title);
                connectors.Add(newConnector);
            }
            else
            {
                titleConnector.UpdateConnector(music, title);
            }
        }
        else
        {            
            if (titleConnector != musicConnector)
            {
                if (titleConnector != null)
                {
                    connectors.Remove(titleConnector);
                    titleConnector.DestroyConnector();
                }

                musicConnector.UpdateConnector(music, title);
            }            
        }
    }
    Connector CheckForMusic(Selectable music)
    {
        var musicConnectors = connectors.Where(x => x.musicSelectable.songId == music.songId);

        if (musicConnectors.Any())
        {
            return musicConnectors.First();
        }
        else
        {
            return null;
        }
    }
    Connector CheckForTitle(Selectable title)
    {
        var titleConnectors = connectors.Where(x => x.titleSelectable.songId == title.songId);

        if (titleConnectors.Any())
        {
            return titleConnectors.First();
        }
        else
        {
            return null;
        }
    }
}
