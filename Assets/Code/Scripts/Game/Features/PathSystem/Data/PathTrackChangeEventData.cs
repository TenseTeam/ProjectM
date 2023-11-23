namespace ProjectM.Features.PathSystem.Data
{
    using Cinemachine;

    [System.Serializable]
    public struct PathTrackChangeEventData
    {
        public CinemachinePathBase NewPath;
        public int StartWaypointOnChange;

        public PathTrackChangeEventData(CinemachinePathBase newPath, int startWaypointOnChange)
        {
            NewPath = newPath;
            StartWaypointOnChange = startWaypointOnChange;
        }
    }
}