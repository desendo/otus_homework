namespace Custom.Signals
{
    internal struct LevelUpRequest
    {
    }
    internal struct SaveRequest
    {
    }
    internal struct LoadRequest
    {
    }
    internal struct ResetRequest
    {
    }
    internal struct OpenLevelUpPopupRequest
    {
    }
    internal struct OpenUserInfoPopupRequest
    {
    }
    internal readonly struct AddDebugExpRequest
    {
        public int Exp { get; }

        public AddDebugExpRequest(int exp)
        {
            Exp = exp;
        }
    }
    internal struct AddRandomStatRequest
    {
    }
}