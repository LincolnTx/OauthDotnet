using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using OAuthTest.Models;

namespace OAuthTest.Models
{
    [DataContract]
    public class UserModel : ITrackable
    {
        public const int INACTIVE = 0;
        public const int ACTIVE = 1;
        public const int PENDING = 2;

        [DataMember]
        public string id { get; set; }

        [Column("channel_id")]
        [DataMember]
        public int channelId { get; set; }

        [Column("account_id")]
        [DataMember]
        public int accountId { get; set; }

        [Column("oms_account_id")]
        [DataMember]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string omsAccountId { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [RegularExpression(@"[a-zà-úA-ZÀ-Ú]*", ErrorMessage = "Apenas caracteres alfabéticos são permitidos")]
        [MaxLength(256, ErrorMessage = "Tamanho do campo deve ser entre 1 e 256 caracteres")]
        [DataMember]
        public string name { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [MaxLength(256, ErrorMessage = "Tamanho do campo deve ser entre 1 e 256 caracteres")]
        [RegularExpression(@"[a-zà-úA-ZÀ-Ú]*", ErrorMessage = "Apenas caracteres alfabéticos são permitidos")]
        [DataMember]
        public string surname { get; set; }

        [EmailAddress(ErrorMessage = "Campo não é um formato e-mail válido")]
        [Required(ErrorMessage = "Campo obrigatório")]
        [DataMember]
        public string email { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [MinLength(6, ErrorMessage = "Tamanho do campo deve conter no mínimo 6 caracteres")]
        public string password { get; set; }

        [Column("avatar_url")]
        [DataMember]
        public string avatarUrl { get; set; }

        [DataMember]
        public int status { get; set; }

        [Column("activated_at")]
        [DataMember]
        public DateTime? activatedAt { get; set; }

        [Column("created_at")]
        [DataMember]
        public DateTime? createdAt { get; set; }

        [Column("updated_at")]
        [DataMember]
        public DateTime? updatedAt { get; set; }

        [Column("password_last_change")]
        public DateTime? passwordLastChange { get; set; }

        [Column("unlock_at")]
        public DateTime? unlockAt { get; set; }

        [Column("invalid_attempts")]
        public int? invalidAttempts { get; set; }

        public bool admin { get; set; }
    }
}
