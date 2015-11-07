namespace StudentSystem.Repositories.DataMappers
{
    public interface IMediatorMapper<TDbModel, TRequestModel>
        where TDbModel : class
        where TRequestModel : class
    {
        TDbModel ToDatabaseModel(TRequestModel requestModel);

        TRequestModel ToRequestModel(TDbModel databaseModel);
    }
}
