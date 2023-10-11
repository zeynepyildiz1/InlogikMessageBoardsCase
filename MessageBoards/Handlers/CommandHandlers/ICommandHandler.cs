namespace MessageBoards.Application.Queries;

public interface ICommandHandler<T>
{
    void Handle(T command);
}

