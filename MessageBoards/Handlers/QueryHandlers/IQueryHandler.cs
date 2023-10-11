namespace MessageBoards.Handlers;

public interface IQueryHandler<TQuery, TResult>
{
    TResult Handle(TQuery query);
}