using BareBonesDotNetApi.Entities;
using StudentApi.Data;

namespace BareBonesDotNetApi.Repositories
{
    public class EntityFrameworkUsersRepository : IUsersRepository
    {
        private readonly StudentListContext dbContext;

        public EntityFrameworkUsersRepository(StudentListContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<User> Create(User user)
        {
            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<User> Delete(string username)
        {
            var userToDelete = dbContext.Users.FirstOrDefault(user => user.Username == username);

            if (userToDelete != null)
            {
                dbContext.Users.Remove(userToDelete);
                await dbContext.SaveChangesAsync();
                return userToDelete;
            }
            else
            {
                return new User();
            }
        }

        public async Task<User> Get(string username)
        {
            return await dbContext.Users.FindAsync(username);
        }

        public IEnumerable<User> GetAll()
        {
            return dbContext.Users.ToList();
        }

        public async Task<User> SoftDelete(int id, string newPassword)
        {
            throw new NotImplementedException();
        }
    }
}