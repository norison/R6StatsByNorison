namespace R6Stats
{
    interface IMessage<T>
    {
        void SendMessage(T message);
    }
}
