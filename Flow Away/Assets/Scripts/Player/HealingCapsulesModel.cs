class HealingCapsulesModel
{
    private int _capsulesCount;

    public int Count
    {
        get => _capsulesCount;
        set => _capsulesCount = value;
    }

    public HealingCapsulesModel(int initCount)
    {
        _capsulesCount = initCount;
    }
}