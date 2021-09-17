using CartoPrime.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using TM.Utils;
using TM.Utils.Bases;
using TM.Utils.Delegates;

namespace CartoPrime.Application.Parameters
{
    public class UserParams : BaseParams<User>
    {
        public String Key { get; set; }
        public String UserName { get; set; }
        public String Nome { get; set; }
        public String Email { get; set; }
        public String CPF { get; set; }

        public override Expression<Func<User, bool>> Filter()
        {
            var predicate = PredicateBuilder.New<User>();

            if (!string.IsNullOrWhiteSpace(Key))
            {
                var user = new User { Key = Key.UrlDecode() };
                predicate = predicate.And(p => p.Id.Equals(user.Id));
            }

            if (Id.HasValue)
            {
                predicate = predicate.And(p => p.Id == Id);
            }

            if (!string.IsNullOrWhiteSpace(UserName))
            {
                predicate = predicate.And(p => p.UserName.Equals(UserName));
            }

            if (!string.IsNullOrWhiteSpace(CPF))
            {
                predicate = predicate.And(p => p.CPF.Equals(CPF));
            }

            if (!string.IsNullOrWhiteSpace(Email))
            {
                predicate = predicate.And(p => p.Email.Equals(Email));
            }


            return predicate;
        }
    }
}
