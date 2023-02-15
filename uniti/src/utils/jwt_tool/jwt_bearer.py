
from fastapi import Request
from fastapi.security import HTTPBearer, HTTPAuthorizationCredentials

from exceptions.exceptions import AuthException
from .jwt_handler import decodeJWT


class JWTBearer(HTTPBearer):
    
    def __init__(self, auto_error: bool = True):
        super(JWTBearer, self).__init__(auto_error=auto_error)
    
    async def __call__(self, request: Request):
        credentials: HTTPAuthorizationCredentials = await super(
            JWTBearer, self).__call__(request)
        if credentials:
            if not credentials.scheme == "Bearer":
                raise AuthException.INVALID_TOKEN
            if not self.verify_jwt(credentials.credentials):
                raise AuthException.INVALID_TOKEN
            return credentials.credentials
        else:
            raise AuthException.INVALID_TOKEN

    def verify_jwt(self, jwtoken: str):
        isValid: bool = False
        if decodeJWT(jwtoken):
            isValid = True
        return isValid