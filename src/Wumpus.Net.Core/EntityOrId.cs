namespace Wumpus
{
    public struct EntityOrId<T>
    {
        public Snowflake Id { get; }
        public T Object { get; }

        public EntityOrId(Snowflake id)
        {
            Id = id;
            Object = default;
        }
        public EntityOrId(T obj)
        {
            Id = 0;
            Object = obj;
        }
    }
}
