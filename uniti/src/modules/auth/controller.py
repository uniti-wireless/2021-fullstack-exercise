
from fastapi import Depends, status
from fastapi_utils.cbv import cbv
from fastapi_utils.inferring_router import InferringRouter

from utils.urls import AuthURL
from .schema import AuthRequestSchema, AuthResponseSchema
from .service import AuthService

router = InferringRouter(
    tags=["Auth Module"]
)


@cbv(router)
class AuthController:

    auth_service : object = Depends(AuthService)

    @router.post(
        AuthURL.REQUEST_TOKEN, 
        response_model=AuthResponseSchema, 
        status_code=status.HTTP_200_OK
    )
    async def get_token(    
        self,
        postData: AuthRequestSchema
    ) -> AuthResponseSchema:

        return await self.auth_service.validate_user(postData)
