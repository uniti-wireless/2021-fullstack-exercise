
from typing import List

from utils.tools import get_data

class CustomerService:

    async def get_customer(
        self,
        filter_by_num_employees: str = None,
        filter_by_tags: List[str] = None
    ):
        retval = await get_data("customers")

        if filter_by_tags:
            retval = await self.__filter_data_by_tags(
                filter=filter_by_tags,
                customer_data=retval
            )
            
        if filter_by_num_employees:
            retval = await self.__filter_data_by_num_employees(
                filter=filter_by_num_employees,
                customer_data=retval
            )

        return retval
    
    async def __filter_data_by_tags(
        self, 
        filter: List[str],
        customer_data: List[dict]
    ) -> List[dict]:
        return [d for d in customer_data if set(filter).intersection(d["tags"])]
        
    
    async def __filter_data_by_num_employees(
        self,
        filter: str,
        customer_data: List[dict]
    ):
        # range filter
        if "-" in filter:
            ranger_filter = filter.split("-")
            return [
                d for d in customer_data 
                if (int(d["num_employees"]) >= int(ranger_filter[0]) and
                    int(d["num_employees"]) <= int(ranger_filter[1]))
            ]
        # greater than filter
        elif "gt" in filter:
            num_filter = filter.replace("gt", "")
            return [d for d in customer_data if int(d["num_employees"]) > int(num_filter)]
        # specific number
        else:
            return [d for d in customer_data if int(d["num_employees"]) == int(filter)]
