namespace VUDK.Features.Main.PointsSystem.Data
{
    using VUDK.Features.Main.SaveSystem.Data;

    [System.Serializable]
    public class PointsSaveData : SaveDataBase
    {
        public int Points;

        public PointsSaveData(int points)
        {
            Points = points;
        }
    }
}