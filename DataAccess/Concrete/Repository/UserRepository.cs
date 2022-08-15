using System.Runtime.Serialization;
using System.Xml;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Exception;
using Entities.Concrete;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.Repository;

public class UserRepository : EfEntityRepositoryBase<User, AppDbContext>, IUserRepository
{
    

    public UserRepository(AppDbContext context) : base(context)
    {
       
    }

    public  void UserNameControl(string userName)
    {
        var result = Context.Users.Where(x => x.UserName == userName).Any();
        if (result)
        {

            throw new System.Exception("The username you entered is in use, try a different username.");

        }
    }

    public void UserDeleteControl(Guid userId)
    {
        var result = Context.Users.Where(x => x.UserId == userId).Any(x => x.DeleteDate != null);
        //Query().Any(x => x.DeleteDate != null);
        if (result)
        {
            throw new System.Exception("Wrong Username");
        }

    }
}