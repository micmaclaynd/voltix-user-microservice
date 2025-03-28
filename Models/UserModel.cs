using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Voltix.Shared.Models;


namespace Voltix.UserMicroservice.Models;

[Table("users")]
public class UserModel : BaseModel {
    [Required]
    [StringLength(256)]
    [Column("email", TypeName = "varchar(256)")]
    public required string Email { get; set; }

    [Required]
    [StringLength(64)]
    [Column("name", TypeName = "varchar(64)")]
    public required string Name { get; set; }

    [Required]
    [StringLength(64)]
    [Column("surname", TypeName = "varchar(64)")]
    public required string Surname { get; set; }

    [Required]
    [StringLength(64)]
    [Column("patronymic", TypeName = "varchar(64)")]
    public required string Patronymic { get; set; }

    [StringLength(1024)]
    [Column("description", TypeName = "varchar(1024)")]
    public string? Description { get; set; } = null;

    [Required]
    [StringLength(60)]
    [Column("password_hash", TypeName = "char(60)")]
    public required string PasswordHash { get; set; }

    [Required]
    [Column("role_id", TypeName = "int")]
    public required int RoleId { get; set; }

    [Required]
    [Column("registered_datetime", TypeName = "timestamp with time zone")]
    public required DateTime RegisteredDatetime { get; set; }

    [Required]
    [Column("is_banned", TypeName = "bool")]
    public bool IsBanned { get; set; } = false;

    [Required]
    [Column("is_email_confirmed", TypeName = "bool")]
    public bool IsEmailConfirmed { get; set; } = false;

    [Column("telegram_id", TypeName = "bigint")]
    public long? TelegramId { get; set; } = null;

    [StringLength(32)]
    [Column("telegram_username", TypeName = "varchar(32)")]
    public string? TelegramUsername { get; set; } = null;
}
