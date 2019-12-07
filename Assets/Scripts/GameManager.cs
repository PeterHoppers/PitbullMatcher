using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    UIManager uiManager;
    public AnswerManager answerManager;

    public GameObject songLibrary;

    public Transform musicChoices;
    public Transform titleChoices;    

    Selectable previousSelection;
    
    // Start is called before the first frame update
    void Start()
    {
        uiManager = GetComponent<UIManager>();
        //Insert Level Selector Here
        var songs = songLibrary.transform.Find("Easy").GetComponentsInChildren<SongInfo>().ToList();
        CreateSongList(songs);        
    }

    void CreateSongList(List<SongInfo> songs)
    {
        songs = Shuffle(songs);

        for (int i = 0; i < songs.Count; i++)
        {
            SongInfo song = songs[i];
            song.songId = i;

            GameObject songContainer = Instantiate(uiManager.basicSelectable, musicChoices);
            var selectable = songContainer.AddComponent<MusicSelectable>();
            selectable.SetSelectable(song);

            Button button = songContainer.GetComponent<Button>();
            Color buttonColor = uiManager.SetColorGradiant(i, songs.Count);

            uiManager.SetColorsOfButton(button, buttonColor);

            button.onClick.AddListener(delegate { CheckSelection(selectable); });
        }

        songs = Shuffle(songs);

        for (int i = 0; i < songs.Count; i++)
        {
            GameObject titleContainer = Instantiate(uiManager.basicSelectable, titleChoices);
            var selectable = titleContainer.AddComponent<TitleSelectable>();
            selectable.SetSelectable(songs[i]);
            titleContainer.GetComponent<Button>().onClick.AddListener(delegate { CheckSelection(selectable); });
        }
    }


    void CheckSelection(Selectable selected)
    {
        if (previousSelection == null) previousSelection = selected;
        else
        {            
            if (previousSelection.GetType() == typeof(MusicSelectable))
            {
                if (selected.GetType() == typeof(MusicSelectable))
                    previousSelection = selected;
                else
                    ConnectSelectables(previousSelection, selected);
            }
            else
            {
                if (selected.GetType() == typeof(MusicSelectable))
                    ConnectSelectables(selected, previousSelection);
                else
                    previousSelection = selected;
            }                        
        }
    }

    private void ConnectSelectables(Selectable musicSelectable, Selectable titleSelectable)
    {
        if (musicSelectable.connectedSelectable != null)
        {
            uiManager.SetDefaultColorToButton(musicSelectable.connectedSelectable.gameObject.GetComponent<Button>());
        }

        answerManager.SubmitAnswer(musicSelectable, titleSelectable);

        uiManager.SetColorsOfButton(titleSelectable.GetComponent<Button>(), musicSelectable.GetComponent<Button>().colors.normalColor);

        previousSelection = null;
    }               
    
    public void CheckAnswers()
    {
        int correctAmt = 0;
        Camera.main.GetComponent<AudioSource>().Stop();
        MusicSelectable[] songs = musicChoices.GetComponentsInChildren<MusicSelectable>();

        foreach (MusicSelectable song in songs)
        {
            Button musicButton = song.gameObject.GetComponent<Button>();
            //musicImage.color = Cp
            if (song.connectedSelectable == null)
            {
                uiManager.SetButtonToResult(musicButton, Color.yellow);
                //musicImage.color = Color.yellow;
            }
            else
            {
                if (song.songId == song.connectedSelectable.songId)
                {
                    uiManager.SetButtonToResult(musicButton, Color.green);
                    uiManager.SetButtonToResult(song.connectedSelectable.GetComponent<Button>(), Color.green);
                    correctAmt++;
                }
                else
                {
                    uiManager.SetButtonToResult(musicButton, Color.red);
                    uiManager.SetButtonToResult(song.connectedSelectable.GetComponent<Button>(), Color.red);
                }
            }
        }

        uiManager.SetResultsText(correctAmt);
    }

    List<T> Shuffle<T>(List<T> list)
    {
        System.Random rnd = new System.Random();
        for (int i = 0; i < list.Count; i++)
        {
            int k = rnd.Next(0, i);
            T value = list[k];
            list[k] = list[i];
            list[i] = value;
        }
        return list;
    }
}

public static class Difficulty {
    public enum DiffcultyChoices { Easy, Hard}
}

