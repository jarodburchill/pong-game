using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBurchillFinalProject
{
    public class Paddle
    {
        private Texture2D Texture;
        public Rectangle Position;
        public int Speed = 12;
        public Keys UpControl;
        public Keys DownControl;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="position"></param>
        /// <param name="up"></param>
        /// <param name="down"></param>
        public Paddle(Texture2D texture, 
            Rectangle position, Keys up, Keys down)
        {
            Texture = texture;
            Position = position;
            UpControl = up;
            DownControl = down;
        }
        /// <summary>
        /// update paddle logic
        /// </summary>
        /// <param name="graphics"></param>
        public void Update(GraphicsDeviceManager graphics)
        {
            if (Position.Top > 0)
            {
                if (Keyboard.GetState().IsKeyDown(UpControl))
                    Position.Y -= Speed;
            }

            if (Position.Bottom < graphics.PreferredBackBufferHeight)
            {
                if (Keyboard.GetState().IsKeyDown(DownControl))
                    Position.Y += Speed;
            }
        }
        /// <summary>
        /// draws a paddle
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }
    }
}
