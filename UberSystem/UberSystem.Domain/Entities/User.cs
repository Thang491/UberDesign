﻿namespace UberSystem.Domain.Entities;

public partial class User
{
    public long Id { get; set; }

    public int? Role { get; set; }

    public string? UserName { get; set; }

    public string Email { get; set; } = null!;

    public string? Password { get; set; }
    public bool IsEmailConfirmed { get; set; }
    public virtual ICollection<EmailVerification> EmailVerifications { get; set; }


    public virtual ICollection<Customer> Customers { get; } = new List<Customer>();

    public virtual ICollection<Driver> Drivers { get; } = new List<Driver>();
}
