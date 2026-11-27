using System;

namespace Lab5.Models;

public class Client
{
    public int Id { get; set; }
    public string LastName { get; set; } = "";
    public string FirstName { get; set; } = "";
    public string MiddleName { get; set; } = "";
    public string Residence { get; set; } = "";

    public Client() { }

    public Client(int id, string ln, string fn, 
                  string mn, string res)
    {
        Id = id; 
        LastName = ln; 
        FirstName = fn; 
        MiddleName = mn; 
        Residence = res;
    }

    public string FullName => 
        $"{LastName} {FirstName} {MiddleName}".Trim();

    public override string ToString() => 
        $"[{Id}] {FullName,-35} | {Residence}";
}