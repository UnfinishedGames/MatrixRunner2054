using System;

namespace MissionEngine
{
    public class MissionEngineStrategy
    {
        public static Mission Create(MissionType mission)
        {
            switch (mission)
            {
                case MissionType.CatAndMouse:
                    return new CatAndMouseMission();
                default:
                    throw new ArgumentOutOfRangeException("mission");
            }
        }
    }
}
