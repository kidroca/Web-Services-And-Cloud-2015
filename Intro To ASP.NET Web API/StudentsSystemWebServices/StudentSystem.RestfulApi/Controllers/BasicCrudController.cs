namespace StudentSystem.RestfulApi.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Repositories.RepositoryInterfaces;

    public abstract class BasicCrudController<TRequestModel> : ApiController where TRequestModel : class
    {
        protected abstract IRepositoryBasic<TRequestModel> Repository { get; }

        public async Task<IHttpActionResult> Get()
        {
            IList<TRequestModel> requestModel = await this.Repository.GetAll();

            return this.Ok(requestModel);
        }

        public async Task<IHttpActionResult> Get(int id)
        {
            TRequestModel requestModel = await this.Repository.GetById(id);

            if (requestModel != null)
            {
                return this.Ok(requestModel);
            }
            else
            {
                return this.NotFound();
            }
        }

        public async Task<IHttpActionResult> Post(TRequestModel requestModel)
        {
            int result;

            try
            {
                result = await this.Repository.Create(requestModel);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }

            if (result > 0)
            {
                return this.Created("api/students/" + result, requestModel);
            }
            else
            {
                return this.BadRequest("Could not create the object in the database");
            }
        }

        public async Task<IHttpActionResult> Put(int id, TRequestModel requestModel)
        {
            bool result = await this.Repository.Update(id, requestModel);

            if (result)
            {
                return this.Ok(requestModel);
            }
            else
            {
                return this.BadRequest("Invalid Id or nothing new to update");
            }
        }

        public async Task<IHttpActionResult> Delete(int id)
        {
            bool result = await this.Repository.Delete(id);

            if (result)
            {
                return this.Ok("Deleted successfully");
            }
            else
            {
                return this.NotFound();
            }
        }
    }
}