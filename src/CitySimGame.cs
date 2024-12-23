using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using CitySimGame.RoadNetwork;
using CitySimGame.UI;

namespace CitySimGame
{
    public class CitySimGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private RoadManager _roadManager;
        private MouseState _previousMouseState;
        private Toolbar _toolbar;

        public CitySimGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _roadManager = new RoadManager();
            _previousMouseState = Mouse.GetState();
            _toolbar = new Toolbar(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            base.Initialize();
        }

        protected override void Update(GameTime gameTime)
        {
            MouseState currentMouseState = Mouse.GetState();

            // Handle mouse click
            if (currentMouseState.LeftButton == ButtonState.Released &&
                _previousMouseState.LeftButton == ButtonState.Pressed)
            {
                Point mousePosition = new Point(currentMouseState.X, currentMouseState.Y);
                
                // First check if we clicked on the toolbar
                _toolbar.HandleClick(mousePosition);
                
                // If the road tool is active and we didn't click on the toolbar,
                // handle road placement
                if (_toolbar.IsCommandActive("Straight Road"))
                {
                    Vector2 clickPosition = new Vector2(currentMouseState.X, currentMouseState.Y);
                    _roadManager.HandleClick(clickPosition);
                }
            }

            _previousMouseState = currentMouseState;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _roadManager.Draw(_spriteBatch);
            _toolbar.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
