using Infrastructure.Repository;

namespace Infrastructure.Persistence
{
    public class UnitOfWork
    {
        private readonly AppDbContext _dbContext;
        private readonly StudentRepository _studentRepo;

        public UnitOfWork(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _studentRepo = new StudentRepository(dbContext);
        }

        public StudentRepository StudentRepo { get => _studentRepo; }

        public async Task<int> SaveChange()
        {
            int rowEffected = 0;
            try
            {
                rowEffected = await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Dispose();
                Console.WriteLine(ex.Message);
            }
            return rowEffected;
        }

        // Release memory if saveChange() has error
        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}