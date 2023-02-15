
from fastapi import HTTPException
import yaml

async def get_data(module: str) -> dict:
    with open("utils/exercise.yaml", 'r') as yaml_in:
        yaml_object = yaml.safe_load(yaml_in)
        return yaml_object[module]


class ServiceException(object):                              
    def __init__(self, errorcode, errodetails):                        
        self.errorcode = errorcode
        self.errodetails = errodetails
    def __get__(self, obj, objtype):
        return HTTPException(
                status_code=self.errorcode,
                detail=self.errodetails
            )
