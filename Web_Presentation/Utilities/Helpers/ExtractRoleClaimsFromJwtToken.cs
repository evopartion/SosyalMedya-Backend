using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Web_Presentation.Utilities.Helpers
{
    public class ExtractRoleClaimsFromJwtToken
    {
        public static List<string> GetRoleClaims(string jwtToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken=handler.ReadToken(jwtToken) as JwtSecurityToken;
            return jsonToken?.Claims.Where(claim=>claim.Type==ClaimTypes.Role).Select(claim=>claim.Value).ToList();
        }

        internal static object GetUserIdentityFromJwtToken(string jwtToken)
        {
            throw new NotImplementedException();
        }
    }
}
