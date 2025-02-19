using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;

namespace _2D_VR_Dancing_Simulator;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    public Texture2D tile;
    public Texture2D player_texture;
    public int pos_x;
    public int pos_y;
    public Color color;
    public bool canMove;
    public bool isMovingUp; 
    public bool isMovingLeft; 
    public bool isMovingDown; 
    public bool isMovingRight; 
    public int iterator;
    public int onBeat;
    public int offBeat;
    public bool isOnBeat;
    public bool didMove;
    private Song song;
    bool wPressed;
    bool aPressed;
    bool sPressed;
    bool dPressed;
    Texture2D empty;
    Texture2D map_tile;
    Texture2D up;
    int time;
    int[,] map2D = {
        { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
        { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
        { 0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0 },
        { 0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0 },
        { 0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0 },
        { 0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0 },
        { 0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0 },
        { 0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0 },
        { 0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0 },
        { 0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0 },
        { 0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0 },
        { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
        { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
    };
    public int turn;


    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        _graphics.IsFullScreen = false;
        _graphics.PreferredBackBufferWidth = 640;
        _graphics.PreferredBackBufferHeight = 416;
        
    }

        protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        
        map_tile = Content.Load<Texture2D>("tile-yellow");
        empty = Content.Load<Texture2D>("empty"); // empty tile 
        player_texture = Content.Load<Texture2D>("player-idle");
        up = Content.Load<Texture2D>("up");
        // TODO: use this.Content to load your game content here
    }

    protected override void Initialize()
    {

        if(MediaPlayer.State != MediaState.Stopped)
        {
        MediaPlayer.Stop(); // stop current audio playback if playing or paused.
        }
        song = Content.Load<Song>("at");
        MediaPlayer.Play(song);
        pos_x = 320;
        pos_y = 224;
        color = Color.White;
        canMove = true;
        iterator = 0;
        isOnBeat = true;
        onBeat = 0;
        offBeat = 0;
        didMove = false;
        wPressed = false;
        aPressed = false;
        sPressed = false;
        dPressed = false;
        base.Initialize();
    }
    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        if (Keyboard.GetState().IsKeyUp(Keys.W)) 
        {
            wPressed = false;
        }
        if (Keyboard.GetState().IsKeyUp(Keys.A)) 
        {
            aPressed = false;
        }
        if (Keyboard.GetState().IsKeyUp(Keys.S)) 
        {
            sPressed = false;
        }
        if (Keyboard.GetState().IsKeyUp(Keys.D)) 
        {
            dPressed = false;
        }

        if (isMovingUp) 
        {
            pos_y -= 4;
            iterator += 1;
            if (iterator == 8) 
            {
                isMovingUp = false;
                iterator = 0;
            }
        }
        else if (isMovingDown) 
        {
            pos_y += 4;
            iterator += 1;
            if (iterator == 8) 
            {
                isMovingDown = false;
                iterator = 0;
            }
        }
        else if (isMovingLeft) 
        {
            pos_x -= 4;
            iterator += 1;
            if (iterator == 8) 
            {
                isMovingLeft = false;
                iterator = 0;
            }
        }
        else if (isMovingRight) 
        {
            pos_x += 4;
            iterator += 1;
            if (iterator == 8) 
            {
                isMovingRight = false;
                iterator = 0;
            }
        }

        if (canMove) 
        {
            if (!wPressed && Keyboard.GetState().IsKeyDown(Keys.W)) 
            {
                isMovingUp = true;
                canMove = false;
                didMove = true;
                wPressed = true;
            }
            else if (!sPressed && Keyboard.GetState().IsKeyDown(Keys.S)) 
            {
                isMovingDown = true;
                canMove = false;
                didMove = true;
                sPressed = true;
            }
            else if (!dPressed && Keyboard.GetState().IsKeyDown(Keys.D)) 
            {
                isMovingRight = true;
                canMove = false;
                didMove = true;
                dPressed = true;
            }
            else if (!aPressed && Keyboard.GetState().IsKeyDown(Keys.A)) 
            {
                isMovingLeft = true;
                canMove = false;
                didMove = true;
                aPressed = true;
            }
        }

        if (didMove)
        {
            canMove = false;
        }


        if (isOnBeat)
        {
            onBeat++;
            if (!didMove) 
            {
                canMove = true;
            }
        }

        if (onBeat == 13) 
        {
            isOnBeat = false;
            onBeat = 0;
        }

        if (!isOnBeat) 
        {
            offBeat++;
            didMove = false;
        }

        if (offBeat == 13) 
        {
            isOnBeat = true;
            offBeat = 0;
            turn++;
        }


        // TODO: Add your update logic here

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        // TODO: Add your drawing code here
        _spriteBatch.Begin(samplerState: SamplerState.PointClamp);



        int counter = 0;
        int columns = map2D.GetLength(1);  // Number of columns
        Texture2D variable_texture;
        Rectangle rectangle;
        int real_row;
        int real_col;

        foreach (var item in map2D)
        {
            int col = counter / columns;
            int row = counter % columns;
            real_row = row * 32;
            real_col = col * 32;
            rectangle = new Rectangle(real_row, real_col, 32, 32);

            variable_texture = (item == 1) ? map_tile :  empty;

            _spriteBatch.Draw(texture: variable_texture, destinationRectangle: rectangle, color: color);
            counter++;
        }

        _spriteBatch.Draw(texture: player_texture, destinationRectangle: new Rectangle(x: pos_x, y: pos_y, width:32, height:32), color: color);
        
        if (turn == 9) {
            time++;
            if (time <= 11) {
                _spriteBatch.Draw(texture: up, destinationRectangle: new Rectangle(x: 0, y: -112, width: 640, height: 640), color: color);
                time = 0;
            }
        }
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
