using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;

namespace JBurchillFinalProject
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Pong : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private const int paddleWidth = 16;
        private const int paddleHeight = 100;
        private const int padding = 10;
        private int gameWidth;
        private int gameHeight;

        public static Paddle player1;
        public static Paddle player2;
        private Ball ball;
        private Score score;
        private Menu menu;

        public static bool gameStart = false;
        public static bool menuActive = true;
        public static string message = "Press Space to Begin";
        public static MenuSelection menuSelection;

        private SpriteFont font;
        private SpriteFont largeFont;
        private Texture2D background;
        private Texture2D menuBackground;
        private Texture2D helpBackground;
        private Texture2D creditsBackground;
        private KeyboardState oldState;

        /// <summary>
        /// constructor
        /// </summary>
        public Pong()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferHeight = 720;
            graphics.PreferredBackBufferWidth = 1280;

            gameWidth = graphics.PreferredBackBufferWidth;
            gameHeight = graphics.PreferredBackBufferHeight;
        }
        /// <summary>
        /// initializes
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
        }
        /// <summary>
        /// loads content on load
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //font
            font = Content.Load<SpriteFont>("font");

            //largeFont
            largeFont = Content.Load<SpriteFont>("largeFont");

            //Paddle
            var paddleTexture = Content.Load<Texture2D>("paddle");
            Rectangle player1Position = new Rectangle(padding, 
                (gameHeight / 2) - (paddleHeight / 2),
                paddleWidth, paddleHeight);
            Rectangle player2Position = new Rectangle
                (gameWidth - paddleWidth - padding,
                (gameHeight / 2) - (paddleHeight / 2),
                paddleWidth, paddleHeight);
            player1 = new Paddle(paddleTexture,
                player1Position, Keys.W, Keys.S);
            player2 = new Paddle(paddleTexture,
                player2Position, Keys.Up, Keys.Down);

            //Ball
            var ballTexture = Content.Load<Texture2D>("ball");
            Rectangle ballPosition = new Rectangle
                ((gameWidth / 2) - (16 / 2),
                (gameHeight / 2) - (16 / 2), 16, 16);
            ball = new Ball(graphics, ballTexture, ballPosition);

            //Score
            score = new Score(font, graphics);

            //menu
            menu = new Menu(font, largeFont, graphics);

            //background
            background = Content.Load<Texture2D>("bg");
            menuBackground = Content.Load<Texture2D>("menu");
            helpBackground = Content.Load<Texture2D>("help");
            creditsBackground = Content.Load<Texture2D>("credits");

            //song
            var song = Content.Load<Song>("pongTheme");
            MediaPlayer.Play(song);
            MediaPlayer.IsRepeating = true;
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {

        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape) && 
                oldState.IsKeyUp(Keys.Escape))
            {
                if (menuActive)
                {
                    Exit();
                }
                else
                {
                    menuActive = true;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.F6) && 
                oldState.IsKeyUp(Keys.F6))
            {
                if (!MediaPlayer.IsMuted)
                {
                    MediaPlayer.IsMuted = true;
                }
                else
                {
                    MediaPlayer.IsMuted = false;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.F11) &&
                oldState.IsKeyUp(Keys.F11))
            {
                graphics.ToggleFullScreen();
            }

            if (menuActive)
            {
                menu.Update();
            }
            else if (menuSelection == MenuSelection.Play)
            {
                UpdatePlay(gameTime);
            }

            oldState = Keyboard.GetState();
            base.Update(gameTime);
        }
        /// <summary>
        /// update gameplay logic
        /// </summary>
        /// <param name="gameTime"></param>
        private void UpdatePlay(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space) &&
                oldState.IsKeyUp(Keys.Space))
            {
                gameStart = true;
            }
            if (gameStart)
            {
                player1.Update(graphics);
                player2.Update(graphics);

                ball.Update(graphics, gameTime, player1, player2,
                    Content.Load<SoundEffect>("paddleSound"),
                    Content.Load<SoundEffect>("wallSound"),
                    Content.Load<SoundEffect>("scoreSound"));
            }
            oldState = Keyboard.GetState();
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            if (menuActive)
            {
                spriteBatch.Draw(menuBackground, 
                    new Vector2(0, 0), Color.White);
                menu.Draw(spriteBatch);
            }
            else
            {
                switch (menuSelection)
                {
                    case MenuSelection.Play:
                        spriteBatch.Draw(background,
                            new Vector2(0,0), Color.White);
                        DrawPlay();
                        break;
                    case MenuSelection.Help:
                        spriteBatch.Draw(helpBackground, 
                            new Vector2(0, 0), Color.White);
                        break;
                    case MenuSelection.Credits:
                        spriteBatch.Draw(creditsBackground, 
                            new Vector2(0, 0), Color.White);
                        break;
                }
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
        /// <summary>
        /// draws gameplay
        /// </summary>
        private void DrawPlay()
        {
            player1.Draw(spriteBatch);
            player2.Draw(spriteBatch);

            ball.Draw(spriteBatch, font);

            score.Draw(spriteBatch);

            if (!gameStart)
            {
                spriteBatch.DrawString(font, message,
                    new Vector2(graphics.PreferredBackBufferWidth / 2 - 280,
                    graphics.PreferredBackBufferHeight - 150), Color.White);
            }
        }
    }
}
