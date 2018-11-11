using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace FinalProject31
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        Texture2D backGround, explosionTexture;
        SoundEffect music;
        SoundEffect boingSound;
        Rectangle viewportRect;
        SpriteBatch spriteBatch;
        GameObject cannon;
        Texture2D natureBG;
        float scale=0.02f;

        float increment = 5;
        static int imageWidth = 800;
        static int imageHieght = 600;
        
        Vector2 backPos1 = Vector2.Zero;
        Vector2 backPos2 = new Vector2(-imageWidth, 0);

       
        SpriteFont lucidaConsole;
        ExplosionManager explosions = new ExplosionManager(); 
        Vector2 scorePosition = new Vector2(50, 50);            // the position to draw the score

        int bulletCounter=0;
       

        const int maxCannonBalls = 8;
        GameObject[] bullets;


        KeyboardState previousKeyboardState = Keyboard.GetState();

        const int maxEnemies = 3;
        const float maxEnemyHeight = 0.1f;
        const float minEnemyHeight = 0.5f;

        const float maxEnemyvelacity = 5.0f;
        const float minEnemyvelacity = 10.0f;

        Random random = new Random(1);
        GameObject[] enemies;

        int score;
        SpriteFont font;
        Vector2 scoreDrawPoint = new Vector2(0.1f, 0.1f);
        bool pause=false;


        public enum GameState
        {
            START,              // first intro screen displayed
            PREPARELEVEL,       // get ready for the next level
            PLAYING,            // playing a level
            GAMEOVER,
            GAMEPAUSE,
            
       
         // game over, show final score 
        }
        GameState gameState = new GameState();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = imageWidth;
            graphics.PreferredBackBufferHeight = imageHieght;

           
            gameState = GameState.START;
            //  gameState = GameState.GAMEOVER;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.

            spriteBatch = new SpriteBatch(GraphicsDevice);
            ContentManager contentManager = new ContentManager(this.Services, @"Content\");
            music= contentManager.Load<SoundEffect>("audio\\music");
            boingSound = Content.Load<SoundEffect>("audio\\boingSound");
            natureBG = Content.Load<Texture2D>("Sprites\\nature_backgrounds_048");

            SoundEffectInstance instance =music.CreateInstance();
            instance.IsLooped = true;
            instance.Play();

            lucidaConsole = Content.Load<SpriteFont>("Fonts/GameFont");
            backGround = Content.Load<Texture2D>
            ("Sprites\\background");
            explosionTexture = Content.Load<Texture2D>("Sprites\\explod5");
            cannon = new GameObject(Content.Load<Texture2D>("Sprites\\cannon"));
            cannon.position = new Vector2(120, graphics.GraphicsDevice.Viewport.Height - 80);
            explosions.Initialize(explosionTexture);
            bullets = new GameObject[maxCannonBalls];

            for (int i = 0; i < maxCannonBalls; i++)
            {
                bullets[i] = new GameObject(Content.Load<Texture2D>(
               "Sprites\\bullet"));
            }
            enemies = new GameObject[maxEnemies];
            for (int i = 0; i < maxEnemies; i++)
            {
                enemies[i] = new GameObject(
               Content.Load<Texture2D>("Sprites\\enemy"));
            }
            font = Content.Load<SpriteFont>("Fonts\\GameFont");

            viewportRect = new Rectangle(0, 0,
            graphics.GraphicsDevice.Viewport.Width,
            graphics.GraphicsDevice.Viewport.Height);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>fff
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
 protected override void Update(GameTime gameTime)
 {
            // Allows the game to exit
            int maxX = 600;
            //graphics.GraphicsDevice.Viewport.Width - cannon.Width;
            int minX = 180;
            int minY = 250;
            int maxY = 400;
            // int MaxY = graphics.GraphicsDevice.Viewport.Height -cannon.Height;
            // int MinY = 0;
            KeyboardState keyboardState = Keyboard.GetState();


            KeyboardState keyState = Keyboard.GetState();
            GamePadState padState = GamePad.GetState(PlayerIndex.One);
            if (keyState.IsKeyDown(Keys.Escape))
                this.Exit();
            // this is the first level

            if (keyboardState.IsKeyDown(Keys.P))
            {
                pause = true;
                gameState = GameState.GAMEPAUSE;
            }

            if (keyboardState.IsKeyDown(Keys.R))
            {
                pause = false;
                gameState = GameState.PLAYING;
            }

            if (!pause)
            {

             if (gameState == GameState.START)
             {
             if (keyState.IsKeyDown(Keys.S))
             {
              music.Play();
              score = 0;
              gameState = GameState.PLAYING;
              }
              }
              if (gameState == GameState.PLAYING)
               {
                  keyboardState = playing(maxX, minX,maxY,minY, keyboardState);
                 explosions.Update(gameTime);

                }
                else if (gameState == GameState.GAMEOVER)
                {
                    if (keyboardState.IsKeyDown(Keys.B))
                        gameState = GameState.START;
                }
                backPos1.X += increment; backPos2.X += increment;

                    UpdatePosition(ref  backPos1);
                    UpdatePosition(ref  backPos2);

                base.Update(gameTime);
            }
        }

        private void UpdatePosition(ref Vector2 position)
        {
            if (position.X >= imageWidth) // gone off right of the screen
            {
                position.X = position.X - (2 * imageWidth);
            }
            else if (position.X < -imageWidth) // gone off left of the screen
            {
                position.X = position.X + (2 * imageWidth);
            }
        }

        private KeyboardState playing(int maxX,int minX,int maxY,int minY, KeyboardState keyboardState)
        {

            if (keyboardState.IsKeyDown(Keys.Left))
            {
                cannon.rotation -= 0.1f;
            }
            if (keyboardState.IsKeyDown(Keys.Right))
            {
                cannon.rotation += 0.1f;
            }

            if (keyboardState.IsKeyDown(Keys.S))
            {

                cannon.position.X += 2;
                if (cannon.position.X > maxX)
                {
                    cannon.position.X = maxX;
                }

            }
            if (keyboardState.IsKeyDown(Keys.A))
            {
                cannon.position.X -= 2;

                if (cannon.position.X < minX)
                {
                    cannon.position.X = minX;
                }
               }

            if (keyboardState.IsKeyDown(Keys.Down))
            {
                cannon.position.Y += 2;
                if (cannon.position.Y > maxY)
                {
                    cannon.position.Y = maxY;
                }
            }
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                cannon.position.Y -= 2;

                if (cannon.position.Y < minY)
                {
                    cannon.position.Y = minY;
                }
            }
            if (keyboardState.IsKeyDown(Keys.F) &&
               previousKeyboardState.IsKeyUp(Keys.F))
            {
               
                FireCannonBall();
             }
           // cannon.rotation = MathHelper.Clamp(cannon.rotation, -MathHelper.PiOver4, 0);
            previousKeyboardState = keyboardState;
            UpdateCannonBalls();
            UpdateEnemies();
            return keyboardState;
}
        float scaleA=8.0f, scaleB=2.0f;
        float mycount = 0;
public void UpdateEnemies()
{
            foreach (GameObject enemy in enemies)
            {
                if (enemy.alive)
                {
                    enemy.position += enemy.velocity;
                    if (!viewportRect.Contains(new Point(
                        (int)enemy.position.X,
                        (int)enemy.position.Y)))
                    {
                        enemy.alive = false;
                    }
                }
                else
                {
                    enemy.alive = true;
                    enemy.position = new Vector2(
                        viewportRect.Right,
                        MathHelper.Lerp(
                        (float)viewportRect.Height * minEnemyHeight,
                        (float)viewportRect.Height * maxEnemyHeight,
                        (float)random.NextDouble()));
                    scaleA += scale; scaleB += scale;
                    enemy.velocity = new Vector2(-(float)((random.NextDouble() * scaleA) + scaleB), (float)(random.NextDouble() * scaleA));
                }

                // check collision with player
                   Rectangle enemyRect = new Rectangle(
                  (int)enemy.position.X,
                  (int)enemy.position.Y,
                  enemy.sprite.Width,
                  enemy.sprite.Height);
                   Rectangle playerRect = new Rectangle(
                    (int)(cannon.position.X-cannon.centre.X),
                    (int)(cannon.position.Y-cannon.centre.Y) ,
                    cannon.sprite.Width,
                    cannon.sprite.Height);
                if(enemyRect.Intersects(playerRect))
                {
                    mycount++;
                    Console.WriteLine(mycount);
                    enemy.alive = false;
                    score -= 1;
                }

            }
}

public void UpdateCannonBalls()
{
      foreach (GameObject ball in bullets)
             
      {
                    if (ball.alive)
                    {
                    ball.position += ball.velocity;
   
                    if (!viewportRect.Contains(new Point(
                        (int)ball.position.X,
                        (int)ball.position.Y)))
                    {
                        ball.alive = false;
                        continue;
                    }
                    Rectangle cannonBallRect = new Rectangle(
                        (int)ball.position.X,
                        (int)ball.position.Y,
                        ball.sprite.Width,
                        ball.sprite.Height);

                    foreach (GameObject enemy in enemies)
                    {
                        Rectangle enemyRect = new Rectangle(
                            (int)enemy.position.X,
                            (int)enemy.position.Y,
                            enemy.sprite.Width,
                            enemy.sprite.Height);

                        if (cannonBallRect.Intersects(enemyRect))
                        {
                            ball.alive = false;
                            enemy.alive = false;
                            boingSound.Play();
                            score += 1;
                         
                            if (score == 100)
                            {
                                gameState = GameState.GAMEOVER;
                                scaleA = 8.0f; scaleB = 2.0f;

                            }
                            explosions.CreateExplosion(enemy.position, 0.2f);
                            break;
                        }

                    }
                }
            }
        }
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        /// 
        public void FireCannonBall()
        {
            foreach (GameObject ball in bullets)
            {
                if (!ball.alive)
                {
                    ball.alive = true;
                    ball.position = cannon.position - ball.centre;
                    ball.velocity = new Vector2(
                        (float)Math.Cos(cannon.rotation),
                        (float)Math.Sin(cannon.rotation)) * 5.0f;

                    bulletCounter += 1;
                    
                   // Window.Title =(bulletCounter.ToString());
                    return;
                }
            }
        }
        protected override void Draw(GameTime gameTime)
        {
            KeyboardState keyState = Keyboard.GetState();
            graphics.GraphicsDevice.Clear(Color.DarkOrchid);
            spriteBatch.Begin(SpriteBlendMode.AlphaBlend);
          
            if (gameState == GameState.START)
            {
                bulletCounter = 0;
                spriteBatch.Draw(natureBG, new Vector2 (0,0), Color.White);
                spriteBatch.DrawString(font,
               "         WELCOME TO DEEP SEA WAR, PRESS S TO START "+"\n"+
               "                Press F to fire the bullets"+"\n"+
               "                Press right arrow to rotated clockwise "+"\n"+
               "                Press left arrow to rotate anti-clockwise "+"\n"+
               "                Press up arrow to translate upwards "+"\n"+
               "                Press down arrow to translate downwards"+"\n"+
               "                Press s to translate rightwards"+"\n"+
               "                Press a to translate leftwards",
               new Vector2(scoreDrawPoint.X, scoreDrawPoint.Y * viewportRect.Height),
                Color.Orange);


            }
           
            if (gameState == GameState.PLAYING)
            {
               // graphics.GraphicsDevice.Clear(Color.Aquamarine);
                spriteBatch.Draw(backGround, backPos1, Color.White);
                spriteBatch.Draw(backGround, backPos2, Color.White);
                foreach (GameObject ball in bullets)
                {
                    if (ball.alive)
                    {
                       
                        spriteBatch.Draw(ball.sprite,
                            ball.position, Color.White);
                    }
                }
 

                    spriteBatch.Draw(cannon.sprite, cannon.position, null,
                        Color.White, cannon.rotation, cannon.centre, 1.0f,
                        SpriteEffects.None, 0);
                    explosions.Draw(spriteBatch);

                    foreach (GameObject enemy in enemies)
                    {
                        if (enemy.alive)
                        {
                            spriteBatch.Draw(enemy.sprite,
                               enemy.position, Color.White);
                        }
                    }
                    spriteBatch.DrawString(font,
                   "Score:" + score.ToString()+"\n"+"Total Number of Bullets Fired: "+bulletCounter.ToString(),

                   new Vector2(scoreDrawPoint.X, scoreDrawPoint.Y * viewportRect.Height),
                    Color.Red);
                }
                else if (gameState == GameState.GAMEOVER)
            {
                graphics.GraphicsDevice.Clear(Color.Yellow);
                 
                    spriteBatch.DrawString(font,"                                             Game Over!"+"\n"+
                    "\n"+
                   "                        You made the Top Score,which is: "+score+"\n"+
                   "\n" +
                   "                        By Firing: " + bulletCounter +"  Bullets"+"\n"+
                    "\n" +
                   "                        Press B to play again" + "\n"+
                   "\n" +
                   "                        Press ESC to exit" + "\n",
               
                   new Vector2(scoreDrawPoint.X, scoreDrawPoint.Y * viewportRect.Height),
                    Color.Green);
                 }
            else if (gameState == GameState.GAMEPAUSE)
            {
                graphics.GraphicsDevice.Clear(Color.BlanchedAlmond);

                spriteBatch.DrawString(font,
               "Game is Paused Press R to return " + score + "\n" + bulletCounter,

               new Vector2(scoreDrawPoint.X, scoreDrawPoint.Y * viewportRect.Height),
                Color.Green);
            }
                spriteBatch.End();
                // TODO: Add your drawing code here

                base.Draw(gameTime);
            }
        }
    }

