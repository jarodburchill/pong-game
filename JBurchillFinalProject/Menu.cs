using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBurchillFinalProject
{
    public enum MenuSelection
    {
        Play,
        Help,
        Credits
    }
    public class Menu
    {
        private SpriteFont SmallFont;
        private SpriteFont LargeFont;
        private SpriteFont MenuItem1;
        private SpriteFont MenuItem2;
        private SpriteFont MenuItem3;

        public GraphicsDeviceManager Graphics;

        public int selectedIndex = 0;
        public KeyboardState oldState;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="smallFont"></param>
        /// <param name="largeFont"></param>
        /// <param name="graphics"></param>
        public Menu(SpriteFont smallFont,
            SpriteFont largeFont, GraphicsDeviceManager graphics)
        {
            SmallFont = smallFont;
            LargeFont = largeFont;
            Graphics = graphics;

            MenuItem1 = smallFont;
            MenuItem2 = smallFont;
            MenuItem3 = smallFont;
        }
        /// <summary>
        /// update menu logic
        /// </summary>
        public void Update()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Down) &&
                oldState.IsKeyUp(Keys.Down))
            {
                if (selectedIndex < 2)
                {
                    selectedIndex++;
                }
                else
                {
                    selectedIndex = 0;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Up) &&
                oldState.IsKeyUp(Keys.Up))
            {
                if (selectedIndex > 0)
                {
                    selectedIndex--;
                }
                else
                {
                    selectedIndex = 2;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Enter) && 
                oldState.IsKeyUp(Keys.Enter) || 
                Keyboard.GetState().IsKeyDown(Keys.Space) && 
                oldState.IsKeyUp(Keys.Space))
            {
                Pong.menuActive = false;

                if (selectedIndex == 0)
                {
                    Pong.menuSelection = MenuSelection.Play;
                }
                else if (selectedIndex == 1)
                {
                    Pong.menuSelection = MenuSelection.Help;
                }
                else if (selectedIndex == 2)
                {
                    Pong.menuSelection = MenuSelection.Credits;
                }
            }

            oldState = Keyboard.GetState();

            switch (selectedIndex)
            {
                case 0:
                    MenuItem1 = LargeFont;
                    MenuItem2 = SmallFont;
                    MenuItem3 = SmallFont;
                    break;
                case 1:
                    MenuItem1 = SmallFont;
                    MenuItem2 = LargeFont;
                    MenuItem3 = SmallFont;
                    break;
                case 2:
                    MenuItem1 = SmallFont;
                    MenuItem2 = SmallFont;
                    MenuItem3 = LargeFont;
                    break;
            }
        }
        /// <summary>
        /// draws menu items
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(MenuItem1, "Play",
                new Vector2((Graphics.PreferredBackBufferWidth / 2) - 80, 270),
                Color.White);
            spriteBatch.DrawString(MenuItem2,"Help", 
                new Vector2((Graphics.PreferredBackBufferWidth / 2) - 80, 390), 
                Color.White);
            spriteBatch.DrawString(MenuItem3, "Credits", 
                new Vector2((Graphics.PreferredBackBufferWidth / 2) - 110, 510), 
                Color.White);
        }
    }
}
