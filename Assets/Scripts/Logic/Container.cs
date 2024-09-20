namespace Logic
{
    public class Container
    {
        public void RegisterSingle<T>(T implementation) 
            => Implementation<T>.Instance = implementation;

        public void MakeNullSingle<T>() where T : class => Implementation<T>.Instance = null;

        public bool IsExists<T>() => Implementation<T>.Instance != null;

        public T GetSingle<T>() => Implementation<T>.Instance;

        private static class Implementation<T>
        {
            public static T Instance;
        }
    }
}