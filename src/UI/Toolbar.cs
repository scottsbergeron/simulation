using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CitySimGame.UI
{
    public class Toolbar
    {
        private const int TOOLBAR_HEIGHT = 100;
        private const int TAB_WIDTH = 100;
        private const int COMMAND_SIZE = 80;
        private const int PADDING = 10;

        private Rectangle _bounds;
        private ToolbarTab[] _tabs;
        private int _activeTabIndex;

        public Toolbar(int screenWidth, int screenHeight)
        {
            _bounds = new Rectangle(0, screenHeight - TOOLBAR_HEIGHT, screenWidth, TOOLBAR_HEIGHT);
            
            // Create tabs
            _tabs = new ToolbarTab[]
            {
                new ToolbarTab("Roads", new ToolbarCommand[]
                {
                    new ToolbarCommand("Straight Road", false)
                })
            };
        }

        public void HandleClick(Point mousePosition)
        {
            if (!_bounds.Contains(mousePosition))
                return;

            // Check tab clicks
            Rectangle tabBounds = new Rectangle(_bounds.X, _bounds.Y, TAB_WIDTH, TOOLBAR_HEIGHT);
            for (int i = 0; i < _tabs.Length; i++)
            {
                if (tabBounds.Contains(mousePosition))
                {
                    _activeTabIndex = i;
                    return;
                }
                tabBounds.X += TAB_WIDTH;
            }

            // Check command clicks in active tab
            if (_activeTabIndex >= 0 && _activeTabIndex < _tabs.Length)
            {
                var tab = _tabs[_activeTabIndex];
                Rectangle commandBounds = new Rectangle(
                    _bounds.X + TAB_WIDTH * (_tabs.Length) + PADDING,
                    _bounds.Y + PADDING,
                    COMMAND_SIZE,
                    COMMAND_SIZE);

                foreach (var command in tab.Commands)
                {
                    if (commandBounds.Contains(mousePosition))
                    {
                        command.IsActive = !command.IsActive;
                        // Deactivate other commands
                        foreach (var otherCommand in tab.Commands)
                        {
                            if (otherCommand != command)
                                otherCommand.IsActive = false;
                        }
                        return;
                    }
                    commandBounds.X += COMMAND_SIZE + PADDING;
                }
            }
        }

        public bool IsCommandActive(string commandName)
        {
            foreach (var tab in _tabs)
            {
                foreach (var command in tab.Commands)
                {
                    if (command.Name == commandName && command.IsActive)
                        return true;
                }
            }
            return false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw toolbar background
            Texture2D pixel = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            pixel.SetData(new[] { Color.LightGray });
            spriteBatch.Draw(pixel, _bounds, Color.White);

            // Draw tabs
            Rectangle tabBounds = new Rectangle(_bounds.X, _bounds.Y, TAB_WIDTH, TOOLBAR_HEIGHT);
            for (int i = 0; i < _tabs.Length; i++)
            {
                Color tabColor = i == _activeTabIndex ? Color.White : Color.Gray;
                spriteBatch.Draw(pixel, tabBounds, tabColor);
                tabBounds.X += TAB_WIDTH;
            }

            // Draw commands for active tab
            if (_activeTabIndex >= 0 && _activeTabIndex < _tabs.Length)
            {
                var tab = _tabs[_activeTabIndex];
                Rectangle commandBounds = new Rectangle(
                    _bounds.X + TAB_WIDTH * (_tabs.Length) + PADDING,
                    _bounds.Y + PADDING,
                    COMMAND_SIZE,
                    COMMAND_SIZE);

                foreach (var command in tab.Commands)
                {
                    Color commandColor = command.IsActive ? Color.Yellow : Color.DarkGray;
                    spriteBatch.Draw(pixel, commandBounds, commandColor);
                    commandBounds.X += COMMAND_SIZE + PADDING;
                }
            }
        }
    }
} 