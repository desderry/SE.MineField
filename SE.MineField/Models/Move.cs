namespace SE.MineField.Models
{
    public class Move
    {
        public Move(int xPosition, int yPosition)
        {
            XPosition = xPosition;
            YPosition = yPosition;
        }

        public int XPosition { get; }
        public int YPosition { get; }
    }
}