using System.IdentityModel.Tokens.Jwt;

class Service {
    public static string GetIdFromToken(HttpRequest request) {
        try {
            var header = request.Headers["Authorization"]!.ToString();
            string token = header.Split(" ")[1];
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);
            var claims = jwtToken.Claims;

            return claims.FirstOrDefault(c => c.Type == "sub")?.Value!;
        } catch (Exception error) {
            return error.Message;
        }
    }
}

