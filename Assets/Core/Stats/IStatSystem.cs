namespace Core.Game
{
    public interface IStatSystem
    {
        public void Initialize(string initialStats);
        public float GetValue(string name);
        public void AttachModifier(string statName, Modifier modifier);
        public void RemoveModifier(string statName, Modifier modifier);
    }
}
