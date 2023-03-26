﻿using System.ComponentModel;

namespace HospitalSanJoseModel
{
  public class UserCreateDTO
    {
   
        public string Password { get; set; } = null!;

        [DisplayName("Correo")]
        public string Email { get; set; } = null!;

        [DisplayName("Nombre")]
        public string FirstName { get; set; } = null!;

        [DisplayName("Apellido")]
        public string LastName { get; set; } = null!;


        [DisplayName("Eliminado")]
        public bool Deleted { get; set; }
        [DisplayName("Estado")]
        public bool Activated { get; set; }

        public string Username { get; set; } = null!;
        [DisplayName("Bloqueado")]
        public bool IsLocked { get; set; }

    }
}
