using FirebaseAdmin.Auth;

namespace Collaborative_Tasks.Services
{
    public class FirebaseAuthService
    {
        public async Task<UserRecord> RegisterAsync(
            string email,
            string password)
        {
            var args = new UserRecordArgs
            {
                Email = email,
                Password = password
            };

            return await FirebaseAuth.DefaultInstance
                .CreateUserAsync(args);
        }

        public async Task<UserRecord> LoginAsync(
            string email,
            string password)
        {
            // Firebase Auth não valida senha diretamente via Admin SDK.
            // Em produção, isso seria feito pelo cliente (React/Flutter).
            // Aqui, simulamos a validação por email para fins de API/portfólio.

            var user = await FirebaseAuth.DefaultInstance
                .GetUserByEmailAsync(email);

            if (user == null)
                throw new Exception("Usuário não encontrado.");

            return user;
        }
    }
}