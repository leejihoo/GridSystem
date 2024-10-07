namespace Interface
{
    public interface IGridObject
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int OnGridPositionX { get; set; }
        public int OnGridPositionY { get; set; }
        public Size GridObjectInfo { get; set; }
        public void SetGridObjectInfo(Size size);
        public void Rotate();
    }
}
