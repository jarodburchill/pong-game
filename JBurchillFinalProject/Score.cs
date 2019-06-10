using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBurchillFinalProject
{
    public class Score
    {
        private SpriteFont Font;
        public GraphicsDeviceManager Graphics;
        public static int player1Score { get; set; }
        public static int player2Score { get; set; }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="spriteFont"></param>
        /// <param name="graphics"></param>
        public Score(SpriteFont spriteFont, GraphicsDeviceManager graphics)
        {
            Font = spriteFont;
            Graphics = graphics;
        }
        /// <summary>
        /// draws scores
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Font, player1Score.ToString(), 
                new Vector2((Graphics.PreferredBackBufferWidth / 2) - 210, 70),
                Color.White);
            spriteBatch.DrawString(Font, player2Score.ToString(), 
                new Vector2((Graphics.PreferredBackBufferWidth / 2) + 170, 70), 
                Color.White);
        }
    }
}
