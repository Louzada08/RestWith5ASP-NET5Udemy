using FluentValidation.Results;
using RestWithASPNET.Domain.Entities.Base;
using RestWithASPNET.Domain.Interfaces.Repositories;
using RestWithASPNET.Domain.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestWithASPNET.Domain.Entities
{
    [Table("users")]
    public class User : BaseEntity
    {
        [Column("user_name")]
        public string UserName { get; set; } = string.Empty;
        [Column("full_name")]
        public string FullName { get; set; } = string.Empty;

        [Column("email")]
        public string Email { get; set; } = string.Empty;

        [Column("password")]
        public string Password { get; set; } = string.Empty;
        [Column("refresh_token")]
        public string? RefreshToken { get; set; } = string.Empty;
        [Column("refresh_token_expiry_time")]
        public DateTime RefreshTokenExpiryTime { get; set; }
        public UserRolesEnum UserRole { get; set; }

        public ValidationResultBag CancelCreationIfExists(IGenericRepository<User> repository, User entity)
        {
            var result = new ValidationResultBag();
            var dbUser = repository.QueryableFor(u => u.Email == entity.Email).FirstOrDefault();

            if (dbUser != null)
                result.Errors.Add(new ValidationFailure(nameof(entity.Email), "Este e-mail já está em uso."));

            return result;
        }

        public ValidationResultBag CancelChangeIfExists(IGenericRepository<User> repository, User entity)
        {
            var result = new ValidationResultBag();
            var dbUserEmail = repository.QueryableFor(u => u.Email == entity.Email).FirstOrDefault();

            if (dbUserEmail != null && dbUserEmail.Id != entity.Id)
                result.Errors.Add(new ValidationFailure(nameof(entity.Email), "Este e-mail já está em uso."));

            //var dbUserCpf = repository.QueryableFor(u => u.Cpf == entity.Cpf).FirstOrDefault();
            //if (dbUserCpf != null && dbUserCpf.Id != entity.Id)
            //    result.Errors.Add(new ValidationFailure(nameof(entity.Cpf), "Este CPF/CNPJ já está em uso."));

            return result;
        }
    }
}
