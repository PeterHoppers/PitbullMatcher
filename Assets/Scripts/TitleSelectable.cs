public class TitleSelectable : Selectable
{
    string title;

    public override void SetSelectable(SongInfo song)
    {
        title = song.title;
        songId = song.songId;

        SetupUI(title);
    }
}
