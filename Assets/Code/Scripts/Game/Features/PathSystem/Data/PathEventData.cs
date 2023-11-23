namespace ProjectM.Features.PathSystem.Data
{
    using Cinemachine;

    [System.Serializable]
    public struct PathEventData
    {
        public CinemachinePathBase PathToFollow;
        public int WaypointTargetIndex;

        public PathEventData(CinemachinePathBase pathToFollow, int waypointTargetIndex)
        {
            PathToFollow = pathToFollow;
            WaypointTargetIndex = waypointTargetIndex;
        }
    }
}