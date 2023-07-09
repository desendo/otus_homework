using UnityEngine;

namespace Views
{
    public class UnitView : EntityMono
    {

        private int _team0Layer;
        private int _team1Layer;

        private void Awake()
        {
            _team0Layer = LayerMask.NameToLayer("Team1");
            _team1Layer = LayerMask.NameToLayer("Team2");
        }

        public override void SetTeam(int team)
        {
            base.SetTeam(team);
            if (team == 0)
            {
                var layer = _team0Layer;
                SetLayer(layer);
            }

            if (team == 1)
            {
                var layer = _team1Layer;
                SetLayer(layer);
            }
        }

    }
}