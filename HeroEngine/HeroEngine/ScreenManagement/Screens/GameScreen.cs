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
using HeroEngine.Sound;
using HeroEngine.LevelEditing;
using HeroEngine.Render;
using HeroEngine.Operations;
using HeroEngine.AI;
using HeroEngine.CoreGame;
using System.IO;
using HeroEngine.Objects;
using HeroEngineShared;
namespace HeroEngine.ScreenManagement.Screens
{
    class GameScreen : ScreenComponent
    {
        MusicPlayer mplayer;

        ControlBinding bind = new ControlBinding();

        TileRenderer tile_render;

        public SpriteFont def_font;

        Player local_player;

        public Camera camera;

        EntityManager EntityManager;

        HUD headsup;

        Map current_map = LevelGenerator.CreateLevel(EngineLimit.TotalMapAreaHeight, "Random");

        public Debug debug = new Debug();

        KeyCheck debug_key = new KeyCheck(Keys.F10);

        KeyCheck pause_key;

        public Lighting g_lighting = new Lighting();

        public GameScreen(Game _game, ScreenManager sman)
            : base(_game, sman) { }

        public override void Initialize()
        {
           base.Initialize();
        }

        public override void LoadContent()
        {
            if (!File.Exists(Environment.CurrentDirectory + @"\\WorldFiles\" + "Random" + ".wld")) //If the current map does not exist.
                current_map.WriteToFile(); //Write the map to the file.

            Song[] music = new Song[GameResources.music.CountResources()]; //Array for songs.
            string[] names = new string[GameResources.music.CountResources()]; //Song names.

            for (int i = 0; i < GameResources.music.CountResources(); i++) //Grab music for music player.
			{
			  music[i] = (Song)GameResources.music.GetResourceByIndex(i); names[i] = music[i].Name + " - " + music[0].Artist;
			}
            mplayer = new MusicPlayer(music, names); //Create the music player.
            bind.LoadBinding(Constants.HeroEngine_Folder_Config + Constants.HeroEngine_Config_Binding); //Get the bind for the player.

            //Create the player, adds him into dat EntityManager Later on.

            camera = new Camera(Game);//Create the camera for the game.

            EntityManager = new Objects.EntityManager(camera, this,smanager.SpriteBatch,current_map);
            local_player = new Player("local_player", 100, current_map.local_player_position, true, Game.GraphicsDevice.Viewport, bind, EntityManager, EntityManager.SpriteBatch,camera);
            EntityManager.AddEntity(new ent_blank("Blanky",EntityManager,EntityManager.SpriteBatch,camera));
            EntityManager.AddEntity(local_player);

            tile_render = new TileRenderer(current_map, smanager.GraphicsDevice.Viewport, Game, camera,this); //Create the tile renderer

            Texture2D[] bars = new Texture2D[2];
            bars[0] = (Texture2D)GameResources.textures.GetResource("HPBAR");
            bars[1] = (Texture2D)GameResources.textures.GetResource("ARMOUR");
            headsup = new HUD(Game.GraphicsDevice.Viewport,(Texture2D)GameResources.textures.GetResource("MINIMAP"),bars);
    //        console = new Render.Console(def_font);

            pause_key = new KeyCheck(local_player.binding.GetBoundKeyName("PAUSE"));
            EngineLimit.SetTileScale();  //Find our tilescale for the map so the players do not get a bigger or smaller map if they change the res.
            base.LoadContent();
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gt)
        {
            mplayer.UpdateSong();//Check for a new song
            EntityManager.Think();
           // UIFramework.GetKeyboardText.GetKeyChar(); //Nothing atm

            current_map.PassTimeBy(); //Time rolls on in the map.

            g_lighting.Update(current_map); //Also update our lighting

            if (debug.isDebugging) //Are we debugging, if not then lets save time.
                //console.Update();
                debug.Update(
                    current_map,
                    tile_render.tilesrendered, 
                    (int)(g_lighting.RawLight[1] * 255), 
                    Frames.GetFramesPerSecond(gt), 
                    "X" + local_player.Marker.x.ToString() + " Y" + local_player.Marker.y.ToString(),
                    local_player.speed_buffer.X + " " + local_player.speed_buffer.Y,
                    "X" + Math.Round((decimal)camera.CameraPosOnWorld.X, 2) + " Y" + Math.Round((decimal)camera.CameraPosOnWorld.Y, 2),
                    ((int)local_player.rotation).ToString(),
                    this
                    );

            if (debug_key.KeyDownAndUp()) //Does the player want to debug.
            {
               debug.isDebugging = Toggle.ToggleBool(debug.isDebugging);
               //console.On = Toggle.ToggleBool(console.On);
            }
            headsup.Update(local_player); //Keep our Heads up display correct.

            if (pause_key.KeyDownAndUp() && this.State != ScreenState.Covered) //Do we want to pause
            {
                    this.State = ScreenState.Covered; //Cover the screen.
                    MediaPlayer.Pause(); //Pause the music.
                    smanager.AddScreen(new MenuScreen(Game, smanager, "Paused", "Menu stuffs", new Screen.Cursor(), true, this)); //Add back our menu screen.
            }

            //Update Tile Renderer
            tile_render.Update(this, local_player); //Update the tile renderer.

            base.Update(gt);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sb = smanager.SpriteBatch; //Get the batch
            
            screenManager.GraphicsDevice.Clear(Color.Black); //Clear the screen

            sb.Begin(SpriteSortMode.Deferred,BlendState.Opaque,SamplerState.AnisotropicClamp,DepthStencilState.Default,RasterizerState.CullNone,null,Resolution.getTransformationMatrix());
            //Tell it how we like our sprites drawn.
            
            tile_render.Draw(sb,local_player); //Tell the tile renderer to draw.
            
            sb.End(); //End the sprite batch.
            EntityManager.Draw(); //The drawing is done by the EntityManager.
            sb.Begin(SpriteSortMode.Texture, BlendState.NonPremultiplied); //Do another one for the hud.
            
            mplayer.DrawSongTitle((SpriteFont)GameResources.fonts.GetResource("MenuItem"), sb); 
            
            headsup.Draw(sb, current_map, tile_render); //Draw the hud.
            
            if (debug.isDebugging) //Debug hood is different.
            {
                debug.Draw(sb, (SpriteFont)GameResources.fonts.GetResource("MenuItem"));
                //console.Draw(sb, Game.GraphicsDevice.Viewport);
            }

            sb.End();
            base.Draw(gameTime);
        }

        }
    }
