
from typing import List
from fastapi_utils.cbv import cbv
from fastapi_utils.inferring_router import InferringRouter
from fastapi import Depends, status, Query
from fastapi_pagination import Page, paginate

from utils.urls import CustomerURL
from .schema import CustomerSchema
from .service import CustomerService
from utils.jwt_tool.jwt_bearer import JWTBearer

router = InferringRouter(
    tags=["Customer Module"]
)


@cbv(router)
class CustomerController:

    customer_service : object = Depends(CustomerService)

    @router.get(CustomerURL.GET_CUSTOMER, 
        response_model=Page[CustomerSchema], 
        status_code=status.HTTP_200_OK,
        dependencies=[Depends(JWTBearer())]
    )
    async def get_customer(
        self,
        num_employees: str | None = Query(None, regex="^gt\d+|^\d+-\d+|^\d+", 
            description="Ex. gt50 = greater than, 1-10 = range query, 50 = specific number query"
        ),
        tags: List[str] | None = Query(None)
    ) -> str:
        ret_obj = await self.customer_service.get_customer(
            filter_by_num_employees=num_employees,
            filter_by_tags=tags
        )
        return paginate(ret_obj)
