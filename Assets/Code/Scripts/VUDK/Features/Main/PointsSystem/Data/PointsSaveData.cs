namespace VUDK.Features.Main.PointsSystem.Data
{
    using VUDK.Features.Main.SaveSystem.Data;

    [System.Serializable]
    public class PointsSaveData : SaveData
    {
        public int Points;

        public PointsSaveData(int points)
        {
            Points = points;
        }
    }
}