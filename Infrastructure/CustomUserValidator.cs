﻿using MagicMirror.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MagicMirror.Infrastructure
{
    public class CustomUserValidator : UserValidator<AppUser>
    {
        public override async Task<IdentityResult> ValidateAsync(
            UserManager<AppUser> manager,
            AppUser user)
        {
            IdentityResult result = await base.ValidateAsync(manager, user);

            var errors = result.Succeeded ?
                new List<IdentityError>() : result.Errors.ToList();

            return errors.Count == 0 ? IdentityResult.Success
                : IdentityResult.Failed(errors.ToArray());
        }
    }
}