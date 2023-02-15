
from logging.config import dictConfig
from fastapi import FastAPI
from fastapi_pagination import add_pagination

from modules import customer, auth
from settings.logconfig import LogConfig


# logger config
dictConfig(LogConfig().dict())

app = FastAPI(
    title="Uniti",
    description="Uniti Test Api",
    version="1.0.0",
    contact={
        "name": "Razen Marling",
        "email": "razen.marling2@gmail.com",
    },
)

# routers
app.include_router(customer.router)
app.include_router(auth.router)

#pagination
add_pagination(app)
