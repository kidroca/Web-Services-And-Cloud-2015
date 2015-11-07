namespace StudentSystem.Repositories
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using DataMappers;
    using RepositoryInterfaces;

    public abstract class BaseRepository<TContext, TDbModel, TRequestModel>
        : IRepositoryBasic<TRequestModel>
        where TContext : DbContext, new()
        where TDbModel : class
        where TRequestModel : class
    {
        private int transactionsCounter;

        protected TContext DbContext;

        protected IDbSet<TDbModel> dbSet;

        protected readonly IMediatorMapper<TDbModel, TRequestModel> dataMapper;

        protected BaseRepository(
            TContext dbContext,
            IMediatorMapper<TDbModel, TRequestModel> dataMapper,
            int maxTransactionsForInstance = 50)
        {
            this.DbContext = dbContext;
            this.dbSet = this.DbContext.Set<TDbModel>();

            this.dataMapper = dataMapper;
            this.MaxTransactionsForContextInstance = maxTransactionsForInstance;
            this.transactionsCounter = 0;
        }

        public int MaxTransactionsForContextInstance { get; set; }

        public async Task<TRequestModel> GetById(int id)
        {
            await this.IncrementTransactionCounter();
            var dataFromDb = await Task.Run(() => this.dbSet.Find(id));

            var result = this.dataMapper.ToRequestModel(dataFromDb);

            return result;
        }

        public async Task<IList<TRequestModel>> GetAll()
        {
            await this.IncrementTransactionCounter();
            var dataFromDb = await this.dbSet.ToListAsync();

            var resut = new List<TRequestModel>();
            foreach (TDbModel dbModel in dataFromDb)
            {
                resut.Add(this.dataMapper.ToRequestModel(dbModel));
            }

            return resut;
        }

        public async Task<bool> Update(int id, TRequestModel requestModel)
        {
            await this.IncrementTransactionCounter();
            var dbModel = await Task.Run(() => this.dbSet.Find(id));

            if (dbModel == null)
            {
                return false;
            }

            var requestModelProperties = requestModel.GetType().GetProperties();
            var databaseModelProperties = dbModel.GetType().GetProperties();

            foreach (PropertyInfo property in databaseModelProperties)
            {
                if (!property.PropertyType.IsValueType &&
                    !property.PropertyType.IsEquivalentTo(typeof(string)))
                {
                    continue;
                }

                var requestModelProperty = requestModelProperties.First(prop => prop.Name.Equals(property.Name));

                if (requestModelProperty != null)
                {
                    var value = requestModelProperty
                    .GetValue(requestModel);

                    if (value != null && !property.Name.Equals("Id"))
                    {
                        property.SetValue(dbModel, value);
                    }

                    var dbPropValue = property.GetValue(dbModel);

                    requestModelProperty.SetValue(requestModel, dbPropValue);
                }
            }

            int rowsChanged = await this.DbContext.SaveChangesAsync();
            return rowsChanged > 0;
        }

        public async Task<int> Create(TRequestModel requestModel)
        {
            await this.IncrementTransactionCounter();
            var asDbModel = this.dataMapper.ToDatabaseModel(requestModel);

            this.dbSet.Add(asDbModel);
            await this.DbContext.SaveChangesAsync();

            return (int)asDbModel.GetType().GetProperty("Id").GetValue(asDbModel);
        }

        public async Task<bool> Delete(int id)
        {
            await this.IncrementTransactionCounter();
            var toBeDeleted = await Task.Run(() => this.dbSet.Find(id));

            if (toBeDeleted != null)
            {
                this.dbSet.Remove(toBeDeleted);
                await this.DbContext.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        protected async Task IncrementTransactionCounter()
        {
            this.transactionsCounter++;

            if (this.transactionsCounter >= this.MaxTransactionsForContextInstance)
            {
                await this.RecycleDbContext();
                this.transactionsCounter = 0;
            }
        }

        private async Task RecycleDbContext()
        {
            await this.DbContext.SaveChangesAsync();
            this.DbContext.Dispose();
            this.DbContext = new TContext();
            this.dbSet = this.DbContext.Set<TDbModel>();
        }
    }
}
