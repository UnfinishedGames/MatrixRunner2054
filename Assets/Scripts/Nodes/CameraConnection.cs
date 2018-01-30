class CameraConnection : SpecificConnection
{
    internal override void Interact(MissionManager missionManager)
    {
        missionManager.Inform(MissionEngine.GameAction.HackedWinningNode);
    }
}
