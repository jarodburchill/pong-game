using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBurchillFinalProject
{
    public class Ball
    {
        private Texture2D Texture;
        public Rectangle Position;
        public int VerticalSpeed;
        public int HorizontalSpeed;

        private float timer;
        private string winner = "";

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="texture"></param>
        /// <param name="position"></param>
        public Ball(GraphicsDeviceManager graphics,
            Texture2D texture, Rectangle position)
        {
            Texture = texture;
            Position = position;
            NewServe(graphics);
        }

        /// <summary>
        /// updates ball logic
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="gameTime"></param>
        /// <param name="paddle1"></param>
        /// <param name="paddle2"></param>
        /// <param name="paddleSound"></param>
        /// <param name="wallSound"></param>
        /// <param name="scoreSound"></param>
        public void Update(GraphicsDeviceManager graphics,
            GameTime gameTime, Paddle paddle1, Paddle paddle2,
            SoundEffect paddleSound, SoundEffect wallSound,
            SoundEffect scoreSound)
        {
            // passes through the left of gamespace
            if (Position.Right < 0)
            {
                if (Score.player2Score < 9)
                {
                    Score.player2Score++;
                    scoreSound.Play();
                    NewServe(graphics);
                }
                else
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.Space))
                    {
                        Restart(graphics);
                        return;
                    }

                    Score.player2Score++;
                    scoreSound.Play();

                    Pong.gameStart = false;
                    Pong.message = "Press Space for Menu";
                    winner = "Player 2";
                }
            }
            // passes through the Right of gamespace
            if (Position.Left > graphics.PreferredBackBufferWidth)
            {
                if (Score.player1Score < 9)
                {
                    Score.player1Score++;
                    scoreSound.Play();
                    NewServe(graphics);
                }
                else
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.Space))
                    {
                        Restart(graphics);
                        return;
                    }

                    Score.player1Score++;
                    scoreSound.Play();

                    Pong.gameStart = false;
                    Pong.message = "Press Space to Restart";
                    winner = "Player 1";
                }
            }

            // hits top or bottom of gamespace
            if (BottomCollision(graphics) || TopCollision(graphics))
            {
                VerticalSpeed *= -1;
                wallSound.Play();
            }

            // hits top or bottom corner of left paddle
            if (LeftPaddleCollision(paddle1) && 
                (TopPaddleCollision(paddle1) ||
                BottomPaddleCollision(paddle1)))
            {
                VerticalSpeed *= -1;
                HorizontalSpeed *= -1;

                paddleSound.Play(1.0f, 0.5f, 0.0f);

                VerticalSpeedUp(5);
            }
            // hits top or bottom corner of right paddle
            else if (RightPaddleCollision(paddle2) &&
                (TopPaddleCollision(paddle2) || 
                BottomPaddleCollision(paddle2)))
            {
                VerticalSpeed *= -1;
                HorizontalSpeed *= -1;

                paddleSound.Play(1.0f, 0.5f, 0.0f);

                VerticalSpeedUp(5);
            }
            // hits front of the left or right paddle
            else if (LeftPaddleCollision(paddle1) ||
                RightPaddleCollision(paddle2))
            {
                HorizontalSpeed *= -1;
                paddleSound.Play();
            }
            // hits the top or bottom of the left paddle
            else if (TopPaddleCollision(paddle1) ||
                BottomPaddleCollision(paddle1))
            {
                VerticalSpeed *= -2;
                paddleSound.Play();
            }
            // hits the top or bottom of the right paddle
            else if (TopPaddleCollision(paddle2) ||
                BottomPaddleCollision(paddle2))
            {
                VerticalSpeed *= -2;
                paddleSound.Play();
            }
            //Speeds up ball evrey 5 seconds
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timer > 5)
            {
                HorizontalSeedUp(1);
                VerticalSpeedUp(1);
                timer = 0;
            }

            Position.X += HorizontalSpeed;
            Position.Y += VerticalSpeed;
            
        }
        /// <summary>
        /// speeds up ball verticaly at a rate
        /// </summary>
        /// <param name="rate"></param>
        private void VerticalSpeedUp(int rate)
        {
            if (VerticalSpeed < 0)
            {
                VerticalSpeed -= rate;
            }
            else
            {
                VerticalSpeed += rate;
            }
        }
        /// <summary>
        /// speeds up ball horizontally at a rate
        /// </summary>
        /// <param name="rate"></param>
        private void HorizontalSeedUp(int rate)
        {
            if (HorizontalSpeed < 0)
            {
                HorizontalSpeed -= rate;
            }
            else
            {
                HorizontalSpeed += rate;
            }
        }
        /// <summary>
        /// serves a new ball
        /// </summary>
        /// <param name="graphics"></param>
        private void NewServe(GraphicsDeviceManager graphics)
        {
            Position = new Rectangle
                ((graphics.PreferredBackBufferWidth / 2) - (16 / 2),
                (graphics.PreferredBackBufferHeight / 2) - (16 / 2), 16, 16);
            VerticalSpeed = 7;
            HorizontalSpeed = 7;

            Random rnd = new Random();
            switch (rnd.Next(0, 4))
            {
                case 0:
                    VerticalSpeed *= 1;
                    HorizontalSpeed *= 1;
                    break;
                case 1:
                    VerticalSpeed *= -1;
                    HorizontalSpeed *= 1;

                    break;
                case 2:
                    VerticalSpeed *= 1;
                    HorizontalSpeed *= -1;

                    break;
                case 3:
                    VerticalSpeed *= -1;
                    HorizontalSpeed *= -1;

                    break;
            }

            HorizontalSpeed *= -1;
            timer = 0;
        }
        /// <summary>
        /// restarts game
        /// </summary>
        /// <param name="graphics"></param>
        private void Restart(GraphicsDeviceManager graphics)
        {
            Pong.gameStart = false;
            Pong.menuActive = true;
            NewServe(graphics);
            Score.player1Score = 0;
            Score.player2Score = 0;
            winner = "";
            Pong.message = "Press Space to Begin";
            Pong.player1.Position.Y = 
                (graphics.PreferredBackBufferHeight / 2) - 50;
            Pong.player2.Position.Y = 
                (graphics.PreferredBackBufferHeight / 2) - 50;
        }
        /// <summary>
        /// bottom game collision
        /// </summary>
        /// <param name="graphics"></param>
        /// <returns></returns>
        public bool BottomCollision(GraphicsDeviceManager graphics)
        {
            if (Position.Top < graphics.PreferredBackBufferHeight &&
            Position.Bottom > graphics.PreferredBackBufferHeight)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Top game collision
        /// </summary>
        /// <param name="graphics"></param>
        /// <returns></returns>
        public bool TopCollision(GraphicsDeviceManager graphics)
        {
            if (Position.Bottom > 0 &&
            Position.Top < 0)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// left paddle collision
        /// </summary>
        /// <param name="paddle"></param>
        /// <returns></returns>
        public bool LeftPaddleCollision(Paddle paddle)
        {
            if (Position.Left + HorizontalSpeed < paddle.Position.Right &&
            Position.Right > paddle.Position.Right &&
            Position.Bottom > paddle.Position.Top &&
            Position.Top < paddle.Position.Bottom)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// right paddle collision
        /// </summary>
        /// <param name="paddle"></param>
        /// <returns></returns>
        public bool RightPaddleCollision(Paddle paddle)
        {
            if (Position.Right + HorizontalSpeed > paddle.Position.Left &&
            Position.Left < paddle.Position.Left &&
            Position.Bottom > paddle.Position.Top &&
            Position.Top < paddle.Position.Bottom)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// top paddle collision
        /// </summary>
        /// <param name="paddle"></param>
        /// <returns></returns>
        public bool TopPaddleCollision(Paddle paddle)
        {
            if (Position.Top + VerticalSpeed < paddle.Position.Bottom &&
            Position.Bottom > paddle.Position.Bottom &&
            Position.Right > paddle.Position.Left &&
            Position.Left < paddle.Position.Right)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// bottom paddle collision
        /// </summary>
        /// <param name="paddle"></param>
        /// <returns></returns>
        public bool BottomPaddleCollision(Paddle paddle)
        {
            if (Position.Bottom + VerticalSpeed > paddle.Position.Top &&
            Position.Top < paddle.Position.Top &&
            Position.Right > paddle.Position.Left &&
            Position.Left < paddle.Position.Right)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// draws ball and win message for ball
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="font"></param>
        public void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            Vector2 location;

            if (winner == "Player 1")
            {
                location = new Vector2(30, 10);
                spriteBatch.DrawString(font, $"{winner} Wins!",
                    location, Color.White);
            }
            else if(winner == "Player 2")
            {
                location = new Vector2(900, 10);
                spriteBatch.DrawString(font, $"{winner} Wins!",
                    location, Color.White);
            }

            spriteBatch.Draw(Texture, Position, Color.White);
        }
    }
}
