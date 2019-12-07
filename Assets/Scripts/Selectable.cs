using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Selectable : MonoBehaviour
{
    public Selectable connectedSelectable;

    public int songId;

    public virtual void SetSelectable(SongInfo songInfo) { }

    public virtual void SetupUI(string label)
    {
        Text text = GetComponentInChildren<Text>();

        text.text = label;
    }
}
