﻿using Microsoft.AspNetCore.Identity;

namespace Proje.Models.Domain
{
    public class ApplicationUser:IdentityUser
    {
        public  String FullName { get; set; }
    }
}
