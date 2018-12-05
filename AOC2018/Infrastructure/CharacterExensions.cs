namespace AOC2018
{
    public static class CharacterExensions
    {
        public static bool IsReactiveWith(this char value, char other)
        {
            return (value != other) && (char.ToLower(value) == char.ToLower(other));
        }
    }
}
