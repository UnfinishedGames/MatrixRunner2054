using System;
using System.Collections.Generic;

class CameraConnection : SpecificAction
{
    public override void Interact(Node callingNode, PlayerCharacterSheet player, MissionManager missionManager)
    {
        var metaData = new Dictionary<Type, object>
        {
            { typeof(Node), callingNode }
        };
        missionManager.Inform(MissionEngine.GameAction.NodeHacked, metaData);
    }
}
