﻿using Microsoft.AspNetCore.Mvc.Rendering;

namespace HospitalSanJoseModel
{
    public class UserRole
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int RoleId { get; set; }

        public  Role Role { get; set; } = null!;

        public  User User { get; set; } = null!;

        public Response? Response { get; set; }
    }
}
