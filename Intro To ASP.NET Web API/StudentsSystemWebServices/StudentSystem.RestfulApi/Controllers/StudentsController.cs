namespace StudentSystem.RestfulApi.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Web.Http;

    using Repositories;
    using Repositories.RepositoryInterfaces;
    using Repositories.RequestModels;

    public class StudentsController : BasicCrudController<StudentRequestModel>
    {
        private readonly IRepositoryAdvanced<StudentRequestModel> studentsRepository;

        public StudentsController() : this(new StudentsRepository())
        {
        }

        public StudentsController(IRepositoryAdvanced<StudentRequestModel> studentsRepository)
        {
            this.studentsRepository = studentsRepository;
        }

        protected override IRepositoryBasic<StudentRequestModel> Repository
        {
            get { return this.studentsRepository; }
        }

        public async Task<IHttpActionResult> Get(string educationForm)
        {
            try
            {
                IList<StudentRequestModel> students = await this.studentsRepository.GetByQueryString(
                    educationForm);

                if (students.Count > 0)
                {
                    return this.Ok(students);
                }
                else
                {
                    return this.NotFound();
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }

        }

        public async Task<IHttpActionResult> Get(string educationForm, string fname, string lname)
        {
            try
            {
                IList<StudentRequestModel> students = await this.studentsRepository.GetByQueryString(
                    educationForm, fname, lname);

                if (students.Count > 0)
                {
                    return this.Ok(students);
                }
                else
                {
                    return this.NotFound();
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }

        }

        public async Task<IHttpActionResult> Delete(StudentRequestModel student)
        {
            bool result = await this.studentsRepository.Delete(student);

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