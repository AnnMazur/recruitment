using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string? SecondName { get; set; }
        public string Role { get; set; } = default!;
        public DateTime CreatedAt { get; set; }

    }

    public class CreateUserRequest
    {
        public string Email { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string? SecondName { get; set; }
        public string Role { get; set; } = default!;
    }

    public class UpdateUserRequest
    {
        public string? Email { get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public string? SecondName { get; set; }
    }
}
