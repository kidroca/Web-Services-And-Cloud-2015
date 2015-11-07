namespace StudentSystem.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;

    using DataLink;
    using DataMappers;
    using DbModels;
    using RepositoryInterfaces;
    using RequestModels;

    public class StudentsRepository :
        BaseRepository<StudentsDbContext, Student, StudentRequestModel>,
        IRepositoryAdvanced<StudentRequestModel>
    {
        public StudentsRepository()
            : this(
                new StudentsDbContext(),
                new MediatorMapper<Student, StudentRequestModel>(new DataMapper()))
        {
        }

        public StudentsRepository(
            StudentsDbContext dbContext,
            IMediatorMapper<Student, StudentRequestModel> dataMapper,
            int maxTransactionsForInstance = 50)
            : base(dbContext, dataMapper, maxTransactionsForInstance)
        {
        }

        public async Task<IList<StudentRequestModel>> GetObjectLike(StudentRequestModel model)
        {
            await this.IncrementTransactionCounter();
            IQueryable<Student> query = this.DbContext.Students;
            if (model.FirstName != null)
            {
                query = query.Where(s => s.FirstName.Equals(model.FirstName, StringComparison.Ordinal));
            }

            if (model.LastName != null)
            {
                query = query.Where(s => s.LastName.Equals(model.LastName, StringComparison.Ordinal));
            }

            if (model.EducationForm != null)
            {
                query = query.Where(s => s.EducationForm == model.EducationForm);
            }

            if (model.Email != null)
            {
                query = query.Where(s => s.Email.Equals(model.Email, StringComparison.Ordinal));
            }

            if (model.PhoneNumber != null)
            {
                query = query.Where(s => s.PhoneNumber.Equals(model.PhoneNumber, StringComparison.Ordinal));
            }

            var dataFromDb = await query.ToListAsync();

            var result = new List<StudentRequestModel>();

            foreach (Student student in dataFromDb)
            {
                result.Add(this.dataMapper.ToRequestModel(student));
            }

            return result;
        }

        public async Task<IList<StudentRequestModel>> GetByQueryString(params string[] queryString)
        {
            await this.IncrementTransactionCounter();
            IQueryable<Student> query = this.DbContext.Students;

            if (queryString.Length > 0)
            {
                EducationForm form =
                    (EducationForm)Enum.Parse(typeof(EducationForm), queryString[0]);

                query = query.Where(s => s.EducationForm == form);
            }

            if (queryString.Length > 1)
            {
                string fname = queryString[1];
                query = query.Where(s => s.FirstName.Contains(fname));
            }

            if (queryString.Length > 2)
            {
                string lname = queryString[2];
                query = query.Where(s => s.LastName.Contains(lname));
            }

            var dataFromDb = await query.ToListAsync();

            var result = new List<StudentRequestModel>();

            foreach (Student student in dataFromDb)
            {
                result.Add(this.dataMapper.ToRequestModel(student));
            }

            return result;
        }

        public async Task<bool> Delete(StudentRequestModel model)
        {
            await this.IncrementTransactionCounter();
            var toBeDeleted = await this.DbContext.Students
                .FirstAsync(s =>
                    s.FirstName.Equals(model.FirstName) &&
                    s.LastName.Equals(model.LastName) &&
                    s.Email.Equals(model.Email) &&
                    s.PhoneNumber.Equals(model.PhoneNumber) &&
                    s.EducationForm.Equals(model.EducationForm));

            if (toBeDeleted != null)
            {
                this.DbContext.Students.Remove(toBeDeleted);
                await this.DbContext.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}