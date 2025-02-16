using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.ComponentModel;
using System.Diagnostics;

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

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        _graphics.IsFullScreen = false;
        _graphics.PreferredBackBufferWidth = 640;
        _graphics.PreferredBackBufferHeight = 400;
        
    }

        protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        
        player_texture = Content.Load<Texture2D>("player-idle");
        // TODO: use this.Content to load your game content here
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        if(MediaPlayer.State != MediaState.Stopped)
        {
        MediaPlayer.Stop(); // stop current audio playback if playing or paused.
        }
        song = Content.Load<Song>("at");
        MediaPlayer.Play(song);
        pos_x = 320;
        pos_y = 200;
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
            onBeat += 1;
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
            offBeat += 1;
            didMove = false;
        }

        if (offBeat == 13) 
        {
            isOnBeat = true;
            offBeat = 0;
        }


        // TODO: Add your update logic here

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here

        _spriteBatch.Begin(samplerState: SamplerState.PointClamp);

        _spriteBatch.Draw(texture: player_texture, destinationRectangle: new Rectangle(x: pos_x, y: pos_y, width:32, height:32), color: color);

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
