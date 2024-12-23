namespace CitySimGame.UI
{
    public class ToolbarCommand
    {
        public string Name { get; }
        public bool IsActive { get; set; }

        public ToolbarCommand(string name, bool isActive)
        {
            Name = name;
            IsActive = isActive;
        }
    }
} 