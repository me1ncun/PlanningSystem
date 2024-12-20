﻿using System.ComponentModel.DataAnnotations.Schema;
using DAL.Enums;

namespace DAL.Entities;

[Table("users")]
public class Employee
{
    [Column("id")]
    public int Id { get; set; }
    [Column("name")]
    public string Name { get; set; }
    [Column("surname")]
    public string Surname { get; set; }
    [Column("email")]
    public string Email { get; set; }
    [Column("password")]
    public string? Password { get; set; }
    [Column("phone")]
    public string Phone { get; set; }
    [Column("role")]
    public Role Role { get; set; }
}