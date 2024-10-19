using Boshphelm.GameEvents;

namespace Boshphelm.Missions
{
    public abstract class RepeatableMission : BaseMission
    {
        protected readonly int requiredRepetitions;
        protected int currentRepetitions;

        protected RepeatableMission(MissionInfo info, int requiredRepetitions) : base(info)
        {
            this.requiredRepetitions = requiredRepetitions;
            currentRepetitions = 0;
        }

        public override object SaveMission() => currentRepetitions;

        public override void LoadProgress(object progress)
        {
            if (progress is int loadedRepetitions)
            {
                currentRepetitions = loadedRepetitions;
            }
        }

        public override bool CheckCompletion() => currentRepetitions >= requiredRepetitions;

        protected void IncrementRepetitions()
        {
            currentRepetitions++;

            float progress = (float)currentRepetitions / requiredRepetitions;
            UpdateProgress(progress);
        }

        public override abstract void OnNotify(IGameEvent gameEvent);
    }
}
