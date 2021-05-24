namespace Algorithms.Algorithm
{
    /// <summary>
    /// Профиль настроек скорости алгоритма.
    /// </summary>
    public enum TimeProfile
    {
        Search = 1,
        Sorting = 2,
        DifSorting = 3
    }

    /// <summary>
    /// Стадии для скорости алгоритма.
    /// </summary>
    public enum TimeSwitch
    {
        Slow = 1,
        Medium = 2,
        Fast = 3
    }

    /// <summary>
    /// Хранит в себе настройки для 3 стадий скорости выполнения алгоритма.
    /// </summary>
	public struct TimeSetting
    {
        public int Slow { get; set; }
        public int Medium { get; set; }
        public int Fast { get; set; }

        public TimeSetting(int slow, int medium, int fast)
        {
            Slow = slow;
            Medium = medium;
            Fast = fast;
        }
    }
}
