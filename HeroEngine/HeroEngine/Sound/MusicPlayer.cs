using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
namespace HeroEngine.Sound
{
    class MusicPlayer
    {
        Song[] _playlist;
        string[] _playlistnames;
        int song_in_play;
        public MusicPlayer(Song[] playlist, string[] playlist_names)
        {
            _playlist = playlist;
            _playlistnames = playlist_names;
        }

        public void UpdateSong()
        {
                if (MediaPlayer.State == MediaState.Stopped)
                {
                    MediaPlayer.Play(GetRandomSong());
                }
        }

        public Song GetRandomSong()
        {
            int song_number = new Random(DateTime.Now.Second).Next(0, _playlist.Length);
            song_in_play = song_number;
            return _playlist[song_number];

        }

        public void DrawSongTitle(SpriteFont font, SpriteBatch sb)
        {
            sb.DrawString(font, _playlistnames[song_in_play], new Vector2(300, 10), Color.Aquamarine);
        }
    }
}
