using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankDepositsApi.Models;

public class Deposit
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    public int TermMonths { get; set; }

    public decimal InterestRate { get; set; }

    public ICollection<Account> Accounts { get; set; } = new List<Account>();
}

public class Client
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string FullName { get; set; } = string.Empty;

    [Required]
    public string PassportNumber { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty;

    [Required]
    public string Login { get; set; } = string.Empty;

    public ICollection<Account> Accounts { get; set; } = new List<Account>();
}

public class Account
{
    [Key]
    public string AccountNumber { get; set; } = string.Empty;

    public int ClientId { get; set; }
    [ForeignKey("ClientId")]
    public Client? Client { get; set; }

    public int DepositId { get; set; }
    [ForeignKey("DepositId")]
    public Deposit? Deposit { get; set; }

    public DateTime OpenDate { get; set; }
    public DateTime? CloseDate { get; set; }

    public decimal Amount { get; set; }
}
