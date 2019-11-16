using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleTrader.Domain.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
    }
}
