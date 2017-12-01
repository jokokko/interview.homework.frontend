namespace Demo.Backend.Infrastructure
{
    internal interface IInjectFault
    {
        int SinceEveryNthRequest { get; }
        int WithMillisecondsOfOutage { get; }
    }
}