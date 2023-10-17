using BareBonesDotNetApi.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace BareBonesDotNetApi.Repositories;

public interface IUsersRepository
{
    Task<User> Create(User user);
    Task<User> Delete(string username);
    Task<User> Get(string username);
    IEnumerable<User> GetAll();
    Task<User> SoftDelete(int id, string newPassword);
}