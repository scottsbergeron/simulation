namespace CitySimGame.UI
{
    public class ToolbarTab
    {
        public string Name { get; }
        public ToolbarCommand[] Commands { get; }

        public ToolbarTab(string name, ToolbarCommand[] commands)
        {
            Name = name;
            Commands = commands;
        }
    }
} 