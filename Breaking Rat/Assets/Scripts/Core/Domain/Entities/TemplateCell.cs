namespace BreakingRat.Assets.Scripts.Core.Domain.Entities
{
    public struct TemplateCell
    {
        public bool LeftWall;
        public bool BottomWall;

        public int X;
        public int Y;

        public override int GetHashCode() =>
            X.GetHashCode() * 31 + Y.GetHashCode();

        public override bool Equals(object obj)
        {
            if (obj is TemplateCell == false)
                return false;

            var cell = (TemplateCell)obj;

            return X == cell.X && Y == cell.Y;
        }
    }
}
