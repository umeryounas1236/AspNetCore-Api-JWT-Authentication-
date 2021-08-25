namespace AspNet_Core_API__JWT_Authentication_.JwtService
{
    public interface IJwtHandler
    {
        string GenerateJWTToken(string userName);

        string ValidateJWTToken(string token);
    }
}
