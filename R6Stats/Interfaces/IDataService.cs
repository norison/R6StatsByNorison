namespace R6Stats
{
    interface IDataService<T>
    {
        T Load();
        void Save(T data);
    }
}
