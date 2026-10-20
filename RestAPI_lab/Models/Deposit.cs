using System.ComponentModel.DataAnnotations;

namespace RestAPI.Models;

public class Deposit
{
    [Key]
    public int DepositCode { get; set; } // код вклада

    [Required]
    public string Name { get; set; } = string.Empty; // наименование вклада

    public int TermMonths { get; set; } // срок хранения (месяцев)

    public decimal InterestRate { get; set; } // ставка, % годовых
}
