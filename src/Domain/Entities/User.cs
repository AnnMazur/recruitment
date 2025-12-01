using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;

namespace Domain.Entities
{
    public class User : BaseEntity
    {
        public string Email { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string? SecondName { get; set; }
        public UserRole Role { get; set; }

        public List<Interview> CreatedInterviews { get; set; } = new();
        public List<Interview> ConductedInterviews { get; set; } = new();
    }
}

