using MyTestProject.Models;

namespace MyTestProject.Data;

public static class UserLogins
{
    public static UserLogin StandardUser => new(){ UserName = "standard_user", Password = "secret_sauce"};
    public static UserLogin ProblemUser => new(){ UserName = "problem_user", Password = "secret_sauce"};
    public static UserLogin StandardUserBadPassword => new(){ UserName = "standard_user", Password = "special_sauce"};
    public static UserLogin LockedOutUser => new(){ UserName = "locked_out_user", Password = "secret_sauce"};
    
    public static List<UserLogin> SuccessfulLogins => new(){ StandardUser, ProblemUser };
}