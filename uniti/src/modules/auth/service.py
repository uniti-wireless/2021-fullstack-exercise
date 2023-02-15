
from utils.tools import get_data
from .schema import AuthRequestSchema, AuthResponseSchema
from exceptions.exceptions import AuthException
from utils.jwt_tool.jwt_handler import signJWT


class AuthService:

    async def validate_user(
        self, 
        postData: AuthRequestSchema
    ) -> AuthResponseSchema:
        users = await get_data("auth")
        for user in users:
            if user["username"] == postData.username and user["password"] == postData.password:
                # createJWTtoken
                return AuthResponseSchema(
                    auth_token=signJWT(postData.username)
                )
        raise AuthException.INVALID_CREDENTIALS
