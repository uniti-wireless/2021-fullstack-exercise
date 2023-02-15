
from pydantic import BaseModel


class CustomerSchema(BaseModel):
    id: str
    num_employees: int
    name: str
    tags: list
