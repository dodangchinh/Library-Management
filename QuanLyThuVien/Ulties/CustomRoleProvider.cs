using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace QuanLyThuVien
{
    public class CustomRoleProvider : RoleProvider
    {
        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override bool IsUserInRole(string username, string roleName)
        {
            using (var context = new LibraryContext())
            {
                var account = context.Account.FirstOrDefault(x => x.Name == username);
                if (account == null)
                    return false;
                return context.AccountRole.Any(accountRole => accountRole.AccountID == account.Id && accountRole.RoleID == roleName);
            }
        }

        public override string[] GetRolesForUser(string username)
        {
            var account = HttpContext.Current.Session["Logged"] as Account;
            if (account == null)
                return new string[] { };
            using (var context = new LibraryContext())
            {
                var a = context.AccountRole
                    .Where(accountRole => accountRole.AccountID == account.Id)
                    .Select(accountRole => accountRole.role.Name)
                    .ToArray();
                return a;
            }
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }
    }
}